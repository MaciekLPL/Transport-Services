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
    /// Interaction logic for SelectDriverWindow.xaml
    /// </summary>
    public partial class SelectDriverWindow : Window {

        ServiceDBEntities db;
        ICollectionView view;
        AddTransportWindow parent;

        public SelectDriverWindow(AddTransportWindow _parent, ServiceDBEntities _db) {
            InitializeComponent();
            parent = _parent;
            db = _db;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            var list = db.Drivers.ToList();
            view = CollectionViewSource.GetDefaultView(list);
            dataGrid.ItemsSource = view;
        }

        private void dataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var row = sender as DataGridRow;

            if (row != null) {
                var item = row.DataContext as Drivers;

                if (item != null) {

                    parent.driver = item;
                    parent.driverTextBox.Text = $"{item.name} {item.surname}";
                    this.Close();

                }
            }
        }

        private void filterTextBox_TextChanged(object sender, TextChangedEventArgs e) {

            string filter = filterTextBox.Text;
            if (string.IsNullOrEmpty(filter))
                view.Filter = null;
            else
                view.Filter = item => {
                    Drivers i = item as Drivers;
                    if (i != null) {
                        if (!string.IsNullOrWhiteSpace(i.name) && i.name.Contains(filter) || !string.IsNullOrWhiteSpace(i.surname) && i.surname.Contains(filter))
                            return true;
                    }
                    return false;
                };
        }
    }
}
