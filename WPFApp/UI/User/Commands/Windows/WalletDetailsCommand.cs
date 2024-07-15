using FinancialWPFApp.Models;
using FinancialWPFApp.UI.User.ViewModels.Windows;
using FinancialWPFApp.UI.User.Views.Pages;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace FinancialWPFApp.UI.User.Commands.Windows
{
    public class WalletDetailsCommand
    {

        public WalletDetailsViewModel _viewModel;

        public WalletDetailsCommand(WalletDetailsViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.CloseWindowCommand = new ReplayCommand(CloseWindow);
        }


        public void CloseWindow(object parameter)
        {
            Window w = parameter as Window;

            w.Close();
        }



    }
}
