using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TransportationService {
    /// <summary>
    /// Interaction logic for EditDriverWindow.xaml
    /// </summary>
    public partial class EditDriverWindow : Window {

        ServiceDBEntities db;
        int driverID;
        Drivers driver;
        bool changed = false;

        public EditDriverWindow(ServiceDBEntities _db, int _driverID) {
            InitializeComponent();

            db = _db;
            driverID = _driverID;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            reloadLicences();

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

        private void reloadLicences() {

            var query =
            from vt in db.Vehicle_types
            join l in db.Licenses.Where(d => d.driver_id == driverID)
                on vt.id equals l.vehicle_type_id into g
            from p in g.DefaultIfEmpty()
            select new {
                Name = vt,
                Owner = p == null ? false : true
            };


            var list = query.ToList();
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = list;

        }

        private void btnRevoke_Click(object sender, EventArgs e) {

            object item = dataGrid.SelectedItem;
            Type type = item.GetType();

            Vehicle_types vt = type.GetProperty("Name").GetValue(item, null) as Vehicle_types;

            if (vt != null) {
                Licenses license = db.Licenses.Where(l => l.vehicle_type_id == vt.id && l.driver_id == driverID).FirstOrDefault();

                if (license != null) {
                    db.Licenses.Remove(license);
                    db.SaveChanges();
                    reloadLicences();
                }
            }
        }

        private void btnGrant_Click(object sender, EventArgs e) {

            object item = dataGrid.SelectedItem;
            Type type = item.GetType();

            Vehicle_types vt = type.GetProperty("Name").GetValue(item, null) as Vehicle_types;

            if (vt != null) {
                Licenses license = db.Licenses.Where(l => l.vehicle_type_id == vt.id && l.driver_id == driverID).FirstOrDefault();

                if (license == null) {

                    Licenses newLicense = new Licenses();
                    newLicense.vehicle_type_id = vt.id;
                    newLicense.driver_id = driverID;
                    db.Licenses.Add(newLicense);
                    db.SaveChanges();
                    reloadLicences();
                }
            }
        }
    }
}
