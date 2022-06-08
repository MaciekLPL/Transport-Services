using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace TransportationService {
    /// <summary>
    /// Interaction logic for AddVehicleTypeWindow.xaml
    /// </summary>
    public partial class AddVehicleTypeWindow : Window {

        ServiceDBEntities db;
        Validate v;
        public AddVehicleTypeWindow(ServiceDBEntities _db) {

            InitializeComponent();
            v = new Validate(_db);
            db = _db;

        }

        private void submitBtn_Click(object sender, RoutedEventArgs e) {


            if (v.checkIfNull(nameTextBox.Text)) {
                //MessageBox.Show("Enter vehicle type name");
                MsgBox box = new MsgBox("Enter vehicle type name");
                box.Show();

                return;
            }

            if (v.checkIfNull(loadTextBox.Text)) {
                //MessageBox.Show("Enter vehicle type max load");

                MsgBox box = new MsgBox("Enter vehicle type max load");
                box.Show();

                return;
            }


            Vehicle_types newVehicleType = new Vehicle_types();
            newVehicleType.name = nameTextBox.Text;
            newVehicleType.max_load = int.Parse(loadTextBox.Text);
            db.Vehicle_types.Add(newVehicleType);
            db.SaveChanges();

            this.Close();
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
