using System.Linq;
using System.Windows;


namespace TransportationService {
    /// <summary>
    /// Interaction logic for DashboardAdmin.xaml
    /// </summary>
    public partial class DashboardAdmin : Window {
        
        ServiceDBEntities db;
        private int userID;
        private string username;

        public DashboardAdmin(int _userID, string _username) {
            userID = _userID;
            username = _username;
            db = new ServiceDBEntities();

            InitializeComponent();

            var res = from d in db.Users select d;
            dataGrid.ItemsSource = res.ToList();
            unameLabel.Content = $"(admin) Logged as {username}";
        }

        private void addUserBtn_Click(object sender, RoutedEventArgs e) {
            AddUserWindow addUserWindow = new AddUserWindow();
            addUserWindow.Show();
        }

        private void addVehicleBtn_Click(object sender, RoutedEventArgs e) {
            AddVehicleWindow addVehicleWindow = new AddVehicleWindow();
            addVehicleWindow.Show();
        }

        private void addVehicleTypeBtn_Click(object sender, RoutedEventArgs e) {
            AddVehicleTypeWindow addVehicleTypeWindow = new AddVehicleTypeWindow();
            addVehicleTypeWindow.Show();
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e) {
            SignInWindow signInWindow = new SignInWindow();
            signInWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddDriversWindow addDriversWindow = new AddDriversWindow();
            addDriversWindow.Show();
        }
    }
}
