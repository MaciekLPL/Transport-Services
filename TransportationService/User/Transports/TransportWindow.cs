using System;
using System.Text.RegularExpressions;
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
        
        public TextBox distanceTextbox;
        public TextBox fuelTextbox;
        public TextBox costTextbox;
        public TextBox incomeTextbox;

        public DatePicker startDate;
        public DatePicker endDate;

        public bool skipCalculation = false;

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
            fuelTextbox.Text = "";
            costTextbox.Text = "";
            incomeTextbox.Text = "";

            int weight;
            if (!int.TryParse(weightTextbox.Text, out weight))
                weightTextbox.Text = "";
        }

        public void vehicleTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            selectedDriver = null;
            driverTextbox.Text = "";
            fuelTextbox.Text = "";
            costTextbox.Text = "";
            incomeTextbox.Text = "";
        }

        public void driverTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            fuelTextbox.Text = "";
            costTextbox.Text = "";
            incomeTextbox.Text = "";
        }

        public void costTextBox_TextChanged(object sender, TextChangedEventArgs e) {

            if (skipCalculation) return;

            if (selectedDriver == null || selectedVehicle == null) {
                costTextbox.Text = "";
                incomeTextbox.Text = "";
                return;
            }

            decimal cost;

            if (decimal.TryParse(costTextbox.Text, out cost) && distanceTextbox.Text != "" && fuelTextbox.Text != "" && costTextbox.Text != "") {
                    TimeSpan difference = endDate.SelectedDate.Value.Subtract(startDate.SelectedDate.Value);
                    decimal hours = (decimal)difference.TotalHours;
                    decimal fuelPrice = int.Parse(distanceTextbox.Text) / 100 * selectedVehicle.avg_fuel_consumption * decimal.Parse(fuelTextbox.Text);
                    incomeTextbox.Text = (cost - (hours * selectedDriver.hourly_rate) - fuelPrice).ToString("F");

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

        public void distanceTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            fuelTextbox.Text = "";
            costTextbox.Text = "";
            incomeTextbox.Text = "";

            int distance;
            if (!int.TryParse(distanceTextbox.Text, out distance))
                distanceTextbox.Text = "";
        }

        public void fuelTextBox_TextChanged(object sender, TextChangedEventArgs e) {


            costTextbox.Text = "";
            incomeTextbox.Text = "";

            decimal fuel;
            if (selectedDriver == null || selectedVehicle == null || !decimal.TryParse(fuelTextbox.Text, out fuel)) {
                fuelTextbox.Text = "";
                return;
            }
        }

        public void intValidation(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void decimalValidation(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^,0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
