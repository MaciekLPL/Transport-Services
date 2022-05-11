
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

            if (birthDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Enter driver's birth date");
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
            newDriver.birth_date = birthDatePicker.SelectedDate.Value;
            newDriver.hourly_rate = hourlyRate;


            db.Drivers.Add(newDriver);
            db.SaveChanges();

            this.Close();
            MessageBox.Show("Successfully added a new driver");
        }
    }

}
