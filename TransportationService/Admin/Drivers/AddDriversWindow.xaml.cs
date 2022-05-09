
using System.Windows;


namespace TransportationService
{
    /// <summary>
    /// Interaction logic for AddDriversWindow.xaml
    /// </summary>
    public partial class AddDriversWindow : Window
    {
        ServiceDBEntities db;
        public AddDriversWindow(ServiceDBEntities _db)
        {
            InitializeComponent();
            db = _db;
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            int age = 0;
            decimal hourlyRate;
            if (nameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Enter driver's name");
                return;
            }

            if (surnameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Enter driver's surname");
                return;
            }

            if (ageTextBox.Text.Length == 0)
            {
                MessageBox.Show("Enter driver's age");
                return;
            }
            else if (!int.TryParse(ageTextBox.Text, out age))
            {
                MessageBox.Show("Entered driver's age is invalid!");
                return;
            }

            if (hourlyRateTextBox.Text.Length == 0)
            {
                MessageBox.Show("Enter driver's name");
                return;
            }
            else if (!decimal.TryParse(hourlyRateTextBox.Text, out hourlyRate))
            {
                MessageBox.Show("Entered driver's hourly rate is invalid!");
                return;
            }

            Drivers newDriver = new Drivers();
            newDriver.name = nameTextBox.Text;
            newDriver.surname = surnameTextBox.Text;
            newDriver.age = age;
            newDriver.hourly_rate = hourlyRate;


            db.Drivers.Add(newDriver);
            db.SaveChanges();

            this.Close();
            MessageBox.Show("Successfully added a new driver");
        }
    }

}
