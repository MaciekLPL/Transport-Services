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
using System.Windows.Shapes;

namespace TransportationService {
    /// <summary>
    /// Interaction logic for EditVehicleWindow.xaml
    /// </summary>
    public partial class EditVehicleWindow : Window {

        ServiceDBEntities db;
        int vehicleID;
        Vehicles vehicle;
        List<Vehicle_types> vehicleTypes;

        public EditVehicleWindow(ServiceDBEntities _db, int _vehicleID) {
            InitializeComponent();

            db = _db;
            vehicleID = _vehicleID;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            vehicleTypes = db.Vehicle_types.ToList();
            foreach (var vehicle in vehicleTypes)
                vehicleTypeComboBox.Items.Add(vehicle.name);

            vehicle = db.Vehicles.Where(i => i.id == vehicleID).SingleOrDefault();

            if (vehicle == null) {
                this.Close();
            } else {

                makeTextBox.Text = vehicle.make;
                modelTextBox.Text = vehicle.model;
                registrationTextBox.Text = vehicle.registration;
                fuelTextBox.Text = vehicle.avg_fuel_consumption.ToString();

            }
        }


        private void submitBtn_Click(object sender, RoutedEventArgs e) {

            bool wasChanged = updateVehicle();

            if (wasChanged) {
                db.SaveChanges();
                this.Close();
                MessageBox.Show("Vehicle edited successfuly");
            } else {
                MessageBox.Show("No changes made");
            }

        }


        public bool updateVehicle() {

            bool changed = false;
            //double avgFuelConsumption;

            if (makeTextBox.Text != vehicle.make) {
                vehicle.make = makeTextBox.Text;
                changed = true;
            }

            if (modelTextBox.Text != vehicle.model) {
                vehicle.model = makeTextBox.Text;
                changed = true;
            }

            if (registrationTextBox.Text != vehicle.registration) {
                vehicle.registration = registrationTextBox.Text;
                changed = true;
            }

            /*if (fuelTextBox.Text != vehicle.avg_fuel_consumption.ToString()) {
                
                if(!double.TryParse(fuelTextBox.Text, out avgFuelConsumption)) {

                    vehicle.avg_fuel_consumption = avgFuelConsumption;
                    changed = true;
                }
            }*/

            if (vehicleTypeComboBox.SelectedIndex != -1)
                vehicle.Vehicle_types = db.Vehicle_types.FirstOrDefault(t => t.name.Equals(vehicleTypeComboBox.SelectedValue.ToString()));

            return changed;
        }
    }

}

