﻿using FinancialWPFApp.UI.User.ViewModels;
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

namespace FinancialWPFApp.UI.User.Views
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class UserMainWindowView : Window
    {
        public UserMainWindowView()
        {
            InitializeComponent();
            UserMainWindowViewModel viewModal = new UserMainWindowViewModel();
        }

        private void rdDashboard_Click(object sender, RoutedEventArgs e)
        {
            lbTitle.Content = (sender as RadioButton).Content;
        }

     
    }
}
