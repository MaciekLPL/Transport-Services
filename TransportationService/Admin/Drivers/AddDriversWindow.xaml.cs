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
            if (v.checkIfNull(nameTextBox.Text))
            {
                //MessageBox.Show("Enter driver's name");

                var box = new MsgBox("Enter driver's name");
                box.Show();

                return;
            }

            if (v.checkIfNull(surnameTextBox.Text))
            {
                //MessageBox.Show("Enter driver's surname");

                var box = new MsgBox("Enter driver's surname");
                box.Show();

                return;
            }

            if (birthDatePicker.SelectedDate == null)
            {
                //MessageBox.Show("Enter driver's birth date");

                var box = new MsgBox("Enter driver's birth date");
                box.Show();

                return;
            }

            if (v.checkIfNull(hourlyRateTextBox.Text))
            {
                //MessageBox.Show("Enter driver's hourly rate");

                var box = new MsgBox("Enter driver's hourly rate");
                box.Show();

                return;
            }
            else if (!decimal.TryParse(hourlyRateTextBox.Text, out hourlyRate))
            {
                //MessageBox.Show("Entered driver's hourly rate is invalid!");

                var box = new MsgBox("Entered driver's hourly rate is invalid!");
                box.Show();

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
                //MessageBox.Show("Successfully added a new driver");

                var box = new MsgBox("Successfully added a new driver");
                box.Show();
            }
            else
            {
                String msg = String.Format("Driver {0} already exists", nameTextBox.Text);
                nameTextBox.Text = "";
                surnameTextBox.Text = "";
                hourlyRateTextBox.Text = "";
                birthDatePicker.SelectedDate = null;
                //MessageBox.Show(msg);

                var box = new MsgBox(msg);
                box.Show();
            }
            
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
