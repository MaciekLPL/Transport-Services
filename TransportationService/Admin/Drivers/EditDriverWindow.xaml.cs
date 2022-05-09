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
                ageTextBox.Text = driver.age.ToString();
                hourlyRateTextBox.Text = driver.hourly_rate.ToString();

            }
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e) {
            
            bool wasChanged = updateDriver();

            if (wasChanged) {
                db.SaveChanges();
                this.Close();
                MessageBox.Show("Vehicle edited successfuly");
            } else {
                MessageBox.Show("No changes made");
            }
        }

        private bool updateDriver() {

            bool changed = false;


            if (nameTextBox.Text != driver.name) {
                driver.name = nameTextBox.Text;
                changed = true;
            }

            if (surnameTextBox.Text != driver.surname) {
                driver.surname = surnameTextBox.Text;
                changed = true;
            }

            //dodać sprawdzanie zmian dla wieku i stawki (rzutowanie)

            return changed;

        }

        
    }
}
