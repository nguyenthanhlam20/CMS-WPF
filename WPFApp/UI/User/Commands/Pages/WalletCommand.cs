using FinancialWPFApp.UI.User.ViewModels.Pages;
using FinancialWPFApp.UI.User.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FinancialWPFApp.UI.User.Commands.Pages
{

    public class WalletCommand
    {

        private WalletViewModel _viewModel;
        private WalletDetails w;
        public WalletCommand(WalletViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.AddNewWalletCommand = new ReplayCommand(AddNewWallet);
            _viewModel.EditWalletCommand = new ReplayCommand(EditWallet);
            _viewModel.ViewtWalletCommand = new ReplayCommand(ViewWallet);
        }

        private void AddNewWallet(object parameter)
        {
            bool isExist = false;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(WalletDetails))
                {
                    isExist = true;
                }
            }
            if (isExist == false)
            {
                w = new WalletDetails();
                w.AType = Constants.AppConstants.ActionType.Insert;
                w.SetDataContext();
                w.Show();
            }
        }


        private void EditWallet(object parameter)
        {
            bool isExist = false;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(WalletDetails))
                {
                    isExist = true;
                }
            }
            if (isExist == false)
            {
                w = new WalletDetails();
                w.AType = Constants.AppConstants.ActionType.Edit;
                w.WalletId = int.Parse(parameter.ToString());
                w.SetDataContext();

                w.Show();
            }
        }


        private void ViewWallet(object parameter)
        {
            bool isExist = false;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(WalletDetails))
                {
                    isExist = true;
                }
            }
            if (isExist == false)
            {
                w = new WalletDetails();
                w.AType = Constants.AppConstants.ActionType.View;
                w.WalletId = int.Parse(parameter.ToString());
                w.SetDataContext();

                w.Show();
            }
        }

    }

}
