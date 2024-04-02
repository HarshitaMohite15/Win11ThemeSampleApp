﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestingApplication.ViewModel;

namespace TestingApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void clickEvent(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button Clicked");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  this.NavigationService.Navigate(new Uri("Page3.xaml", UriKind.Relative));         
            ButtonWindow btnwindow = new ButtonWindow();
            btnwindow.Owner = this;
            //Added for fixing blank window issue
            Application.Current.MainWindow = btnwindow;
            btnwindow.Show();
        }

        private void calendarButton_Click(object sender, RoutedEventArgs e)
        {          
            CalendarWindow clndrwindow = new CalendarWindow();
            clndrwindow.Owner = this;
            //Added for fixing blank window issue
            Application.Current.MainWindow = clndrwindow;
            clndrwindow.Show();
        }

        private void comboBoxButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxViewModel vm = new ComboBoxViewModel();
            ComboBoxWindow cbw = new ComboBoxWindow(vm);
            cbw.Owner = this;
            cbw.Show();
        }
    }
}