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
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window {

        ServiceDBEntities db;
        int userID;
        Users user;
        Validate v;
        PageUsers parent;
        public EditUserWindow(ServiceDBEntities _db, int _userID, PageUsers _parent) {
            InitializeComponent();
            v = new Validate(_db);
            db = _db;
            userID = _userID;
            parent = _parent;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {

            user = db.Users.Where(i => i.id == userID).SingleOrDefault();

            if (user == null) {
                this.Close();
            }

            else {

                unameTextBox.Text = user.login;
                if (user.type == 0)
                    userRadioBtn.IsChecked = true;
                else if (user.type == 1)
                    adminRadioBtn.IsChecked = true;
                else {
                    userRadioBtn.IsChecked = true;
                    suspendChkBox.IsChecked = true;
                }
            }
        }


        private void submitBtn_Click(object sender, RoutedEventArgs e) {

            bool wasChanged = updateAccount();

            if (wasChanged) {
                db.SaveChanges();
                parent.loadDataGrid();
                this.Close();
                //MessageBox.Show("Account edited successfuly");
                var box = new MsgBox("Account edited successfuly");
                box.Show();
            }
            else {
                //MessageBox.Show("No changes made");
                var box = new MsgBox("No changes made");
                box.Show();
            }
            
        }


        public bool updateAccount() {

            bool changed = false;

            if(user.login != unameTextBox.Text && unameTextBox.Text.Length > 1) {
                user.login = unameTextBox.Text;
                changed = true;
            }

            if (suspendChkBox.IsChecked.Value && user.type < 2) {
                user.type = 2;
                changed = true;
            } 
            else if (!suspendChkBox.IsChecked.Value && user.type > 1) {
                user.type = userRadioBtn.IsChecked.Value ? 0 : 1;
                changed = true;
            } 
            else if (user.type == 0 && adminRadioBtn.IsChecked.Value) {
                user.type = 1;
                changed = true;
            } 
            else if (user.type == 1 && userRadioBtn.IsChecked.Value) {
                user.type = 0;
                changed = true;
            }

            if (!v.checkIfNull(passwdTextBox.Password)) {

                if(passwdRepTextBox.Password == passwdTextBox.Password && v.validatePassword(passwdTextBox.Password)) {
                    user.salt = Hash.generateSalt();
                    user.sha256 = Hash.generateHash(passwdTextBox.Password, user.salt);
                    changed = true;
                }
                else
                {
                    passwdTextBox.Password = "";
                    passwdRepTextBox.Password = "";
                    updateAccount();
                }
            }

            return changed;
        }

        private void QuitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
