using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace TransportationService {
    /// <summary>
    /// Interaction logic for EditVehicleWindow.xaml
    /// </summary>
    public partial class EditVehicleWindow : Window {

        ServiceDBEntities db;
        int vehicleID;
        Vehicles vehicle;
        Validate v;
        PageVehicles parent;

        public EditVehicleWindow(ServiceDBEntities _db, int _vehicleID, PageVehicles _parent) {
            InitializeComponent();
            v = new Validate(_db);
            db = _db;
            vehicleID = _vehicleID;
            parent = _parent;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            vehicleTypeComboBox.ItemsSource = db.Vehicle_types.ToList();
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
                parent.loadDataGrid();
                this.Close();
                //MessageBox.Show("Vehicle edited successfuly");
                var box = new MsgBox("Vehicle edited successfuly");
                box.Show();
            } else {
                //MessageBox.Show("No changes made");
                var box = new MsgBox("No changes made");
                box.Show();
            }

        }


        public bool updateVehicle() {

            bool changed = false;
            decimal avgFuelConsumption;

            if (makeTextBox.Text != vehicle.make && !v.checkIfNull(makeTextBox.Text)) {
                vehicle.make = makeTextBox.Text;
                changed = true;
            }

            if (modelTextBox.Text != vehicle.model && !v.checkIfNull(modelTextBox.Text)) {
                vehicle.model = makeTextBox.Text;
                changed = true;
            }

            if (registrationTextBox.Text != vehicle.registration && !v.checkIfNull(registrationTextBox.Text)) {
                vehicle.registration = registrationTextBox.Text;
                changed = true;
            }

            if (fuelTextBox.Text != vehicle.avg_fuel_consumption.ToString() && !v.checkIfNull(fuelTextBox.Text)) {
                
                if(decimal.TryParse(fuelTextBox.Text, out avgFuelConsumption)) {

                    vehicle.avg_fuel_consumption = avgFuelConsumption;
                    changed = true;
                }
            }

            if (vehicleTypeComboBox.SelectedIndex != -1) {
                int id = Convert.ToInt32(vehicleTypeComboBox.SelectedValue.ToString());
                vehicle.Vehicle_types = db.Vehicle_types.FirstOrDefault(t => t.id == id);
                changed = true;
            }


            return changed;
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

