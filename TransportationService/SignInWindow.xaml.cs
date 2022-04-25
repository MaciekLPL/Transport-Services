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

namespace TransportationService {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window {

        ServiceDBEntities db;

        public SignInWindow() {
            InitializeComponent();
            db = new ServiceDBEntities();
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e) {

            string uname = unameTextBox.Text;

            var user = db.Users.Where(i => i.login == uname).SingleOrDefault();

            if (user != null) {
                if (user.password == passwdTextBox.Password) {

                    if(user.type == 0) {
                        DashboardUser dashboardWindow = new DashboardUser(user.id, user.login);
                        dashboardWindow.Show();
                        this.Close();

                    } else if (user.type == 1){
                        DashboardAdmin dashboardWindow = new DashboardAdmin(user.id, user.login);
                        dashboardWindow.Show();
                        this.Close();

                    } else {
                        MessageBox.Show("Your account is suspended");
                        passwdTextBox.Password = "";
                    }

                } else {
                    MessageBox.Show("Invalid password");
                    passwdTextBox.Password = "";
                }

            } else {
                MessageBox.Show("User not found");
                unameTextBox.Text = "";
                passwdTextBox.Password = "";
            }
        }
    }
}
