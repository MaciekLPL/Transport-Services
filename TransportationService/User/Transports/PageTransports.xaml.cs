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
        }

        private void AddTransport_Click(object sender, RoutedEventArgs e)
        {
            DashboardUser parentWindow = (DashboardUser)Window.GetWindow(this);
            AddTransportWindow addTransportWindow = new AddTransportWindow(db, parentWindow.userID);
            addTransportWindow.ShowDialog();
        }

        private void dataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            
            if (row != null) {
                var item = row.DataContext as Transports;
                
                if (item != null) {
                    EditTransportWindow editTransportsWindow = new EditTransportWindow(db, item.id, this);
                    editTransportsWindow.ShowDialog();
                }

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            db = new ServiceDBEntities();
            loadDataGrid();
        }

        public void loadDataGrid() {
            db.Transports.ToList();
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = db.Transports.Local;
        }
    }
}
