using WPFApp.UI.Admin.Views;
using WPFApp.UI.Public.ViewModels.Pages;
using Services.Authen;
using System;
using System.Windows;
using WPFApp.UI.User.Commands;
using WPFApp.UI.User.Views;
using WPFApp.UI.Staff.Views;
using WPFApp.Contexts;

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
                var email = _viewModel.Email;
                var password = _viewModel.Password;
                
                dynamic window = new AdminMainWindowView();

                if (email == "Admin" && password == "@admin123")
                {
                    window = new AdminMainWindowView();
                } else if(email == "Staff" && password == "@staff123")
                {
                    window = new StaffMainWindowView();
                }
                else
                {
                    var success = await _service.Login(_viewModel.Email, _viewModel.Password);
                    
                    if(!success)
                    {
                        MessageBox.Show("Incorrect email or password");
                        return;
                    }

                    GlobalVariables.Username = email;
                    window = new UserMainWindowView();
                }

                Application.Current.MainWindow.Hide();
                Application.Current.MainWindow = window;
                Application.Current.MainWindow.Show();
            }
        }
    }
}
