using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;


namespace TransportationService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {

        ServiceDBEntities db;
        public SignInWindow()
        {
            InitializeComponent();
            db = new ServiceDBEntities();
            db.Users.ToList();
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {

            string uname = unameTextBox.Text;

            var user = db.Users.Where(i => i.login == uname).SingleOrDefault();

            if (user != null)
            {

                bool authenticationToken = Hash.authentication(user.sha256, user.salt, passwdTextBox.Password);
                if (authenticationToken)
                {

                    if (user.type == 0)
                    {
                        DashboardUser dashboardWindow = new DashboardUser(user.id, user.login);
                        dashboardWindow.Show();
                        this.Close();

                    }
                    else if (user.type == 1)
                    {
                        DashboardAdmin dashboardWindow = new DashboardAdmin(user.id, user.login);
                        dashboardWindow.Show();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Your account is suspended");
                        passwdTextBox.Password = "";
                    }

                }
                else
                {
                    MessageBox.Show("Invalid password");
                    passwdTextBox.Password = "";
                }

            }
            else
            {
                MessageBox.Show("User not found");
                unameTextBox.Text = "";
                passwdTextBox.Password = "";
            }
        }
    }
}
