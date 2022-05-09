using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

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
                newUser.salt = Hash.generateSalt();
                newUser.sha256 = Hash.generateHash(passwdTextBox.Password, newUser.salt);

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
