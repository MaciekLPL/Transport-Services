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
using System.Windows.Shapes;

namespace TransportationService {
    /// <summary>
    /// Interaction logic for DashboardUser.xaml
    /// </summary>
    public partial class DashboardUser : Window {

        ServiceDBEntities db;
        private int userID;
        private string username;

        public DashboardUser(int _userID, string _username) : base() {
            userID = _userID;
            username = _username;
            db = new ServiceDBEntities();
            
            InitializeComponent();
            
            var res = from d in db.Users select d;
            dataGrid.ItemsSource = res.ToList();
            unameLabel.Content = $"Logged as {username}";
        }

    }
}
