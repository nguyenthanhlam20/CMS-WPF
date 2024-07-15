using FinancialWPFApp.Models;
using FinancialWPFApp.UI.User.ViewModels.Windows;
using FinancialWPFApp.UI.User.Views.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using FinancialWPFApp.Constants;

namespace FinancialWPFApp.UI.User.Commands.Windows
{
    public class TransactionDetailsCommand
    {


        public TransactionDetailsViewModel _viewModel;

        public TransactionDetailsCommand(TransactionDetailsViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.CloseWindowCommand = new ReplayCommand(CloseWindow);
            if (_viewModel.actionType == Constants.AppConstants.ActionType.Insert)
            {
                _viewModel.AddNewTransactionCommand = new ReplayCommand(AddNew);
            }

            if (_viewModel.actionType == Constants.AppConstants.ActionType.Edit)
            {
                _viewModel.AddNewTransactionCommand = new ReplayCommand(EditTransaction);
            }
        }

        public Transaction GetOldTransaction()
        {
            using (var context = new FinancialManagementContext())
            {
                return context.Transactions.SingleOrDefault(tr => tr.TransactionId == _viewModel.TransactionId);
            }
        }


        public Wallet GetCurrentWallet()
        {
            using (var context = new FinancialManagementContext())
            {
                return context.Wallets.SingleOrDefault(we => we.WalletId == _viewModel.Wallet);
            }
        }



        public void EditTransaction(object parameter)
        {
            if (String.IsNullOrEmpty(_viewModel.FromTo) || String.IsNullOrEmpty(_viewModel.Description))
            {

                MessageBox.Show("Please enter all required fields");

            }
            else
            {


                using (var context = new FinancialManagementContext())
                {

                    int oldStatus, newStatus;

                    Transaction oldTransaction = GetOldTransaction();
                    Wallet currentWallet = GetCurrentWallet();



                    double oldAmount = oldTransaction.Amount;

                    oldStatus = int.Parse(oldTransaction.TransactionStatusId.ToString());
                    newStatus = _viewModel.TStatus;

                    Transaction w = new Transaction()
                    {
                        TransactionId = _viewModel.TransactionId,
                        TransactionDate = _viewModel.TransactionDate,
                        Amount = double.Parse(_viewModel.Amount.ToString(), NumberStyles.Currency),
                        Description = _viewModel.Description,
                        Owner = "",
                        FromTo = _viewModel.FromTo,
                        TransactionTypeId = _viewModel.TType,
                        TransactionStatusId = _viewModel.TStatus,
                        WalletId = _viewModel.Wallet
                    };


                    double differMoney = oldAmount - w.Amount;

                    if (currentWallet.Balance < _viewModel.Amount
                        && newStatus == (int)AppConstants.TransctionStatus.Completed
                        && _viewModel.TType == (int)AppConstants.TransactionType.Expense)
                    {
                        MessageBox.Show($"You dont have enough money in {currentWallet.DisplayWallet}. Please choose another wallets or deposit money to this wallet.");
                    }
                    else
                    {
                        context.Transactions.Update(w);
                        if (context.SaveChanges() > 0)
                        {
                            MessageBox.Show("Update Transaction successfully");
                            Window win = parameter as Window;


                            Frame frame = (Frame)Application.Current.MainWindow.FindName("frameContent");


                            TransactionView mw = frame.Content as TransactionView;


                            if (oldStatus == (int)AppConstants.TransctionStatus.Completed
                                      && newStatus != (int)AppConstants.TransctionStatus.Completed)
                            {

                                if (_viewModel.TType == (int)AppConstants.TransactionType.Income)
                                {
                                    if (differMoney >= 0)
                                    {

                                        currentWallet.Balance -= oldAmount;
                                    }
                                    else
                                    {
                                        currentWallet.Balance -= _viewModel.Amount;

                                    }
                                }
                                else
                                {

                                    if (differMoney >= 0)
                                    {

                                        currentWallet.Balance += oldAmount;
                                    }
                                    else
                                    {
                                        currentWallet.Balance += _viewModel.Amount;

                                    }


                                }

                            }

                            if (oldStatus != (int)AppConstants.TransctionStatus.Completed
                                 && newStatus == (int)AppConstants.TransctionStatus.Completed)
                            {

                                if (_viewModel.TType == (int)AppConstants.TransactionType.Income)
                                {

                                    currentWallet.Balance += _viewModel.Amount;
                                }
                                else
                                {
                                    currentWallet.Balance -= _viewModel.Amount;

                                }
                            }

                            if (oldStatus == (int)AppConstants.TransctionStatus.Completed
                                && newStatus == (int)AppConstants.TransctionStatus.Completed)
                            {

                                if (_viewModel.TType == (int)AppConstants.TransactionType.Income)
                                {

                                    if (differMoney >= 0)
                                    {
                                        currentWallet.Balance -= differMoney;
                                       
                                    }
                                    else
                                    {
                                        currentWallet.Balance += Math.Abs(differMoney);
                                    }
                                }
                                else
                                {

                                    if(differMoney >= 0)
                                    {
                                        currentWallet.Balance += differMoney;
                                    }
                                    else
                                    {
                                        currentWallet.Balance -= Math.Abs(differMoney);
                                    }
                                   

                                }
                            }
                            context.Update(currentWallet);
                            context.SaveChanges();


                            mw.LoadTransactions(false);
                            win.Close();
                        }
                    }
                }
            }
        }


        public void AddNew(object parameter)
        {
            if (String.IsNullOrEmpty(_viewModel.FromTo) || String.IsNullOrEmpty(_viewModel.Description))
            {

                MessageBox.Show("Please enter all required fields");

            }
            else
            {
                using (var context = new FinancialManagementContext())
                {
                    Wallet currentWallet = context.Wallets.SingleOrDefault(we => we.WalletId == _viewModel.Wallet);

                    Transaction w = new Transaction()
                    {
                        TransactionDate = _viewModel.TransactionDate,
                        Amount = double.Parse(_viewModel.Amount.ToString(), NumberStyles.Currency),
                        Description = _viewModel.Description,
                        Owner = "",
                        FromTo = _viewModel.FromTo,
                        TransactionTypeId = _viewModel.TType,
                        TransactionStatusId = _viewModel.TStatus,
                        WalletId = _viewModel.Wallet
                    };
                    if (currentWallet.Balance < _viewModel.Amount
                          && _viewModel.TStatus == (int)AppConstants.TransctionStatus.Completed
                        && _viewModel.TType == (int)AppConstants.TransactionType.Expense)
                    {
                        MessageBox.Show($"You dont have enough money in {currentWallet.DisplayWallet}. Please choose another wallets or deposit money to this wallet.");
                    }
                    else
                    {
                        context.Transactions.Add(w);
                        if (context.SaveChanges() > 0)
                        {
                            MessageBox.Show("Add Transaction successfully");
                            Window win = parameter as Window;


                            Frame frame = (Frame)Application.Current.MainWindow.FindName("frameContent");

                            if (_viewModel.TStatus == (int)AppConstants.TransctionStatus.Completed
                             )
                            {
                                if (_viewModel.TType == (int)AppConstants.TransactionType.Income)
                                {

                                    currentWallet.Balance += _viewModel.Amount;
                                }
                                else
                                {
                                    currentWallet.Balance -= _viewModel.Amount;

                                }
                                context.Update(currentWallet);
                                context.SaveChanges();
                            }

                            TransactionView mw = frame.Content as TransactionView;

                            mw.ResetDefaultValue();
                            mw.LoadTransactions(true);
                            win.Close();
                        }
                    }
                }
            }
        }



        public void CloseWindow(object parameter)
        {
            Window w = parameter as Window;

            w.Close();
        }
    }
}
