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
    /// Interaction logic for PageTransports.xaml
    /// </summary>
    public partial class PageTransports : Page
    {
        ServiceDBEntities db;
        public PageTransports()
        {
            InitializeComponent();

            db = new ServiceDBEntities();
            db.Transports.ToList();
            this.DataContext = db.Transports.Local;
        }

        private void AddTransport_Click(object sender, RoutedEventArgs e)
        {
            //AddDriversWindow addUserWindow = new AddDriversWindow(db);
            //addUserWindow.ShowDialog();
        }

        private void dataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;

            if (row != null)
            {
                var item = row.DataContext as Drivers;

                if (item != null)
                {

                }

            }
        }
    }
}
