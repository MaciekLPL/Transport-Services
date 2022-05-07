using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace TransportationService {
    /// <summary>
    /// Logika interakcji dla klasy AddVehicleWindow.xaml
    /// </summary>
    public partial class AddVehicleWindow : Window {
        ServiceDBEntities db;

        List<Vehicle_types> vehicleTypes;
        public AddVehicleWindow(ServiceDBEntities _db) {
            InitializeComponent();
            db = _db;

            vehicleTypes = db.Vehicle_types.ToList();
            populateComboBox();
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e) {

            double avgFuelConsumption;

            if (makeTextBox.Text.Length == 0) {
                MessageBox.Show("Enter vehicle make");
                return;
            }

            if (modelTextBox.Text.Length == 0) {
                MessageBox.Show("Enter vehicle model");
                return;
            }

            if (registrationTextBox.Text.Length == 0) {
                MessageBox.Show("Enter vehicle registration");
                return;
            }

            if (fuelTextBox.Text.Length == 0) {
                MessageBox.Show("Enter vehicle fuel consumption");
                return;
            }

            if (vehicleTypeComboBox.SelectedIndex == -1) {
                MessageBox.Show("Select vehicle type");
                return;
            } 
            
            if (!double.TryParse(fuelTextBox.Text, out avgFuelConsumption)) {
                MessageBox.Show("Entered average fuel consumption of the vehicle is invalid!");
                return;
            }

            Vehicles newVehicle = new Vehicles();
            newVehicle.make = makeTextBox.Text;
            newVehicle.model = modelTextBox.Text;
            newVehicle.registration = registrationTextBox.Text;
            newVehicle.avg_fuel_consumption = avgFuelConsumption;               //do poprawy w bazie na decimale (+ w transportach poprawa przecinków w decimalach)

            newVehicle.Vehicle_types = db.Vehicle_types.FirstOrDefault(t => t.name.Equals(vehicleTypeComboBox.SelectedValue.ToString()));

            db.Vehicles.Add(newVehicle);
            db.SaveChanges();

            this.Close();
            MessageBox.Show("Pomyslnie dodano pojazd");

        }

        private void populateComboBox() {

            foreach (var vehicle in vehicleTypes)
                vehicleTypeComboBox.Items.Add(vehicle.name);
        }
    }
}
