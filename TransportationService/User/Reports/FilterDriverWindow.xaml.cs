using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace TransportationService
{
    /// <summary>
    /// Interaction logic for FilterDriverWindow.xaml
    /// </summary>
    public partial class FilterDriverWindow : Window
    {
        ServiceDBEntities db;
        ICollectionView view;
        PageReports parent;
        public FilterDriverWindow(PageReports parent, ServiceDBEntities _db)
        {
            db = _db;
            this.parent = parent;
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            IQueryable<Transports> query;
            IQueryable<Drivers> queryDrivers;



            query = from d in db.Transports select d;
            queryDrivers = from d in db.Drivers select d;
            var list = query.ToList();
            var list2 = queryDrivers.ToList();
            List<Drivers> driversLists = new List<Drivers>();
            foreach( var elem in list2)
            {
                for(int i=0;i<list.Count;i++)
                {
                    if(elem.id==list[i].Drivers.id)
                    {
                        driversLists.Add(elem);
                        break;
                    }
                }
            }
            view = CollectionViewSource.GetDefaultView(driversLists);
            dataGrid.ItemsSource = view;
        }

        private void dataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;

            if (row != null)
            {
                var item = row.DataContext as Drivers;

                if (item != null)
                {

                    parent.selectedDriver = item;
                    this.Close();

                }
            }
        }

        private void filterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            string filter = filterTextBox.Text;
            if (string.IsNullOrEmpty(filter))
                view.Filter = null;
            else
                view.Filter = item =>
                {
                    Drivers i = item as Drivers;
                    if (i != null)
                    {
                        if (!string.IsNullOrWhiteSpace(i.name) && i.name.Contains(filter) || !string.IsNullOrWhiteSpace(i.surname) && i.surname.Contains(filter))
                            return true;
                    }
                    return false;
                };
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
