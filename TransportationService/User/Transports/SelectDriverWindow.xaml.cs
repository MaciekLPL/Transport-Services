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
        TransportWindow parent;

        public SelectDriverWindow(TransportWindow _parent, ServiceDBEntities _db) {
            InitializeComponent();
            parent = _parent;
            db = _db;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            Vehicles choosenVehicle = parent.selectedVehicle;
            
            DateTime start = parent.startDate.SelectedDate.Value;
            DateTime end = parent.endDate.SelectedDate.Value;

            /*
             * TODO: Walidacja czy wszystko ustawione
             */


            IQueryable<Drivers> query;

            if (parent.transport != null) {
                query = from d in db.Drivers
                        where (db.Licenses.Any(l => (l.driver_id == d.id) && (l.vehicle_type_id == choosenVehicle.type_id)))
                        && (!db.Transports.Any(t => (t.driver_id == d.id) && (t.id != parent.transport.id) && (t.start_date <= end) && (start <= t.end_date) && t.Status.name == "active"))
                        select d;
            } else {
                query = from d in db.Drivers
                        where (db.Licenses.Any(l => (l.driver_id == d.id) && (l.vehicle_type_id == choosenVehicle.type_id)))
                        && (!db.Transports.Any(t => (t.driver_id == d.id) && (t.start_date <= end) && (start <= t.end_date) && t.Status.name == "active"))
                        select d;
            }

            var list = query.ToList();
            view = CollectionViewSource.GetDefaultView(list);
            dataGrid.ItemsSource = view;
        }

        private void dataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var row = sender as DataGridRow;

            if (row != null) {
                var item = row.DataContext as Drivers;

                if (item != null) {

                    parent.selectedDriver = item;
                    parent.driverTextbox.Text = $"{item.name} {item.surname}";
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
