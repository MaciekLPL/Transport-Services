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

namespace TransportationService
{
    /// <summary>
    /// Interaction logic for PageDrivers.xaml
    /// </summary>
    public partial class PageDrivers : Page
    {

        ServiceDBEntities db;
        public PageDrivers()
        {
            InitializeComponent();

            db = new ServiceDBEntities();
            db.Drivers.ToList();
            this.DataContext = db.Drivers.Local;

        }

        private void AddDriver_Click(object sender, RoutedEventArgs e) {
            AddDriversWindow addUserWindow = new AddDriversWindow(db);
            addUserWindow.ShowDialog();
        }

        private void dataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var row = sender as DataGridRow;

            if (row != null) {
                var item = row.DataContext as Drivers;

                if (item != null) {
                    MessageBox.Show($"{item.id} {item.name} {item.surname}");
                }

            }
        }

    }
}
