using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using WPFApp.UI.User.Commands;
using WPFApp.UI.User.Views.Pages;

namespace WPFApp.UI.User.ViewModels
{
    public class UserMainWindowViewModel : INotifyPropertyChanged
    {

        [MaybeNull]
        public ReplayCommand MaximizeWindowCommand { get; set; }
        [MaybeNull]
        public ReplayCommand MinimizeWindowCommand { get; set; }
        [MaybeNull]
        public ReplayCommand CloseWindowCommand { get; set; }

        [MaybeNull]
        public ReplayCommand SwitchThemeCommand { get; set; }
        public ReplayCommand LogoutCommand { get; set; }

        public ReplayCommand OpenPage { get; set; }


        public object _currentPage;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public UserMainWindowViewModel()
        {
            UserMainWindowCommand commands = new UserMainWindowCommand(this);
            CurrentPage = new InfoSettingView();
        }
    }
}
