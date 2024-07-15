using FinancialWPFApp.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace FinancialWPFApp.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public string? Owner { get; set; }
        public string FromTo { get; set; } = null!;
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; } = null!;
        public int? TransactionTypeId { get; set; }
        public int? TransactionStatusId { get; set; }
        public int? WalletId { get; set; }

        public string GetAmount
        {
            get
            {
                decimal amount = Decimal.Parse(Amount.ToString());
                string formattedPrice = amount.ToString("C2", CultureInfo.CreateSpecificCulture("en-US"));
                if (TransactionTypeId == (int) AppConstants.TransactionType.Income)
                {
                    return "+ " + formattedPrice;
                } else
                {
                    return "- " + formattedPrice;
                }
            }
        }
        public string GetValue(int index)
        {
            if (index == (int)AppConstants.TransactionColumn.Id)
            {
                return DisplayId;

            }
            else if (index == (int)AppConstants.TransactionColumn.Client)
            {
                return FromTo;
            }
            else if (index == (int)AppConstants.TransactionColumn.TransactionDate)
            {
                return TransactionDate.ToString("d");
            }
            else if (index == (int)AppConstants.TransactionColumn.Type)
            {
                return TransactionType.TransactionTypeName;
            }


            else if (index == (int)AppConstants.TransactionColumn.Wallet)
            {
                return Wallet.WalletName;

            }
            else if (index == (int)AppConstants.TransactionColumn.Amount)
            {
                return "$" + Amount;
            }
            else if (index == (int)AppConstants.TransactionColumn.Note)
            {
                return Description;
            }
            else
            {
                return TransactionStatus.TransactionStatusName;
            }
        }


        public string DisplayId
        {
            get
            {
                if(TransactionId >= 10)
                {
                    return "T0" + TransactionId;
                } else
                {
                    return "T00" + TransactionId;
                }
            }
        }
        public virtual Account? OwnerNavigation { get; set; }
        public virtual TransactionStatus? TransactionStatus { get; set; }
        public virtual TransactionType? TransactionType { get; set; }
        public virtual Wallet? Wallet { get; set; }
    }
}
