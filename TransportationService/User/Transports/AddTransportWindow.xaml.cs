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
    public partial class AddTransportWindow : TransportWindow {

        int userID;
        Validate v;
        private Users user { get; set; }

        public AddTransportWindow(ServiceDBEntities _db, int _userID) : base(_db) {
            InitializeComponent();

            customerTextbox = customerTextBox;
            driverTextbox = driverTextBox;
            vehicleTextbox = vehicleTextBox;
            weightTextbox = weightTextBox;

            costTextbox = costTextBox;
            incomeTextbox = incomeTextBox;

            startDate = startDatePicker;
            endDate = endDatePicker;

            userID = _userID;
            v = new Validate(_db);
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
            if (!decimal.TryParse(costTextBox.Text, out cost)) {
                MessageBox.Show("Entered cost is invalid!");
                return;
            }
            if (!decimal.TryParse(incomeTextBox.Text, out income)) {
                MessageBox.Show("Entered income is invalid!");
                return;
            }
            if (v.checkIfNull(originTextBox.Text))
            {
                MessageBox.Show("Origin is empty!");
                return;
            }
            if (v.checkIfNull(destinationTextBox.Text))
            {
                MessageBox.Show("Destination is empty!");
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
    }
}
