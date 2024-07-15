using FinancialWPFApp.Helpers;
using FinancialWPFApp.Models;
using FinancialWPFApp.UI.User.ViewModels.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static FinancialWPFApp.Constants.AppConstants;

namespace FinancialWPFApp.UI.User.Views.Windows
{
    /// <summary>
    /// Interaction logic for WalletDetails.xaml
    /// </summary>
    public partial class WalletDetails : Window
    {




        public ActionType AType { get; set; }

        public int WalletId { get; set; }
        public WalletDetails()
        {


            InitializeComponent();



        }


        public void SetDataContext()
        {
            DataContext = new WalletDetailsViewModel(AType, WalletId);

            if (AType == ActionType.View)
            {
                btnSave.Visibility = Visibility.Collapsed;
                txtBalance.IsEnabled = false;
                txtDescription.IsEnabled = false;
                txtWalletName.IsEnabled = false;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Check if the entered text is a number
            if (!ValidationHelper.IsNumber(e.Text))
            {
                // If not a number, mark the event as handled to prevent the character from being entered
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (String.IsNullOrEmpty(txt.Text))
            {
                txtBalance.Text = "0";
            }
        }
    }
}
