using System;
using System.Windows;


namespace TransportationService
{
    /// <summary>
    /// Interaction logic for AddDriversWindow.xaml
    /// </summary>
    public partial class AddDriversWindow : Window
    {
        ServiceDBEntities db;
        Validate v;
        public AddDriversWindow(ServiceDBEntities _db)
        {
            InitializeComponent();
            v = new Validate(_db);
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
            if(!v.checkDriverExists(nameTextBox.Text, surnameTextBox.Text, birthDatePicker.SelectedDate.Value))
            {
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
            else
            {
                String msg = String.Format("Driver {0} already exists", nameTextBox.Text);
                nameTextBox.Text = "";
                surnameTextBox.Text = "";
                hourlyRateTextBox.Text = "";
                birthDatePicker.SelectedDate = null;
                MessageBox.Show(msg);
            }
            
        }
    }

}
