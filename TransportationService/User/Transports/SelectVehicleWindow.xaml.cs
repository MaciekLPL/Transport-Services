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
        TransportWindow parent;

        public SelectVehicleWindow(TransportWindow _parent, ServiceDBEntities _db) {
            InitializeComponent();
            parent = _parent;
            db = _db;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            int weight;
            if (!int.TryParse(parent.weightTextbox.Text, out weight)) {
                this.Close();
            }

            DateTime start = parent.startDate.SelectedDate.Value;
            DateTime end = parent.endDate.SelectedDate.Value;

            /*
             * TODO: Walidacja czy waga i daty są ustawione
             */

            var query = from v in db.Vehicles
                        where (v.Vehicle_types.max_load > weight) 
                        && (!db.Transports.Any(t => (t.vehicle_id == v.id) && (t.start_date <= end) && (start <= t.end_date)))
                        select v;

            var list = query.ToList();
            view = CollectionViewSource.GetDefaultView(list);
            dataGrid.ItemsSource = view;
        }

        private void dataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var row = sender as DataGridRow;

            if (row != null) {
                var item = row.DataContext as Vehicles;

                if (item != null) {
                    parent.selectedVehicle = item;
                    parent.vehicleTextbox.Text = $"{item.registration} {item.Vehicle_types.name}";
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
