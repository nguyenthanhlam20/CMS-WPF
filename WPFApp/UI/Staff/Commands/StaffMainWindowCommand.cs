using WPFApp.Themes;
using WPFApp.UI.Staff.ViewModels;
using WPFApp.UI.Staff.ViewModels.Pages;
using WPFApp.UI.Public.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WPFApp.UI.Staff.Views.Pages;

namespace WPFApp.UI.Staff.Commands
{
    public class StaffMainWindowCommand
    {
        private StaffMainWindowViewModel _viewModel;
        public StaffMainWindowCommand(StaffMainWindowViewModel viewModel)
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
            var page = parameter.ToString();
            _viewModel.Title = page ?? "";

            if (page == "Dashboard")
                frame.Navigate(new DashboardPage());

            if (page == "Assessments")
                frame.Navigate(new AssessmentPage());

            if (page == "Enrollments")
                frame.Navigate(new EnrollmentPage());

            if (page == "Marks")
                frame.Navigate(new MarkPage());
        }

        private void MaximizeWindow(object parameter)
        {
            Window mainWindow = (Window)parameter;
            if (mainWindow.WindowState == WindowState.Normal)
                mainWindow.WindowState = WindowState.Maximized;
            else
                mainWindow.WindowState = WindowState.Normal;
        }

        private void MinimizeWindow(object parameter)
        {
            Window mainWindow = (Window)parameter;
            mainWindow.WindowState = WindowState.Minimized;
        }

        private void CloseWindow(object parameter) => Application.Current.Shutdown();

        private void SwitchTheme(object parameter)
        {
            ToggleButton tgb = (ToggleButton)parameter;
            if (tgb.IsChecked == true)
                ThemeController.SetTheme(ThemeController.ThemeTypes.Dark);
            else
                ThemeController.SetTheme(ThemeController.ThemeTypes.Light);
        }
    }
}
