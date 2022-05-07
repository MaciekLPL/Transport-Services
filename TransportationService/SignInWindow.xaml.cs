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
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {

            string uname = unameTextBox.Text;

            var user = db.Users.Where(i => i.login == uname).SingleOrDefault();

            if (user != null)
            {

                bool authenticationToken = authentication(user.sha256, user.salt);
                if (authenticationToken)
                {

                    if (user.type == 0)
                    {
                        User.DashboardUser dashboardWindow = new User.DashboardUser(user.id, user.login);
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

        bool authentication(string _hashedPassword, string _salt)
        {
            byte[] bytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(passwdTextBox.Password + _salt));
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                sb.Append(bytes[i].ToString("x2"));
            string hashedPassword = sb.ToString();
            if (hashedPassword == _hashedPassword)
                return true;
            return false;
        }
    }
}
