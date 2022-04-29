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

namespace TransportationService.User
{
    /// <summary>
    /// Interaction logic for DashboardUser.xaml
    /// </summary>
    public partial class DashboardUser : Window
    {
        Button activePanel;
        public DashboardUser(int _userID, string _username)
        {
            InitializeComponent();
            usernameText.Text = "@" + _username;
            //erPanelFrame.Content = new ap_userPage();
            activePanel = B1;
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            //UserPanelFrame.Content = ;
            if (activePanel != B1)
            {
                activePanel.Style = (Style)Application.Current.Resources["menuButton"];
                activePanel = B1;
                activePanel.Style = (Style)Application.Current.Resources["menuButtonActive"];
            }
        }
        private void B2_Click(object sender, RoutedEventArgs e)
        {
            //UserPanelFrame.Content = ;
            if (activePanel != B2)
            {
                activePanel.Style = (Style)Application.Current.Resources["menuButton"];
                activePanel = B2;
                activePanel.Style = (Style)Application.Current.Resources["menuButtonActive"];
            }
        }
        private void B3_Click(object sender, RoutedEventArgs e)
        {
            //UserPanelFrame.Content = ;
            if (activePanel != B3)
            {
                activePanel.Style = (Style)Application.Current.Resources["menuButton"];
                activePanel = B3;
                activePanel.Style = (Style)Application.Current.Resources["menuButtonActive"];
            }
        }
        private void B4_Click(object sender, RoutedEventArgs e)
        {
            //UserPanelFrame.Content = ;
            if (activePanel != B4)
            {
                activePanel.Style = (Style)Application.Current.Resources["menuButton"];
                activePanel = B4;
                activePanel.Style = (Style)Application.Current.Resources["menuButtonActive"];
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
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}