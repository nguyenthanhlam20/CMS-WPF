using System;
using System.Collections.Generic;

namespace FinancialWPFApp.Models
{
    public partial class Account
    {
        public Account()
        {
            Transactions = new HashSet<Transaction>();
            Wallets = new HashSet<Wallet>();
        }

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool? Gender { get; set; }
        public DateTime Dob { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }


        public string DisplayGender
        {
            get
            {
                if(Gender == true)
                {
                    return "Male";
                } else
                {
                    return "Female";
                }
            }
        }

        public string DisplayStatus
        {
            get
            {
                if (IsActive == true)
                {
                    return "Active";
                }
                else
                {
                    return "Deactive";
                }
            }

        }

        public string DisplayStatusReverse
        {
            get
            {
                if (IsActive == true)
                {
                    return "Deactive";
                }
                else
                {
                    return "Active";
                }
            }
        }

        public string FullName { get; set; } = null!;
        public string? Avatar { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
