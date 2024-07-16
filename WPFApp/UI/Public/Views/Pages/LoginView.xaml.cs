using WPFApp.UI.Public.ViewModels.Pages;

namespace WPFApp.UI.Public.Views.Pages
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : System.Windows.Controls.Page
    {
        private LoginViewModel _viewModel;

        public LoginView()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            DataContext = _viewModel;
        }
    }
}
