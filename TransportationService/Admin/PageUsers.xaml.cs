using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TransportationService.Admin
{
    /// <summary>
    /// Interaction logic for PageUsers.xaml
    /// </summary>
    public partial class PageUsers : Page
    {
        ServiceDBEntities db;
        public PageUsers()
        {

            InitializeComponent();

            db = new ServiceDBEntities();
            db.Users.Load();
            this.DataContext = db.Users.Local;

        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow(db);
            addUserWindow.Show();
        }

        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            AddVehicleWindow addVehicleWindow = new AddVehicleWindow();
            addVehicleWindow.Show();
        }

        private void AddVehicleType_Click(object sender, RoutedEventArgs e)
        {
            AddVehicleTypeWindow addVehicleTypeWindow = new AddVehicleTypeWindow();
            addVehicleTypeWindow.ShowDialog();
        }


        private void dataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var row = sender as DataGridRow;

            if (row != null) {
                var item = row.DataContext as Users;

                if (item != null) {
                    MessageBox.Show($"{item.id} {item.login} {item.password}");
                }

            }
        }
    }
}
