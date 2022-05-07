using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace TransportationService {
    /// <summary>
    /// Interaction logic for AddVehicleTypeWindow.xaml
    /// </summary>
    public partial class AddVehicleTypeWindow : Window {

        ServiceDBEntities db;
        public AddVehicleTypeWindow(ServiceDBEntities _db) {

            InitializeComponent();
            db = _db;

        }

        private void submitBtn_Click(object sender, RoutedEventArgs e) {


            if (nameTextBox.Text.Equals("")) {
                MessageBox.Show("Enter vehicle type name");
                return;
            }

            if (nameTextBox.Text.Equals("")) {
                MessageBox.Show("Enter vehicle type max load");
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
