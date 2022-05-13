using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System;

namespace TransportationService {
    /// <summary>
    /// Logika interakcji dla klasy AddVehicleWindow.xaml
    /// </summary>
    public partial class AddVehicleWindow : Window {
        ServiceDBEntities db;

        public AddVehicleWindow(ServiceDBEntities _db) {
            InitializeComponent();
            db = _db;

            vehicleTypeComboBox.ItemsSource = db.Vehicle_types.ToList();

        }

        private void submitBtn_Click(object sender, RoutedEventArgs e) {

            decimal avgFuelConsumption;

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
            
            if (!decimal.TryParse(fuelTextBox.Text, out avgFuelConsumption)) {
                MessageBox.Show("Entered average fuel consumption of the vehicle is invalid!");
                return;
            }

            Vehicles newVehicle = new Vehicles();
            newVehicle.make = makeTextBox.Text;
            newVehicle.model = modelTextBox.Text;
            newVehicle.registration = registrationTextBox.Text;
            newVehicle.avg_fuel_consumption = avgFuelConsumption;

            int id = Convert.ToInt32(vehicleTypeComboBox.SelectedValue.ToString());
            newVehicle.Vehicle_types = db.Vehicle_types.FirstOrDefault(t => t.id == id);

            db.Vehicles.Add(newVehicle);
            db.SaveChanges();

            this.Close();
            MessageBox.Show("Pomyslnie dodano pojazd");

        }
    }
}
