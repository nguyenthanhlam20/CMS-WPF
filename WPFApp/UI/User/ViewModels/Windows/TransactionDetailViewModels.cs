using WPFApp.Models;
using WPFApp.UI.User.Commands.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.User.ViewModels.Windows
{
    public class TransactionDetailsViewModel : INotifyPropertyChanged
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



        public int _wallet;

        public int Wallet
        {
            get { return _wallet; }
            set
            {
                _wallet = value;
                OnPropertyChanged("Wallet");
            }
        }


        public int _tType;

        public int TType
        {
            get { return _tType; }
            set
            {
                _tType = value;
                OnPropertyChanged("TType");
            }
        }


        public int _tStatus;

        public int TStatus
        {
            get { return _tStatus; }
            set
            {
                _tStatus = value;
                OnPropertyChanged("TStatus");
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


        public string _fromTo;

        public string FromTo
        {
            get { return _fromTo; }
            set
            {
                _fromTo = value;
                OnPropertyChanged("FromTo");
            }
        }


        public DateTime _transactionDate;

        public DateTime TransactionDate
        {
            get { return _transactionDate; }
            set
            {
                _transactionDate = value;
                OnPropertyChanged("TransactionDate");
            }
        }



        public double _amount;

        public double Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }



        public ReplayCommand CloseWindowCommand { get; set; }


        public ReplayCommand AddNewTransactionCommand { get; set; }
        public ReplayCommand EditTransactionCommand { get; set; }

        public ActionType actionType { get; set; }
        public int TransactionId { get; set; }



        public void GetDefaultValue()
        {
            TransactionDate = DateTime.Now;
            using (var context = new FinancialManagementContext())
            {
                List<Wallet> wallets = context.Wallets.Where(w => w.Email == "").ToList();

                if (wallets.Count() > 0)
                {
                    Wallet = wallets.ElementAt(0).WalletId;
                }
                else
                {
                    MessageBox.Show("Please add wallet first before create any transaction");
                    return;
                }
                TStatus = context.TransactionStatuses.ToList().ElementAt(0).TransactionStatusId;
                TType = context.TransactionTypes.ToList().ElementAt(0).TransactionTypeId;
            }
        }

        public TransactionDetailsViewModel(ActionType aType, int transactionId)
        {
            actionType = aType;
            TransactionId = transactionId;

            //MessageBox.Show(aType.ToString());

            TransactionDetailsCommand commands = new TransactionDetailsCommand(this);
            GetDefaultValue();

            if (aType == ActionType.Insert)
            {
                Title = ("create new Transaction").ToUpper();

            }


            if (aType == ActionType.Edit)
            {
                Title = ("edit Transaction" + " #" + transactionId).ToUpper();
                GetTransactionDetails();
            }

            if (aType == ActionType.View)
            {

                Title = ($"view transaction #{transactionId}").ToUpper();
                GetTransactionDetails();
            }

        }

        public void GetTransactionDetails()
        {
            using (var context = new FinancialManagementContext())
            {
                Transaction w = context.Transactions.SingleOrDefault(we => we.TransactionId == TransactionId);
                Wallet = int.Parse(w.WalletId.ToString());
                TType = int.Parse(w.TransactionTypeId.ToString());
                TStatus = int.Parse(w.TransactionStatusId.ToString());
                Amount = double.Parse(w.Amount.ToString());
                Description = w.Description;
                FromTo = w.FromTo;
                TransactionDate = DateTime.Parse(w.TransactionDate.ToString());

            }
        }
    }
}
