using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window {

        ServiceDBEntities db;
        public AddUserWindow(ServiceDBEntities _db) {
            InitializeComponent();
            db = _db;

        }

        private void submitBtn_Click(object sender, RoutedEventArgs e) {


            if (passwdTextBox.Password == passwdRepTextBox.Password) {

                Users newUser = new Users();
                newUser.login = unameTextBox.Text;



                var buff = new byte[32];
                RandomNumberGenerator.Create().GetBytes(buff);
                newUser.salt = Convert.ToBase64String(buff);
                
                byte[] bytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(passwdTextBox.Password + newUser.salt));
                var sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    sb.Append(bytes[i].ToString("x2"));
                newUser.sha256 = sb.ToString();

                if (userRadioBtn.IsChecked == true)
                    newUser.type = 0;
                else
                    newUser.type = 1;


                
                db.Users.Add(newUser);
                db.SaveChanges();

                this.Close();
                MessageBox.Show("Dodano użytkownika");
            }

        }
    }
}
