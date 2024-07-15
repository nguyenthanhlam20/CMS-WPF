using FinancialWPFApp.Models;
using FinancialWPFApp.UI.User.Commands.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace FinancialWPFApp.UI.User.ViewModels.Pages
{
    public class WalletViewModel : INotifyPropertyChanged
    {
        public List<Wallet> _wallets { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<Wallet> Wallets
        {
            get { return _wallets; }
            set
            {
                _wallets = value;
                OnPropertyChanged("Wallets");

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

        public int _totalWallet;

        public int TotalWallet
        {
            get { return _totalWallet; }
            set
            {
                _totalWallet = value;
                OnPropertyChanged("TotalWallet");

            }
        }

        public int PageSize { get; set; }


        public int TotalPage { get; set; }
        public string FilterSearch { get; set; } = "";
        public ReplayCommand AddNewWalletCommand { get; set; }
        public ReplayCommand EditWalletCommand { get; set; }
        public ReplayCommand ViewtWalletCommand { get; set; }

        public void LoadWallets()
        {
            using (var context = new FinancialManagementContext())
            {
                List<Wallet> list = context.Wallets.Where(w => w.Email == "" && w.WalletName.Contains(FilterSearch)).ToList();
                
                int from = (CurrentPage - 1) * PageSize;
               
                if (CurrentPage * PageSize >= TotalWallet)
                {
                    int step = TotalWallet - from;
                    Wallets = list.GetRange(from, step);

                    FromIndex = (CurrentPage - 1) * PageSize + 1;
                    ToIndex = TotalWallet;
                }
                else
                {

                    Wallets = list.GetRange(from, PageSize);
                    FromIndex = (CurrentPage - 1) * PageSize + 1;
                    ToIndex = CurrentPage * PageSize;
                }

            }
        }


        public List<Wallet> GetAll()
        {
            using (var context = new FinancialManagementContext())
            {
                List<Wallet> list = context.Wallets.Where(w => w.Email == "" && w.WalletName.Contains(FilterSearch)).ToList();

                return list;

            }

        }

        public WalletViewModel()
        {

            WalletCommand command = new WalletCommand(this);

            CurrentPage = 1;
            PageSize = 10;

            using (var context = new FinancialManagementContext())
            {
                List<Wallet> wallets = context.Wallets.Where(w => w.Email == "" && w.WalletName.Contains(FilterSearch)).ToList();


                TotalWallet = wallets.Count();


            }

            LoadWallets();
        }

    }
}
