using WPFApp.Constants;
using WPFApp.Models;
using WPFApp.UI.User.Commands.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace WPFApp.UI.User.ViewModels.Pages
{
    public class TransactionViewModel
    {
        public List<Transaction> _transactions { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<Transaction> Transactions
        {
            get { return _transactions; }
            set
            {
                _transactions = value;
                OnPropertyChanged("Transactions");

            }
        }

        public int _currentPage;

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");

            }
        }
        public int _fromIndex;

        public int FromIndex
        {
            get { return _fromIndex; }
            set
            {
                _fromIndex = value;
                OnPropertyChanged("FromIndex");

            }
        }


        public int _toIndex;

        public int ToIndex
        {
            get { return _toIndex; }
            set
            {
                _toIndex = value;
                OnPropertyChanged("ToIndex");

            }
        }

        public int _totalTransaction;

        public int TotalTransaction
        {
            get { return _totalTransaction; }
            set
            {
                _totalTransaction = value;
                OnPropertyChanged("TotalTransaction");

            }
        }
        public ReplayCommand AddNewTransactionCommand { get; set; }
        public ReplayCommand EditTransactionCommand { get; set; }
        public ReplayCommand ViewtTransactionCommand { get; set; }
        public ReplayCommand OpenFilterCommand { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public string FilterSearch { get; set; } = "";
        public int WalletFilter { get; set; } = 100;
        public int StatusFilter { get; set; } = (int)AppConstants.TransctionStatus.All;
        public int TypeFilter { get; set; } = (int)AppConstants.TransactionType.All;
        public DateTime FromFilter { get; set; }
        public DateTime ToFilter { get; set; } = DateTime.Now;
        public int WalletExport { get; set; } = 100;
        public int StatusExport { get; set; } = (int)AppConstants.TransctionStatus.All;
        public int TypeExport { get; set; } = (int)AppConstants.TransactionType.All;
        public DateTime FromExport { get; set; }
        public DateTime ToExport { get; set; } = DateTime.Now;


        public void LoadTransactions()
        {

            using (var context = new FinancialManagementContext())
            {
                Transactions = GetAllTransaction();
                TotalTransaction = Transactions.Count();

                if (TotalTransaction > 0)
                {

                    int from = (CurrentPage - 1) * PageSize;

                    if (CurrentPage * PageSize >= TotalTransaction)
                    {
                        int step = TotalTransaction - from;
                        Transactions = Transactions.GetRange(from, step);

                        FromIndex = (CurrentPage - 1) * PageSize + 1;
                        ToIndex = TotalTransaction;
                    }
                    else
                    {

                        Transactions = Transactions.GetRange(from, PageSize);
                        FromIndex = (CurrentPage - 1) * PageSize + 1;
                        ToIndex = CurrentPage * PageSize;
                    }
                }

            }
        }




        public TransactionViewModel()
        {

            TransactionCommand command = new TransactionCommand(this);

            CurrentPage = 1;
            PageSize = 10;
            LoadTransactions();
        }


        public List<Transaction> GetAllTransaction()
        {
            using (var context = new FinancialManagementContext())
            {
                List<Transaction> list = context.Transactions.Where(w => w.Owner == ""
                && w.FromTo.Contains(FilterSearch)).Include(t => t.TransactionType)
                .Include(r => r.TransactionStatus).Include(e => e.Wallet).ToList();

                var dd = DateTime.Now;
                if (list.Count > 0)
                {
                    dd = list.Min(o => o.TransactionDate);
                } 
                if (dd != null || dd.ToString() != "")
                {
                    DateTime smallestDate = DateTime.Parse(dd.ToString());
                    if (FromFilter <= DateTime.Parse("01/01/1000"))
                    {
                        FromFilter = smallestDate;
                    }


                    if (FromExport <= DateTime.Parse("01/01/1000"))
                    {
                        FromExport = smallestDate;
                    }

                }

                if (StatusFilter != (int)AppConstants.TransctionStatus.All)
                {
                    list = list.Where(t => t.TransactionStatusId == StatusFilter).ToList();

                }


                if (TypeFilter != (int)AppConstants.TransactionType.All)
                {
                    list = list.Where(t => t.TransactionTypeId == TypeFilter).ToList();
                }

                if (WalletFilter != (int)AppConstants.WalletLimitation)
                {
                    list = list.Where(t => t.WalletId == WalletFilter).ToList();
                }

                list = list.Where(t => t.TransactionDate >= FromFilter && t.TransactionDate <= ToFilter).ToList();


                //list.OrderBy(s => s.TransactionDate).ToList();
                return list;

            }
        }

    }
}
