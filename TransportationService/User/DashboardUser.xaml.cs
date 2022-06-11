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

namespace TransportationService
{
    /// <summary>
    /// Interaction logic for DashboardUser.xaml
    /// </summary>
    public partial class DashboardUser : Window
    {
        Button activePanel;
        public int userID { get; private set; }
        public DashboardUser(int _userID, string _username)
        {
            InitializeComponent();
            usernameText.Text = "@" + _username;
            userID = _userID;
            UserPanel.Content = new PageTransports();
            activePanel = TransportsButton;
        }

        private void TransportsButton_Click(object sender, RoutedEventArgs e)
        {
            if (activePanel != TransportsButton)
            {
                activePanel.Style = (Style)Application.Current.Resources["menuButton"];
                activePanel = TransportsButton;
                activePanel.Style = (Style)Application.Current.Resources["menuButtonActive"];
                UserPanel.Content = new PageTransports();
            }
        }
        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            if (activePanel != ReportsButton)
            {
                activePanel.Style = (Style)Application.Current.Resources["menuButton"];
                activePanel = ReportsButton;
                activePanel.Style = (Style)Application.Current.Resources["menuButtonActive"];
                UserPanel.Content = new PageReports();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow signInWindow = new SignInWindow();
            signInWindow.Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}