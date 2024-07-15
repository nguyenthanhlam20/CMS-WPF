using FinancialWPFApp.UI.User.ViewModels.Pages;
using FinancialWPFApp.UI.User.Views.Windows;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FinancialWPFApp.UI.User.Commands.Pages
{
    public class TransactionCommand
    {

        private TransactionViewModel _viewModel;
        private TransactionDetails w;
        public TransactionCommand(TransactionViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.AddNewTransactionCommand = new ReplayCommand(AddNewTransaction);
            _viewModel.EditTransactionCommand = new ReplayCommand(EditTransaction);
            _viewModel.ViewtTransactionCommand = new ReplayCommand(ViewTransaction);
        }

        private void AddNewTransaction(object parameter)
        {
            bool isExist = false;
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.GetType() == typeof(TransactionDetails))
                {
                    isExist = true;
                }
            }
            if (isExist == false)
            {
                w = new TransactionDetails();
                w.AType = Constants.AppConstants.ActionType.Insert;
                w.SetDataContext();
                w.Show();
             }  else
            {
                w.Activate();
            }
        } 


        private void EditTransaction(object parameter)
        {
            bool isExist = false;
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.GetType() == typeof(TransactionDetails))
                {
                    isExist = true;
                }
            }
            if (isExist == false)
            {
                w = new TransactionDetails();
                w.AType = Constants.AppConstants.ActionType.Edit;
                w.TransactionId = int.Parse(parameter.ToString());
                w.SetDataContext();

                w.Show();
            }
            else
            {
                w.Activate();
            }

        }


        private void ViewTransaction(object parameter)
        {
            bool isExist = false;
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.GetType() == typeof(TransactionDetails))
                {
                    isExist = true;
                }
            }
            if (isExist == false)
            {
                w = new TransactionDetails();
                w.AType = Constants.AppConstants.ActionType.View;
                w.TransactionId = int.Parse(parameter.ToString());
                w.SetDataContext();

                w.Show();
            }
            else
            {
                w.Activate();
            }
        }

    }
}
