using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.Collections;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TransportationService
{
    /// <summary>
    /// Interaction logic for PageReports.xaml
    /// </summary>
    public partial class PageReports : Page
    {
        ServiceDBEntities db;
        ICollectionView view;
        public PageReports()
        {
            InitializeComponent();
        }

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            db = new ServiceDBEntities();
            loadDataGrid();
        }
        public void loadDataGrid()
        {

            var list = db.Transports.ToList();
            view = CollectionViewSource.GetDefaultView(list);
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = view;
            view.Filter = null;
        }
        private void setFilters()
        {
            view.Filter = item =>
            {
                var i = item as Transports;
                if (i != null)
                {
                    bool ret = true;
                    if (!string.IsNullOrWhiteSpace(employeeTextBox.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(i.Users.login) && !i.Users.login.Contains(employeeTextBox.Text))
                            ret = false;
                    }
                    if (activeCheckBox.IsChecked == true)
                    {
                        if (i.Status.name != "active")
                            ret = false;
                    }
                    if (finishedCheckBox.IsChecked == true)
                    {
                        if (i.Status.name != "finished")
                            ret = false;
                    }
                    if (canceledCheckBox.IsChecked == true)
                    {
                        if (i.Status.name != "canceled")
                            ret = false;
                    }
                    if (!string.IsNullOrWhiteSpace(fromTextBox.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(i.origin) && !i.origin.Contains(fromTextBox.Text))
                            ret = false;
                    }
                    if (!string.IsNullOrWhiteSpace(toTextBox.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(i.destination) && !i.destination.Contains(fromTextBox.Text))
                            ret = false;
                    }
                    if (dateStartPicker.SelectedDate != null && dateEndPicker.SelectedDate != null)
                    {
                        if (!(i.start_date >= dateStartPicker.SelectedDate.Value && i.start_date < dateEndPicker.SelectedDate.Value))
                            ret = false;
                    }
                    if (weightMinTextBox.Text != "" && weightMaxTextBox.Text != "")
                    {
                        int weightMin = Int32.Parse(weightMinTextBox.Text);
                        int weightMax = Int32.Parse(weightMaxTextBox.Text);
                        if (!(i.weight >= weightMin && i.weight < weightMax))
                            ret = false;
                    }
                    if (!string.IsNullOrWhiteSpace(customerTextBox.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(i.Customers.name) && !i.Customers.name.Contains(customerTextBox.Text))
                            ret = false;
                    }
                    return ret;
                }
                return false;
            };
        }
        private void employeeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            setFilters();
        }
        private void status_Checked(object sender, RoutedEventArgs e)
        {
            setFilters();
        }

        private void fromTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            setFilters();
        }

        private void toTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            setFilters();
        }

        private void customerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            setFilters();
        }

        private void dateChanged(object sender, SelectionChangedEventArgs e)
        {
            setFilters();
        }

        private void weightFilter(object sender, TextChangedEventArgs e)
        {
            setFilters();
        }
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        public static childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            foreach (childItem child in FindVisualChildren<childItem>(obj))
            {
                return child;
            }

            return null;
        }
        private void ExportToPdf(string path)
        {
            PdfPTable table = new PdfPTable(dataGrid.Columns.Count);
            Document doc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(path, System.IO.FileMode.Create));
            doc.Open();
            doc.Add(new Paragraph("Report"));

            for (int j = 0; j < dataGrid.Columns.Count; j++)
            {
                table.AddCell(new Phrase(dataGrid.Columns[j].Header.ToString()));
            }
            table.HeaderRows = 1;
            IEnumerable itemsSource = dataGrid.ItemsSource;

            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (row != null)
                    {
                        DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                        for (int i = 0; i < dataGrid.Columns.Count; ++i)
                        {
                            System.Windows.Controls.DataGridCell cell = (System.Windows.Controls.DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                            TextBlock txt = cell.Content as TextBlock;
                            if (txt != null)
                            {
                                table.AddCell(new Phrase(txt.Text));
                            }
                        }
                    }
                }

                calculateSummary(table);
                doc.Add(table);
                doc.Close();
            }
        }

        private void GeneratePDFButton_Click(object sender, RoutedEventArgs e) {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                ExportToPdf(saveFileDialog1.FileName);
            }
        }


        private void calculateSummary(PdfPTable table) {

            List<decimal> sums = new List<decimal>(){ 0, 0, 0, 0 };
            int count = dataGrid.Items.Count;

            for (int i = 0; i < dataGrid.Items.Count; i++) {
                dynamic s = dataGrid.Items[i];
                sums[0] += s.distance;
                sums[1] += s.weight;
                sums[2] += s.cost;
                sums[3] += s.income;
            }

            generateEmptyCells(table, 0);                   //One empty row
            generateEmptyCells(table, 5);                   //Empty cells offset
            addSummaryRow(table, "SUM", sums);

            var avgs = sums.Select(i => i / count).ToList();
            generateEmptyCells(table, 5);                   //Empty cells offset
            addSummaryRow(table, "AVG", avgs);

        }


        private void addSummaryRow(PdfPTable table, string title, List<decimal> values) {
            table.AddCell(title);
            foreach (var item in values) {
                table.AddCell(item.ToString("F"));
            }
        }


        private void generateEmptyCells(PdfPTable table, int offset) {
            for (int i = 0; i < dataGrid.Columns.Count - offset; ++i) {
                PdfPCell empty = new PdfPCell(new Phrase(Chunk.NEWLINE));
                empty.Border = Rectangle.NO_BORDER;
                table.AddCell(empty);
            }
        }
    }
}
