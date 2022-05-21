using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TransportationService {
    /// <summary>
    /// Interaction logic for SelectCustomerWindow.xaml
    /// </summary>
    public partial class SelectCustomerWindow : Window {

        ServiceDBEntities db;
        ICollectionView view;
        AddTransportWindow parent;

        public SelectCustomerWindow(AddTransportWindow _parent, ServiceDBEntities _db) {
            InitializeComponent();
            parent = _parent;
            db = _db;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            
            var list = db.Customers.ToList();
            view = CollectionViewSource.GetDefaultView(list);
            dataGrid.ItemsSource = view;
        }

        private void addAndSelect_Click(object sender, RoutedEventArgs e) {
            
            if (customerTextBox.Text != "") {
                Customers newCustomer = new Customers();
                newCustomer.name = customerTextBox.Text;
                db.Customers.Add(newCustomer);
                db.SaveChanges();

                parent.customer = newCustomer;
                parent.customerTextBox.Text = newCustomer.name;
                this.Close();
            }
        }

        private void dataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var row = sender as DataGridRow;

            if (row != null) {
                var item = row.DataContext as Customers;

                if (item != null) {

                    parent.customer = item;
                    parent.customerTextBox.Text = item.name;
                    this.Close();

                }
            }
        }

        private void filterTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            
            string filter = filterTextBox.Text;
            if (string.IsNullOrEmpty(filter))
                view.Filter = null;
            else
                view.Filter = item =>
                {
                    Customers i = item as Customers;
                    if (i != null) {
                        if (!string.IsNullOrWhiteSpace(i.name) && i.name.Contains(filter))
                            return true;
                    }
                    return false;
                };
        }
    }
}
