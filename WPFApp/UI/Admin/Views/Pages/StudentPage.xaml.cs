using Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFApp.UI.Admin.ViewModels.Pages;

namespace WPFApp.UI.Admin.Views.Pages
{
    public partial class StudentPage : System.Windows.Controls.Page
    {
        private StudentViewModel _viewModel;
        public StudentPage()
        {
            InitializeComponent();
            _viewModel = new StudentViewModel();
            DataContext = _viewModel;

            InitializePageSize();
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
            var items = _viewModel.Items;
            int pageSize = _viewModel.PageSize;

            if (_viewModel.TotalItem == 0)
            {
                bottomContent.Visibility = Visibility.Collapsed;
                dgItem.Visibility = Visibility.Collapsed;
                lbNoRecords.Visibility = Visibility.Visible;
            }
            else
            {
                bottomContent.Visibility = Visibility.Visible;
                dgItem.Visibility = Visibility.Visible;
                lbNoRecords.Visibility = Visibility.Collapsed;
            }

            var btn = new Button();
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

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var index = btn?.Content.ToString() ?? "0";
            Reset(page: int.Parse(index));
        }

        private async void Reset(int? page)
        {
            if (page != null) _viewModel.CurrentPage = page.Value;
            InitializePagination();
            await _viewModel.LoadItems();
        }

        private void cbPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb?.Text != "")
            {
                int selectedIndex = cb.SelectedIndex;
                int pageSize = 0;
                if (selectedIndex == 0)
                    pageSize = 10;

                if (selectedIndex == 1)
                    pageSize = 15;

                if (selectedIndex == 2)
                    pageSize = 20;

                //MessageBox.Show(pageSize.ToString());
                _viewModel.PageSize = pageSize;
                Reset(page: 1);
            }
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CurrentPage > 1)
            {
                _viewModel.CurrentPage -= 1;
                Reset(page: _viewModel.CurrentPage);
            }

        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CurrentPage < _viewModel.TotalPage)
            {
                _viewModel.CurrentPage += 1;
                Reset(page: _viewModel.CurrentPage);
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
                Reset(page: null);
            }
        }
    }


}
