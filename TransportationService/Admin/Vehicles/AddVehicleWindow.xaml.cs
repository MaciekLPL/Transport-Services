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
        Validate v;

        public AddVehicleWindow(ServiceDBEntities _db) {
            InitializeComponent();
            v = new Validate(_db);
            db = _db;

            vehicleTypeComboBox.ItemsSource = db.Vehicle_types.ToList();

        }

        private void submitBtn_Click(object sender, RoutedEventArgs e) {

            decimal avgFuelConsumption;

            if (v.checkIfNull(makeTextBox.Text)) {
                //MessageBox.Show("Enter vehicle make");
                var box = new MsgBox("Enter vehicle make");
                box.Show();

                return;
            }

            if (v.checkIfNull(modelTextBox.Text)) {
                //MessageBox.Show("Enter vehicle model");
                var box = new MsgBox("Enter vehicle model");
                box.Show();

                return;
            }

            if (v.checkIfNull(registrationTextBox.Text)) {
                //MessageBox.Show("Enter vehicle registration");
                var box = new MsgBox("Enter vehicle registration");
                box.Show();

                return;
            }

            if (v.checkIfNull(fuelTextBox.Text)) {
                //MessageBox.Show("Enter vehicle fuel consumption");
                var box = new MsgBox("Enter vehicle fuel consumption");
                box.Show();

                return;
            }

            if (vehicleTypeComboBox.SelectedIndex == -1) {
                //MessageBox.Show("Select vehicle type");
                var box = new MsgBox("Select vehicle type");
                box.Show();

                return;
            } 
            
            if (!decimal.TryParse(fuelTextBox.Text, out avgFuelConsumption)) {
                //MessageBox.Show("Entered average fuel consumption of the vehicle is invalid!");
                var box = new MsgBox("Entered average fuel consumption of the\nvehicle is invalid!");
                box.Show();

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
            //MessageBox.Show("Vehicle added successfuly");
            var Box = new MsgBox("Vehicle added successfuly");
            Box.Show();
        }

        public void decimalValidation(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^,0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
