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

namespace TransportationService
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

        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow(db);
            addUserWindow.ShowDialog();
        }

        private void dataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var row = sender as DataGridRow;

            if (row != null) {
                var item = row.DataContext as Users;

                if (item != null) {
                    EditUserWindow editUserWindow = new EditUserWindow(db, item.id, this);
                    editUserWindow.ShowDialog();
                }

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            db = new ServiceDBEntities();
            loadDataGrid();
        }

        public void loadDataGrid() {
            db.Users.ToList();
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = db.Users.Local;
        }
    }
}
