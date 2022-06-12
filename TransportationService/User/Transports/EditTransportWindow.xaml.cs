using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TransportationService {
    /// <summary>
    /// Interaction logic for EditTransportWindow.xaml
    /// </summary>
    public partial class EditTransportWindow : TransportWindow {

        int transportID;
        bool changed;
        Validate v;
        private RadioButton checkedRadioButton;
        PageTransports parent;

        public EditTransportWindow(ServiceDBEntities _db, int _transportID, PageTransports _parent) : base(_db) {

            InitializeComponent();

            customerTextbox = customerTextBox;
            driverTextbox = driverTextBox;
            vehicleTextbox = vehicleTextBox;
            weightTextbox = weightTextBox;
            
            distanceTextbox = distanceTextBox;
            fuelTextbox = fuelTextBox;
            costTextbox = costTextBox;
            incomeTextbox = incomeTextBox;

            startDate = startDatePicker;
            endDate = endDatePicker;

            transportID = _transportID;
            parent = _parent;
            v = new Validate(_db);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            transport = db.Transports.Where(t => t.id == transportID).FirstOrDefault();

            if (transport == null) {
                //MessageBox.Show("Unknown error occured");
                var box = new MsgBox("Unknown error occured");
                box.Show();
                this.Close();
            }

            setValues();
            setRadioButton();
            
        }

        private void setValues() {
            
            originTextBox.Text = transport.origin;
            destinationTextBox.Text = transport.destination;
            distanceTextBox.Text = transport.distance.ToString();
            weightTextBox.Text = transport.weight.ToString();
            startDatePicker.SelectedDate = transport.start_date;
            endDatePicker.SelectedDate = transport.end_date;
            userTextBox.Text = transport.Users.login;

            selectedVehicle = transport.Vehicles;
            vehicleTextBox.Text = $"{selectedVehicle.registration} {selectedVehicle.Vehicle_types.name}";

            selectedDriver = transport.Drivers;
            driverTextBox.Text = $"{selectedDriver.name} {selectedDriver.surname}";

            selectedCustomer = transport.Customers;
            customerTextBox.Text = $"{selectedCustomer.name}";

            skipCalculation = true;
            costTextBox.Text = transport.cost.ToString();
            incomeTextBox.Text = transport.income.ToString();
            skipCalculation = false;
        }

        private void setRadioButton() {
            switch (transport.Status.name.ToLower()) {
                case "active":
                    activeRadio.IsChecked = true;
                    break;
                case "finished":
                    finishedRadio.IsChecked = true;
                    disableAll();
                    break;
                case "canceled":
                    canceledRadio.IsChecked = true;
                    disableAll();
                    break;
                default:
                    this.Close();
                    //MessageBox.Show("Unknown error occured");
                    var box = new MsgBox("Unknown error occured");
                    box.Show();
                    break;
            }
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e) {

            checkStringTextBoxes();
            checkNumericTextBoxes();
            checkDatePickers();
            checkSelectors();
            checkRadioButtons();

            if (changed) {
                db.SaveChanges();
                parent.loadDataGrid();
                this.Close();
                //MessageBox.Show("Transport edited successfuly");
                var box = new MsgBox("Transport edited successfuly");
                box.Show();
            } else {
                //MessageBox.Show("No changes made");
                var box = new MsgBox("No changes made");
                box.Show();
            }
        }

        private void checkStringTextBoxes() {
            //Origin
            if (!v.checkIfNull(originTextBox.Text) && originTextBox.Text != transport.origin) {
                transport.origin = originTextBox.Text;
                changed = true;
            }
            //Destination
            if (!v.checkIfNull(destinationTextBox.Text) && destinationTextBox.Text != transport.destination) {
                transport.destination = destinationTextBox.Text;
                changed = true;
            }
        }

        private void checkNumericTextBoxes() {
            int distance;
            int weight;
            decimal cost;
            decimal income;

            //Distance
            if (!v.checkIfNull(distanceTextBox.Text) && distanceTextBox.Text != transport.distance.ToString()) {
                if (int.TryParse(distanceTextBox.Text, out distance)) {
                    transport.distance = distance;
                    changed = true;
                }
            }
            //Weight
            if (!v.checkIfNull(weightTextBox.Text) && weightTextBox.Text != transport.weight.ToString()) {
                if (int.TryParse(weightTextBox.Text, out weight)) {
                    transport.weight = weight;
                    changed = true;
                }
            }
            //Cost
            if (!v.checkIfNull(costTextBox.Text) && costTextBox.Text != transport.cost.ToString()) {
                if (decimal.TryParse(costTextBox.Text, out cost)) {
                    transport.cost = cost;
                    changed = true;
                }
            }
            //Income
            if (!v.checkIfNull(incomeTextBox.Text) && incomeTextBox.Text != transport.income.ToString()) {
                if (decimal.TryParse(incomeTextBox.Text, out income)) {
                    transport.income = income;
                    changed = true;
                }
            }
        }

        private void checkDatePickers() {

            if (startDatePicker.SelectedDate != null && endDatePicker.SelectedDate != null) {
                
                if (startDatePicker.SelectedDate.Value != transport.start_date) {
                    transport.start_date = startDatePicker.SelectedDate.Value;
                    changed = true;
                }
                if (endDatePicker.SelectedDate.Value != transport.end_date) {
                    transport.end_date = endDatePicker.SelectedDate.Value;
                    changed = true;
                }
            }
        }

        private void checkSelectors() {
            //Vehicle
            if (selectedVehicle != null && selectedVehicle.id != transport.vehicle_id) {
                transport.Vehicles = selectedVehicle;
                changed = true;
            }
            //Driver
            if (selectedDriver != null && selectedDriver.id != transport.driver_id) {
                transport.Drivers = selectedDriver;
                changed = true;
            }
            //Customer
            if (selectedCustomer != null && selectedCustomer.id != transport.customer_id) {
                transport.Customers = selectedCustomer;
                changed = true;
            }
        }

        private void checkRadioButtons() {

            if(finishedRadio.IsChecked.Value || canceledRadio.IsChecked.Value) {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure? This action will lock the transport.", "Lock transport confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult != MessageBoxResult.Yes) {
                    return;
                }
            }
            string statusContent = checkedRadioButton.Content.ToString().ToLower();

            if (transport.Status.name != statusContent) {
                Status newStatus = db.Status.Where(s => s.name == statusContent).SingleOrDefault();
                if (newStatus != null) {
                    transport.Status = newStatus;
                    changed = true;
                } else {
                    //MessageBox.Show("Transport status error");
                    var box = new MsgBox("Transport status error");
                    box.Show();

                }
            }
        }

        private void radio_Checked(object sender, RoutedEventArgs e) {

            RadioButton ck = sender as RadioButton;
            if (ck.IsChecked.Value)
                checkedRadioButton = ck;
        }

        private void disableAll() {

            panelLeft.IsEnabled = false;
            panelMiddle.IsEnabled = false;
            panelRight.IsEnabled = false;
            submitBtn.IsEnabled = false;

        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
