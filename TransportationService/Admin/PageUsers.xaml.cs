using System;
using System.Collections.Generic;
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
            db = new ServiceDBEntities();

            InitializeComponent();

            var res = from d in db.Users select d;
            dataGrid.ItemsSource = res.ToList();
        }
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow();
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
            addVehicleTypeWindow.Show();
        }
    }
}
