using System;
using System.Collections.Generic;

namespace FinancialWPFApp.Models
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; } = null!;

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
