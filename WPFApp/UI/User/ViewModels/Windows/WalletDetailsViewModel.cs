using FinancialWPFApp.Models;
using FinancialWPFApp.UI.User.Commands.Pages;
using FinancialWPFApp.UI.User.Commands.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static FinancialWPFApp.Constants.AppConstants;

namespace FinancialWPFApp.UI.User.ViewModels.Windows
{
    public class WalletDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }







        public string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }



        public string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }


        public double _balance;

        public double Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                OnPropertyChanged("Balance");
            }
        }

        public string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public ReplayCommand CloseWindowCommand { get; set; }


        public ReplayCommand AddNewWalletCommand { get; set; }
        public ReplayCommand EditWalletCommand { get; set; }

        public ActionType actionType { get; set; }
        public int WalletId { get; set; }
        public WalletDetailsViewModel(ActionType aType, int walletId)
        {
            actionType = aType;
            WalletId = walletId;

            //MessageBox.Show(aType.ToString());

            WalletDetailsCommand commands = new WalletDetailsCommand(this);


            if (aType == ActionType.Insert)
            {
                Title = ("create new wallet").ToUpper();

            }


            if (aType == ActionType.Edit)
            {
                Title = ("edit wallet details").ToUpper();
                GetWalletDetails();
            }

            if (aType == ActionType.View)
            {
                
                Title = ("Wallet details").ToUpper();
                GetWalletDetails();
            }

        }

        public void GetWalletDetails()
        {
            using (var context = new FinancialManagementContext())
            {
                Wallet w = context.Wallets.SingleOrDefault(we => we.WalletId == WalletId);
                Name = w.WalletName;
                Balance = w.Balance;
                Description = w.Description;
            }
        }

    }
}
