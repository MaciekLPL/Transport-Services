using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TransportationService
{
    /// <summary>
    /// Interaction logic for PageVehicles.xaml
    /// </summary>
    public partial class PageVehicles : Page
    {

        ServiceDBEntities db;
        
        public PageVehicles()
        {
            InitializeComponent();

            db = new ServiceDBEntities();
            db.Vehicles.ToList();
            this.DataContext = db.Vehicles.Local;
        }

        private void AddVehicle_Click(object sender, RoutedEventArgs e) {
            AddVehicleWindow addVehicleWindow = new AddVehicleWindow(db);
            addVehicleWindow.ShowDialog();
        }

        private void AddVehicleType_Click(object sender, RoutedEventArgs e) {
            AddVehicleTypeWindow addVehicleTypeWindow = new AddVehicleTypeWindow(db);
            addVehicleTypeWindow.ShowDialog();
        }

        private void dataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var row = sender as DataGridRow;

            if (row != null) {
                var item = row.DataContext as Vehicles;

                if (item != null) {
                    MessageBox.Show($"{item.id} {item.make} {item.model}");
                }

            }
        }

    }
}
