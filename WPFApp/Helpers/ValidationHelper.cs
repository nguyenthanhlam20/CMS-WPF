using FinancialWPFApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinancialWPFApp.Helpers
{
    public static class ValidationHelper
    {
       
        public static bool IsValidEmail(string email)
        {
           if(email != "admin")
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            }
            return true;
        }

        public static bool IsValidPassword(string password)
        {
            if (password != "admin")
            {
                return password.Length >= 6;
            }
            return true;
           
        }

        public static bool IsExistAccount(string email)
        {
            using(var context = new FinancialManagementContext() )
            {
                Account account = context.Accounts.SingleOrDefault(a => a.Email == email);  

                if(account == null)
                {
                    return false;
                }
                return true;    
            }
        }
        public static  bool IsNumber(string text)
        {
            int number;
            return int.TryParse(text, out number);
        }

    }
}
