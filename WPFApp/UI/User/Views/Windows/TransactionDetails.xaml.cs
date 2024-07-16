using WPFApp.Helpers;
using WPFApp.Models;
using WPFApp.UI.User.ViewModels.Windows;
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
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.User.Views.Windows
{
    /// <summary>
    /// Interaction logic for TransactionDetails.xaml
    /// </summary>
    public partial class TransactionDetails : Window
    {

        private TransactionDetailsViewModel viewModel;
        public ActionType AType { get; set; }

        public int TransactionId { get; set; }
        public TransactionDetails()
        {


            InitializeComponent();
            LoadComboBoxes();


        }


        public void LoadComboBoxes()
        {
            using (var context = new FinancialManagementContext())
            {
                cbStatus.ItemsSource = context.TransactionStatuses.ToList();
                cbStatus.DisplayMemberPath = "TransactionStatusName";
                cbStatus.SelectedValuePath = "TransactionStatusId";


                cbWallet.ItemsSource = context.Wallets.Where(w => w.Email == "").ToList();
                cbWallet.DisplayMemberPath = "DisplayWallet";
                cbWallet.SelectedValuePath = "WalletId";


                cbTransactionType.ItemsSource = context.TransactionTypes.ToList();
                cbTransactionType.DisplayMemberPath = "TransactionTypeName";
                cbTransactionType.SelectedValuePath = "TransactionTypeId";

            }
        }

        public void SetDataContext()
        {
            viewModel = new TransactionDetailsViewModel(AType, TransactionId);
            DataContext = viewModel;
            if (AType == ActionType.View)
            {
                btnSave.Visibility = Visibility.Collapsed;
                txtAmount.IsEnabled = false;
                txtDescription.IsEnabled = false;
                txtFromTo.IsEnabled = false;
                cbStatus.IsEnabled = false;
                cbWallet.IsEnabled = false;
                cbTransactionType.IsEnabled = false;
                dpDate.IsEnabled = false;
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
                txtAmount.Text = "0";
            }
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            DatePicker dp = sender as DatePicker;
            viewModel.TransactionDate = DateTime.Parse(dp.Text);
        }
    }
}
