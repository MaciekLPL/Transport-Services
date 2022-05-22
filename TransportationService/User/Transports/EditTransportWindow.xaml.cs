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

namespace TransportationService {
    /// <summary>
    /// Interaction logic for EditTransportWindow.xaml
    /// </summary>
    public partial class EditTransportWindow : TransportWindow {


        public EditTransportWindow(ServiceDBEntities _db) : base(_db) {
            
            InitializeComponent();
            
            customerTextbox = customerTextBox;
            driverTextbox = driverTextBox;
            vehicleTextbox = vehicleTextBox;
            weightTextbox = weightTextBox;

            startDate = startDatePicker;
            endDate = endDatePicker;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            /*
             * TODO
             */
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

            if (false /* TODO: Walidacja */) {
                /*Transports newTransport = new Transports();
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
                MessageBox.Show("Transport added successfuly");*/
            }
        }
    }
}
