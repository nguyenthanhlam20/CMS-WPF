using System;
using System.Collections.Generic;

namespace FinancialWPFApp.Models
{
    public partial class TransactionStatus
    {
        public TransactionStatus()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int TransactionStatusId { get; set; }
        public string TransactionStatusName { get; set; } = null!;

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
