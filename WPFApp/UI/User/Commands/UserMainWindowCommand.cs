using FinancialWPFApp.Themes;
using FinancialWPFApp.UI.User.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using FinancialWPFApp.UI.User.Views.Pages;
using FinancialWPFApp.Constants;
using FinancialWPFApp.Models;
using FinancialWPFApp.UI.Admin.Views;
using FinancialWPFApp.UI.User.Views;
using FinancialWPFApp.UI.Public.Views.Pages;
using FinancialWPFApp.UI.Public.Views;

namespace FinancialWPFApp.UI.User.Commands
{
    public class UserMainWindowCommand : Window
    {

        private UserMainWindowViewModel _viewModel;
        public UserMainWindowCommand(UserMainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.MaximizeWindowCommand = new ReplayCommand(MaximizeWindow);
            _viewModel.MinimizeWindowCommand = new ReplayCommand(MinimizeWindow);
            _viewModel.SwitchThemeCommand = new ReplayCommand(SwitchTheme);
            _viewModel.CloseWindowCommand = new ReplayCommand(CloseWindow);
            _viewModel.LogoutCommand = new ReplayCommand(Logout);
            _viewModel.OpenPage = new ReplayCommand(OpenPage);
        }

        public void Logout(object parameter)
        {
            Application.Current.MainWindow.Hide();

            MainWindowView window = new MainWindowView();


            Application.Current.MainWindow = window;
            Application.Current.MainWindow.Show();
        }
        public void OpenPage(object parameter)
        {
            Frame frame = (Frame)Application.Current.MainWindow.FindName("frameContent");

            string page = parameter.ToString();


            _viewModel.Title = page;

            if (page == "Dashboard")
            {
                frame.Navigate(new DashboardView());
            }


            if (page == "Transaction")
            {

                //MessageBox.Show("(" + page + "), " + frame.Name + ", " + Application.Current.MainWindow.Name);
                frame.Navigate(new TransactionView());
            }

            if (page == "MyWallet")
            {
                frame.Navigate(new MyWallet());
            }

            if (page == "Notification")
            {
                //frame.Navigate(new Notification());
            }

            if (page == "Report")
            {
                frame.Navigate(new ReportView());
            }

            if (page == "InfoSetting")
            {
                frame.Navigate(new InfoSettingView());
            }

        }

        private void MaximizeWindow(object parameter)
        {
            Window mainWindow = (Window)parameter;
            if (mainWindow.WindowState == WindowState.Normal)
            {
                mainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                mainWindow.WindowState = WindowState.Normal;
            }
        }

        private void MinimizeWindow(object parameter)
        {
            Window mainWindow = (Window)parameter;
            mainWindow.WindowState = WindowState.Minimized;
        }

        private void CloseWindow(object parameter)
        {

            Application.Current.Shutdown();

        }

        private void SwitchTheme(object parameter)
        {
            ToggleButton tgb = (ToggleButton)parameter;

            if (tgb.IsChecked == true)
            {
                ThemeController.SetTheme(ThemeController.ThemeTypes.Dark);
            }
            else
            {
                ThemeController.SetTheme(ThemeController.ThemeTypes.Light);
            }
        }
    }
}
