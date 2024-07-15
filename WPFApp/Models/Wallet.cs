using FinancialWPFApp.Constants;
using System;
using System.Collections.Generic;

namespace FinancialWPFApp.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int WalletId { get; set; }
        public string WalletName { get; set; } = null!;
        public double Balance { get; set; }
        public string Description { get; set; } = null!;
        public string? Email { get; set; }

        public virtual Account? EmailNavigation { get; set; }

        public string DisplayWallet
        {
            get
            {
                if (WalletId != 100)
                {
                    return WalletName + " ($" + Balance.ToString("0.00") + ")";
                }
                else
                {
                    return WalletName;
                }
            }
        }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
