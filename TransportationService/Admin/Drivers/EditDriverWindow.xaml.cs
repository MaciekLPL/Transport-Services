using System.Linq;
using System.Windows;

namespace TransportationService {
    /// <summary>
    /// Interaction logic for EditDriverWindow.xaml
    /// </summary>
    public partial class EditDriverWindow : Window {

        ServiceDBEntities db;
        int driverID;
        Drivers driver;
        public EditDriverWindow(ServiceDBEntities _db, int _driverID) {
            InitializeComponent();

            db = _db;
            driverID = _driverID;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            
            driver = db.Drivers.Where(i => i.id == driverID).SingleOrDefault();

            if (driver == null) {
                this.Close();
            } else {

                nameTextBox.Text = driver.name;
                surnameTextBox.Text = driver.surname;
                birthDatePicker.SelectedDate = driver.birth_date;
                hourlyRateTextBox.Text = driver.hourly_rate.ToString();

            }
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e) {
            
            bool wasChanged = updateDriver();

            if (wasChanged) {
                db.SaveChanges();
                this.Close();
                MessageBox.Show("Driver edited successfuly");
            } else {
                MessageBox.Show("No changes made");
            }
        }

        private bool updateDriver() {

            bool changed = false;
            decimal hourlyRate;

            if (nameTextBox.Text != driver.name) {
                driver.name = nameTextBox.Text;
                changed = true;
            }

            if (surnameTextBox.Text != driver.surname) {
                driver.surname = surnameTextBox.Text;
                changed = true;
            }

            if (birthDatePicker.SelectedDate != driver.birth_date)
            {
                driver.birth_date = birthDatePicker.SelectedDate.Value;
                changed = true;
            }

            if (hourlyRateTextBox.Text != driver.hourly_rate.ToString())
            {
                if (decimal.TryParse(hourlyRateTextBox.Text, out hourlyRate))
                {
                    driver.hourly_rate = hourlyRate;
                    changed = true;
                }
            }
            return changed;
        }
    }
}
