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
    /// Interaction logic for SelectVehicleWindow.xaml
    /// </summary>
    public partial class SelectVehicleWindow : Window {

        ServiceDBEntities db;
        ICollectionView view;
        AddTransportWindow parent;

        public SelectVehicleWindow(AddTransportWindow _parent, ServiceDBEntities _db) {
            InitializeComponent();
            parent = _parent;
            db = _db;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            var list = db.Vehicles.ToList();
            view = CollectionViewSource.GetDefaultView(list);
            dataGrid.ItemsSource = view;
        }

        private void dataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var row = sender as DataGridRow;

            if (row != null) {
                var item = row.DataContext as Vehicles;

                if (item != null) {

                    parent.vehicle = item;
                    parent.vehicleTextBox.Text = $"{item.make} {item.model} {item.registration}";
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
                    Vehicles i = item as Vehicles;
                    if (i != null) {
                        if (!string.IsNullOrWhiteSpace(i.make) && i.make.Contains(filter) 
                        || !string.IsNullOrWhiteSpace(i.model) && i.model.Contains(filter)
                        || !string.IsNullOrWhiteSpace(i.registration) && i.registration.Contains(filter)
                        || !string.IsNullOrWhiteSpace(i.Vehicle_types.name) && i.Vehicle_types.name.Contains(filter))
                            return true;
                    }
                    return false;
                };
        }
    }
}
