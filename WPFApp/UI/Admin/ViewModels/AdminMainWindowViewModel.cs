using WPFApp.UI.Admin.Commands;
using WPFApp.UI.Admin.ViewModels.Pages;
using System.ComponentModel;

namespace WPFApp.UI.Admin.ViewModels
{
    public class AdminMainWindowViewModel : INotifyPropertyChanged
    {
        public ReplayCommand? MaximizeWindowCommand { get; set; }

        public ReplayCommand? MinimizeWindowCommand { get; set; }

        public ReplayCommand? CloseWindowCommand { get; set; }

        public ReplayCommand? SwitchThemeCommand { get; set; }

        public ReplayCommand? LogoutCommand { get; set; }

        public ReplayCommand? OpenPage { get; set; }


        public object _currentPage = new DashboardPage();

        public object CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");

            }
        }

        public string _title = "Dashboard";
        public string Title
        {
            get { return _title; }
            set
            {
                _currentPage = value;
                OnPropertyChanged("Title");

            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AdminMainWindowViewModel()
        {
            _ = new AdminMainWindowCommand(this);
        }
    }
}
