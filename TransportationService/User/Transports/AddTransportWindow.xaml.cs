using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TransportationService
{
    /// <summary>
    /// Interaction logic for AddTransportWindow.xaml
    /// </summary>
    public partial class AddTransportWindow : Window {

        ServiceDBEntities db;

        public Users user { get; set; }
        public Customers selectedCustomer { get; set; }
        public Vehicles selectedVehicle { get; set; }
        public Drivers selectedDriver { get; set; }

        int userID;

        public AddTransportWindow(ServiceDBEntities _db, int _userID) {
            InitializeComponent();
            db = _db;
            userID = _userID;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            user = db.Users.Where(u => u.id == userID).FirstOrDefault();

            if (user == null) {
                this.Close();
                MessageBox.Show("Unknown error occured");
            } else {
                userTextBox.Text = user.login;
            }
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e) {

            int distance;
            int weight;
            decimal cost;
            decimal income;

            /*
             * TODO: Walidacja
             */

            if (!int.TryParse(distanceTextBox.Text, out distance)) {
                MessageBox.Show("Entered distance is invalid!");
                return;
            }
            if (!int.TryParse(weightTextBox.Text, out weight)) {
                MessageBox.Show("Entered weight is invalid!");
                return;
            }
            if (!decimal.TryParse(weightTextBox.Text, out cost)) {
                MessageBox.Show("Entered cost is invalid!");
                return;
            }
            if (!decimal.TryParse(weightTextBox.Text, out income)) {
                MessageBox.Show("Entered income is invalid!");
                return;
            }

            if (true /* TODO: Walidacja */) {
                Transports newTransport = new Transports();
                Status status = db.Status.Where(i => i.name.Equals("active")).FirstOrDefault();

                newTransport.Status = status;
                newTransport.origin = originTextBox.Text;
                newTransport.destination = destinationTextBox.Text;
                newTransport.distance = distance;
                newTransport.weight = weight;
                newTransport.start_date = startDatePicker.SelectedDate.Value;
                newTransport.end_date = endDatePicker.SelectedDate.Value;
                newTransport.Users = user;
                newTransport.Customers = selectedCustomer;
                newTransport.Vehicles = selectedVehicle;
                newTransport.Drivers = selectedDriver;
                newTransport.cost = cost;
                newTransport.income = income;

                db.Transports.Add(newTransport);
                db.SaveChanges();
                this.Close();
                MessageBox.Show("Transport added successfuly");
            }

        }

        private void selectVehicle_Click(object sender, RoutedEventArgs e) {
           
            if (startDatePicker.SelectedDate != null && endDatePicker.SelectedDate != null && int.TryParse(weightTextBox.Text, out int test)) {
                SelectVehicleWindow selectVehicleWindow = new SelectVehicleWindow(this, db);
                selectVehicleWindow.ShowDialog();
            } else {
                MessageBox.Show("Fill in the dates and weight to be able to select a vehicle");
            }
        }

        private void selectDriver_Click(object sender, RoutedEventArgs e) {
            if (selectedVehicle != null) {
                SelectDriverWindow selectDriverWindow = new SelectDriverWindow(this, db);
                selectDriverWindow.ShowDialog();
            } else {
                MessageBox.Show("Select a vehicle to be able to select a driver");
            }
        }


        private void selectCustomer_Click(object sender, RoutedEventArgs e) {
            SelectCustomerWindow selectCustomerWindow = new SelectCustomerWindow(this, db);
            selectCustomerWindow.ShowDialog();
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void startDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            validateDate();
        }

        private void endDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            validateDate();
        }

        private void validateDate() {

            if (startDatePicker.SelectedDate == null || endDatePicker.SelectedDate == null) {
                return;
            }

            if (startDatePicker.SelectedDate > endDatePicker.SelectedDate) {
                endDatePicker.SelectedDate = null;
            }

            selectedVehicle = null;
            vehicleTextBox.Text = "";
        }

        private void weightTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            selectedVehicle = null;
            vehicleTextBox.Text = "";
            selectedDriver = null;
            driverTextBox.Text = "";
        }
    }
}
