using WPFApp.UI.Admin.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp.UI.Admin.Views
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

        private void rd_Click(object sender, RoutedEventArgs e)
        {
            lbTitle.Content = (sender as RadioButton).Content;
        }
    }
}
