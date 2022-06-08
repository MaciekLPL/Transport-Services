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
        Validate v;
        public AddUserWindow(ServiceDBEntities _db) {
            InitializeComponent();
            v = new Validate(_db);
            db = _db;

        }

        private void submitBtn_Click(object sender, RoutedEventArgs e) {

            if(!v.checkUserExists(unameTextBox.Text))
            {
                if(!(v.checkIfNull(unameTextBox.Text) && v.checkIfNull(passwdTextBox.Password)))
                {
                    if (passwdTextBox.Password == passwdRepTextBox.Password && v.validatePassword(passwdTextBox.Password))
                    {
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
                        //MessageBox.Show("User added successfully");

                        var box = new MsgBox("User added successfully");
                        box.Show();
                    }
                    else
                    {
                        passwdTextBox.Password = "";
                        passwdRepTextBox.Password = "";
                    }
                }
                else
                {
                    //MessageBox.Show("Enter all user data");
                    var box = new MsgBox("Enter all user data");
                    box.Show();
                }
                
            }
            else
            {
                String msg = String.Format("User {0} already exists", unameTextBox.Text);
                unameTextBox.Text = "";
                passwdTextBox.Password = "";
                passwdRepTextBox.Password = "";
                //MessageBox.Show(msg);
                var box = new MsgBox(msg);
                box.Show();
            }
           


            

        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
