using FinancialWPFApp.Constants;
using FinancialWPFApp.Models;
using FinancialWPFApp.UI.User.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FinancialWPFApp.UI.User.Views.Pages
{
    /// <summary>
    /// Interaction logic for Transaction.xaml
    /// </summary>
    public partial class TransactionView : System.Windows.Controls.Page
    {
        public TransactionViewModel _viewModel { get; set; }



        public TransactionView()
        {
            
            try
            {
                InitializeComponent();

                _viewModel = new TransactionViewModel();
                DataContext = _viewModel;

                InitializePageSize();
                InitializePagination();
            }
            catch (Exception)
            {


            }
        }

        public void ResetDefaultValue()
        {
            _viewModel.ToFilter = DateTime.Now;
            _viewModel.StatusFilter = (int)AppConstants.TransctionStatus.All;
            _viewModel.TypeFilter = (int)AppConstants.TransactionType.All;
            _viewModel.WalletFilter = AppConstants.WalletLimitation;
        }
        public void LoadTransactions(bool isInsert)
        {
            _viewModel.LoadTransactions();

            if (isInsert == true)
            {
                dgTransaction.ItemsSource = _viewModel.GetAllTransaction();
                _viewModel.CurrentPage = 1;
            }
            else
            {
                dgTransaction.ItemsSource = _viewModel.Transactions;

            }
            lbFromIndex.Content = _viewModel.FromIndex;
            lbToIndex.Content = _viewModel.ToIndex;
            lbTotalTransaction.Content = _viewModel.TotalTransaction;

            InitializePagination();
        }

        public void InitializePageSize()
        {
            cbPage.Items.Add(10);
            cbPage.Items.Add(15);
            cbPage.Items.Add(20);
            cbPage.SelectedIndex = 0;
        }

        public void InitializePagination()
        {
            pageContainer.Children.Clear();
            using (var context = new FinancialManagementContext())
            {
                List<Transaction> Transactions = _viewModel.GetAllTransaction();


                int totalRecord = Transactions.Count();


                _viewModel.TotalTransaction = totalRecord;
                int pageSize = _viewModel.PageSize;

                _viewModel.TotalPage = totalRecord % pageSize != 0 ? (totalRecord / pageSize) + 1 : totalRecord / pageSize;


                if (totalRecord == 0)
                {
                    bottomContent.Visibility = Visibility.Collapsed;
                    dgTransaction.Visibility = Visibility.Collapsed;
                    lbNoRecords.Visibility = Visibility.Visible;
                }
                else
                {
                    bottomContent.Visibility = Visibility.Visible;
                    dgTransaction.Visibility = Visibility.Visible;
                    lbNoRecords.Visibility = Visibility.Collapsed;
                }
                System.Windows.Controls.Button btn = new System.Windows.Controls.Button();

                //MessageBox.Show(pageSize.ToString());
                for (int i = 1; i <= _viewModel.TotalPage; i++)
                {
                    btn = new System.Windows.Controls.Button();
                    btn.Content = i.ToString();
                    btn.Style = System.Windows.Application.Current.Resources["PagingButton"] as System.Windows.Style;

                    if (_viewModel.CurrentPage == i)
                    {
                        btn.Background = System.Windows.Application.Current.Resources["ButtonHover"] as Brush;
                        btn.Foreground = System.Windows.Application.Current.Resources["TertiaryWhiteColor"] as Brush;

                    }
                    btn.Click += Btn_Click;
                    pageContainer.Children.Add(btn);

                }
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;

            int pageIndex = int.Parse(btn.Content.ToString());
            //MessageBox.Show("Clic " + pageIndex);

            _viewModel.CurrentPage = pageIndex;
            LoadTransactions(false);

        }

        private void cbPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.Text != "")
            {
                int selectedIndex = cb.SelectedIndex;
                int pageSize = 0;
                if (selectedIndex == 0)
                {
                    pageSize = 10;

                }

                if (selectedIndex == 1)
                {
                    pageSize = 15;

                }

                if (selectedIndex == 2)
                {
                    pageSize = 20;

                }

                //MessageBox.Show(pageSize.ToString());
                _viewModel.PageSize = pageSize;
                _viewModel.CurrentPage = 1;
                LoadTransactions(true);
            }
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CurrentPage > 1)
            {

                _viewModel.CurrentPage -= 1;
                LoadTransactions(false);

            }
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CurrentPage < _viewModel.TotalPage)
            {

                _viewModel.CurrentPage += 1;
                LoadTransactions(false);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text != null)
            {
                _viewModel.FilterSearch = txtSearch.Text;
                LoadTransactions(false);
            }
        }

    }
}
