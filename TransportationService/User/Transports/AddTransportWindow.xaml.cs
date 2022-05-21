using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Customers customer { get; set; }
        public Vehicles vehicle { get; set; }
        public Drivers driver { get; set; }

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
                newTransport.Customers = customer;
                newTransport.Vehicles = vehicle;
                newTransport.Drivers = driver;
                newTransport.cost = cost;
                newTransport.income = income;

                db.Transports.Add(newTransport);
                db.SaveChanges();
                this.Close();
                MessageBox.Show("Transport added successfuly");
            }

        }

        private void selectDriver_Click(object sender, RoutedEventArgs e) {
            SelectDriverWindow selectDriverWindow = new SelectDriverWindow(this, db);
            selectDriverWindow.ShowDialog();
        }

        private void selectVehicle_Click(object sender, RoutedEventArgs e) {
            SelectVehicleWindow selectVehicleWindow = new SelectVehicleWindow(this, db);
            selectVehicleWindow.ShowDialog();
        }

        private void selectCustomer_Click(object sender, RoutedEventArgs e) {
            SelectCustomerWindow selectCustomerWindow = new SelectCustomerWindow(this, db);
            selectCustomerWindow.ShowDialog();
        }
    }
}
