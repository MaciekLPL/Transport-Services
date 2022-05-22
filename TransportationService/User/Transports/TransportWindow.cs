using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TransportationService {
    public class TransportWindow : Window {

        public ServiceDBEntities db;
        public Customers selectedCustomer { get; set; }
        public Vehicles selectedVehicle { get; set; }
        public Drivers selectedDriver { get; set; }

        public TextBox driverTextbox;
        public TextBox vehicleTextbox;
        public TextBox customerTextbox;
        public TextBox weightTextbox;

        public DatePicker startDate;
        public DatePicker endDate;


        public TransportWindow(ServiceDBEntities _db) {
            db = _db;
        }

        public void selectVehicle_Click(object sender, RoutedEventArgs e) {

            if (startDate.SelectedDate != null && endDate.SelectedDate != null && int.TryParse(weightTextbox.Text, out int test)) {
                SelectVehicleWindow selectVehicleWindow = new SelectVehicleWindow(this, db);
                selectVehicleWindow.ShowDialog();
            } else {
                MessageBox.Show("Fill in the dates and weight to be able to select a vehicle");
            }
        }

        public void selectDriver_Click(object sender, RoutedEventArgs e) {
            if (selectedVehicle != null) {
                SelectDriverWindow selectDriverWindow = new SelectDriverWindow(this, db);
                selectDriverWindow.ShowDialog();
            } else {
                MessageBox.Show("Select a vehicle to be able to select a driver");
            }
        }

        public void selectCustomer_Click(object sender, RoutedEventArgs e) {
            SelectCustomerWindow selectCustomerWindow = new SelectCustomerWindow(this, db);
            selectCustomerWindow.ShowDialog();
        }


        public void startDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            validateDate();
        }

        public void endDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            validateDate();
        }
        
        public void weightTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            selectedVehicle = null;
            vehicleTextbox.Text = "";
            selectedDriver = null;
            driverTextbox.Text = "";
        }

        public void validateDate() {

            if (startDate.SelectedDate == null || endDate.SelectedDate == null) {
                return;
            }

            if (startDate.SelectedDate > endDate.SelectedDate) {
                endDate.SelectedDate = null;
            }

            selectedVehicle = null;
            vehicleTextbox.Text = "";
        }

        public void NumberValidation(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
