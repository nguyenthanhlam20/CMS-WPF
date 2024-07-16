using WPFApp.UI.Public.Commands.Pages;
using System.ComponentModel;

namespace WPFApp.UI.Public.ViewModels.Pages
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public LoginViewModel()
        {
            Email = string.Empty;
            new LoginCommand(this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ReplayCommand? SignInCommand { get; set; }

        private string? _email;

        public string? Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private string? _password;

        public string? Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }


    }
}
