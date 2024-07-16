using WPFApp.UI.Staff.Commands;
using WPFApp.UI.Staff.ViewModels.Pages;
using System.ComponentModel;

namespace WPFApp.UI.Staff.ViewModels
{
    public class StaffMainWindowViewModel : INotifyPropertyChanged
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

        public StaffMainWindowViewModel()
        {
            _ = new StaffMainWindowCommand(this);
        }
    }
}
