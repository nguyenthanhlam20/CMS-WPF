using FinancialWPFApp.UI.Admin.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace FinancialWPFApp.UI.Admin.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class AdminMainWindowView : Window
    {
        public AdminMainWindowView()
        {
            InitializeComponent();

            DataContext = new AdminMainWindowViewModel();
        }

        private void rdDashboard_Click(object sender, RoutedEventArgs e)
        {
            lbTitle.Content = (sender as RadioButton).Content;
        }
    }
}
