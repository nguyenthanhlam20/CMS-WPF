using FinancialWPFApp.Models;
using FinancialWPFApp.UI.User.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinancialWPFApp.UI.User.Views.Pages
{
    /// <summary>
    /// Interaction logic for MyWallet.xaml
    /// </summary>
    public partial class MyWallet : System.Windows.Controls.Page
    {

        private WalletViewModel _viewModel;
        public MyWallet()
        {
            InitializeComponent();

            _viewModel = new WalletViewModel();
            DataContext = _viewModel;

            InitializePageSize();
            InitializePagination();
        }


        public void LoadWallets(bool isInsert)
        {


            _viewModel.LoadWallets();
            dgWallet.ItemsSource = _viewModel.GetAll();


            if(isInsert == true)
            {
                InitializePagination();
            }
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
                List<Wallet> wallets = context.Wallets.Where(w => w.Email == "" && w.WalletName.Contains(_viewModel.FilterSearch)).ToList();


                int totalRecord = wallets.Count();

               
                _viewModel.TotalWallet = totalRecord;
                int pageSize = _viewModel.PageSize;

                _viewModel.TotalPage = totalRecord % pageSize != 0 ? (totalRecord / pageSize) + 1 : totalRecord / pageSize;


                if(totalRecord == 0)
                {
                    bottomContent.Visibility= Visibility.Collapsed;
                    dgWallet.Visibility= Visibility.Collapsed;
                    lbNoRecords.Visibility = Visibility.Visible;
                } else
                {
                    bottomContent.Visibility= Visibility.Visible;
                    dgWallet.Visibility = Visibility.Visible;
                    lbNoRecords.Visibility = Visibility.Collapsed;
                }
                Button btn = new Button();

                //MessageBox.Show(pageSize.ToString());
                for (int i = 1; i <= _viewModel.TotalPage; i++)
                {
                    btn = new Button();
                    btn.Content = i.ToString();
                    btn.Style = Application.Current.Resources["PagingButton"] as Style;

                    if (_viewModel.CurrentPage == i)
                    {
                        btn.Background = Application.Current.Resources["ButtonHover"] as Brush;
                        btn.Foreground = Application.Current.Resources["TertiaryWhiteColor"] as Brush;

                    }
                    btn.Click += Btn_Click;
                    pageContainer.Children.Add(btn);

                }
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            int pageIndex = int.Parse(btn.Content.ToString());
            //MessageBox.Show("Clic " + pageIndex);

            _viewModel.CurrentPage = pageIndex;
            InitializePagination();
            _viewModel.LoadWallets();
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
                InitializePagination();
                _viewModel.LoadWallets();
            }
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CurrentPage > 1)
            {

                _viewModel.CurrentPage -= 1;
                InitializePagination();
                _viewModel.LoadWallets();
            }
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CurrentPage < _viewModel.TotalPage)
            {

                _viewModel.CurrentPage += 1;
                InitializePagination();
                _viewModel.LoadWallets();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text != null)
            {
                _viewModel.FilterSearch = txtSearch.Text;
                InitializePagination();
                _viewModel.LoadWallets();
            }
        }
    }


}
