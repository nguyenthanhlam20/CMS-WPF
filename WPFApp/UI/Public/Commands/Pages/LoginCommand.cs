using WPFApp.UI.Admin.Views;
using WPFApp.UI.Public.ViewModels.Pages;
using Services.Authen;
using System;
using System.Windows;

namespace WPFApp.UI.Public.Commands.Pages
{
    public class LoginCommand
    {
        private LoginViewModel _viewModel;

        private readonly IAuthenService _service;


        public LoginCommand(LoginViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.SignInCommand = new ReplayCommand(SignIn);
            _service = new AuthenService();

        }

        private async void SignIn(object parameter)
        {
            if (String.IsNullOrEmpty(_viewModel.Email) || String.IsNullOrEmpty(_viewModel.Password))
            {
                MessageBox.Show("Please enter email and password");
            }
            else
            {
                var success = await _service.Login(_viewModel.Email, _viewModel.Password);

                Application.Current.MainWindow.Hide();
                var window = new AdminMainWindowView();
                Application.Current.MainWindow = window;
                Application.Current.MainWindow.Show();
            }
        }
    }
}
