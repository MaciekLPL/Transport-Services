using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


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

                    EditDriverWindow editDriverWindow = new EditDriverWindow(db, item.id, this);
                    editDriverWindow.ShowDialog();
                }

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            db = new ServiceDBEntities();
            loadDataGrid();
        }

        public void loadDataGrid() {
            db.Drivers.ToList();
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = db.Drivers.Local;
        }
    }
}
