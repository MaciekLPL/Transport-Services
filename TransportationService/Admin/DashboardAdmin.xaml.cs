﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TransportationService
{
    /// <summary>
    /// Interaction logic for DashboardAdmin.xaml
    /// </summary>
    public partial class DashboardAdmin : Window
    {

        private Button activePanel;
        public DashboardAdmin(int _userID, string _username)
        {
            InitializeComponent();
            usernameText.Text = "@" + _username;
            AdminPanel.Content = new PageUsers();
            activePanel = UsersButton;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
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


        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            if (activePanel != UsersButton)
            {
                activePanel.Style = (Style)Application.Current.Resources["menuButton"];
                activePanel = UsersButton;
                activePanel.Style = (Style)Application.Current.Resources["menuButtonActive"];
                AdminPanel.Content = new PageUsers();
            }
        }
        private void DriversButton_Click(object sender, RoutedEventArgs e)
        {

            if (activePanel != DriversButton)
            {
                activePanel.Style = (Style)Application.Current.Resources["menuButton"];
                activePanel = DriversButton;
                activePanel.Style = (Style)Application.Current.Resources["menuButtonActive"];
                AdminPanel.Content = new PageDrivers();
            }
        }

        private void VehiclesButton_Click(object sender, RoutedEventArgs e) {
            if (activePanel != VehiclesButton) {
                activePanel.Style = (Style)Application.Current.Resources["menuButton"];
                activePanel = VehiclesButton;
                activePanel.Style = (Style)Application.Current.Resources["menuButtonActive"];
                AdminPanel.Content = new PageVehicles();
            }
        }
    }
}
