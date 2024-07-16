using DataAccess.Models;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFApp.UI.Admin.ViewModels.Pages
{
    /// <summary>
    /// Interaction logic for UserListPage.xaml
    /// </summary>
    public partial class UserListPage : System.Windows.Controls.Page
    {

        public int currentPage = 1;

        public int pageSize = 10;

        public int totalPage = 0;

        public int totalRecords = 0;

        public string filterSearch = "";

        public List<Student> accounts = new List<Student>();

        private readonly IDepartmentService _service;

        public void InitializePageSize()
        {
            cbPage.Items.Add(10);
            cbPage.Items.Add(15);
            cbPage.Items.Add(20);
            cbPage.SelectedIndex = 0;
        }

        public UserListPage()
        {
            _service = new StudentService();
            DataContext = this;

            InitializeComponent();
            InitializePageSize();
            InitializePagination();
            LoadAccounts();
        }

        public async void LoadAccounts()
        {
            accounts = await _service.GetAll();
            totalRecords = accounts.Count();
            lbTotal.Content = totalRecords.ToString();

            int from = (currentPage - 1) * pageSize;

            if (currentPage * pageSize >= totalRecords)
            {
                int step = totalRecords - from;
                accounts = accounts.GetRange(from, step);
                lbFromIndex.Content = ((currentPage - 1) * pageSize + 1).ToString();
                lbToIndex.Content = totalRecords;
            }
            else
            {
                accounts = accounts.GetRange(from, pageSize);
                lbFromIndex.Content = ((currentPage - 1) * pageSize + 1).ToString();
                lbToIndex.Content = currentPage * pageSize;
            }
            dgWallet.ItemsSource = accounts;
        }
        public void InitializePagination()
        {
            pageContainer.Children.Clear();
            totalPage = totalRecords % pageSize != 0 ? (totalRecords / pageSize) + 1 : totalRecords / pageSize;
            if (totalRecords == 0)
            {
                bottomContent.Visibility = Visibility.Collapsed;
                dgWallet.Visibility = Visibility.Collapsed;
                lbNoRecords.Visibility = Visibility.Visible;
            }
            else
            {
                bottomContent.Visibility = Visibility.Visible;
                dgWallet.Visibility = Visibility.Visible;
                lbNoRecords.Visibility = Visibility.Collapsed;
            }
            var btn = new System.Windows.Controls.Button();

            //MessageBox.Show(pageSize.ToString());
            for (int i = 1; i <= totalPage; i++)
            {
                btn = new System.Windows.Controls.Button();
                btn.Content = i.ToString();
                btn.Style = System.Windows.Application.Current.Resources["PagingButton"] as System.Windows.Style;

                if (currentPage == i)
                {
                    btn.Background = System.Windows.Application.Current.Resources["ButtonHover"] as Brush;
                    btn.Foreground = System.Windows.Application.Current.Resources["TertiaryWhiteColor"] as Brush;

                }
                btn.Click += Btn_Click;
                pageContainer.Children.Add(btn);

            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            int pageIndex = int.Parse(btn?.Content.ToString() ?? "0");
            //MessageBox.Show("Clic " + pageIndex);

            currentPage = pageIndex;
            Reset();
        }

        private void Reset()
        {
            LoadAccounts();
            InitializePagination();
        }

        private void cbPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb?.Text != "")
            {
                int selectedIndex = cb?.SelectedIndex ?? 0;

                if (selectedIndex == 0)
                    pageSize = 10;

                if (selectedIndex == 1)
                    pageSize = 15;

                if (selectedIndex == 2)
                    pageSize = 20;
            }
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage -= 1;
                InitializePagination();
                Reset();
            }
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPage)
            {
                currentPage += 1;
                Reset();
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text != null)
            {
                filterSearch = txtSearch.Text;
                Reset();
            }
        }
    }
}
