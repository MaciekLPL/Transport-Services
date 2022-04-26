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
        public AddVehicleWindow() {
            InitializeComponent();
            db = new ServiceDBEntities();

            vehicleTypes = db.Vehicle_types.ToList();
            populateComboBox();
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e) {

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

            var regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            if (!regex.IsMatch(fuelTextBox.Text)) {
                MessageBox.Show("Fuel consumption value is invalid");
                return;
            }

            Vehicles newVehicle = new Vehicles();
            newVehicle.make = makeTextBox.Text;
            newVehicle.model = modelTextBox.Text;
            newVehicle.registration = registrationTextBox.Text;
            newVehicle.avg_fuel_consumption = double.Parse(fuelTextBox.Text);

            Vehicle_types type = db.Vehicle_types.FirstOrDefault(t => t.name.Equals(vehicleTypeComboBox.SelectedValue.ToString()));
            newVehicle.Vehicle_types = type;

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
