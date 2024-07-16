using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Constants
{
    public class AppConstants
    {
        public AppConstants()
        {
        }

        public enum Roles
        {
            Admin = 1,
            User = 2
        }

        public enum Pages
        {
            SignIn,
            SignUp
        }

        public enum ActionType
        {
            View = 0,
            Edit = 1,
            Insert = 2,
        }

        public enum TransctionStatus
        {
            Completed = 1,
            Pending = 2,
            Cancel = 3,
            Draft = 4,
            All = 100,
        }

        public enum TransactionType
        {
            Income = 1,
            Expense = 2,
            All = 100,
        }


        public enum TransactionColumn
        {
            Id = 0,
            Client = 1,
            TransactionDate = 2,
            Type = 3,
            Wallet = 4,
            Amount = 5,
            Note = 6,
            Status = 7,
        }


        public const int WalletLimitation = 100;

    }
}
