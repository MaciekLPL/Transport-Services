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
        public Transports transport { get; set; }

        public TextBox driverTextbox;
        public TextBox vehicleTextbox;
        public TextBox customerTextbox;
        public TextBox weightTextbox;
        
        public TextBox costTextbox;
        public TextBox incomeTextbox;

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
                //MessageBox.Show("Fill in the dates and weight to be able to select a vehicle");
                var box = new MsgBox("Fill in the dates and weight to\nbe able to select a vehicle");
                box.Show();
            }
        }

        public void selectDriver_Click(object sender, RoutedEventArgs e) {
            if (selectedVehicle != null) {
                SelectDriverWindow selectDriverWindow = new SelectDriverWindow(this, db);
                selectDriverWindow.ShowDialog();
            } else {
                //MessageBox.Show("Select a vehicle to be able to select a driver");
                var box = new MsgBox("Select a vehicle to be able to\nselect a driver");
                box.Show();
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
            costTextbox.Text = "";
            incomeTextbox.Text = "";
        }

        public void vehicleTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            selectedDriver = null;
            driverTextbox.Text = "";
            costTextbox.Text = "";
            incomeTextbox.Text = "";
        }

        public void costTextBox_TextChanged(object sender, TextChangedEventArgs e) {

            if (selectedDriver == null || selectedVehicle == null) {
                costTextbox.Text = "";
                incomeTextbox.Text = "";
                return;
            }

            decimal cost;

            if (costTextbox.Text != "" && decimal.TryParse(costTextbox.Text, out cost)) {
                TimeSpan difference = endDate.SelectedDate.Value.Subtract(startDate.SelectedDate.Value);
                decimal hours = (decimal)difference.TotalHours;
                incomeTextbox.Text = (cost - (hours * selectedDriver.hourly_rate)).ToString();
            } else {
                costTextbox.Text = "";
                incomeTextbox.Text = "";
            }
        }

        public void validateDate() {

            selectedVehicle = null;
            vehicleTextbox.Text = "";
            costTextbox.Text = "";
            incomeTextbox.Text = "";

            if (startDate.SelectedDate == null || endDate.SelectedDate == null)
                return;

            if (startDate.SelectedDate.Value > endDate.SelectedDate.Value)
                endDate.SelectedDate = null;
        }

        public void NumberValidation(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
