using WPFApp.UI.Staff.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp.UI.Staff.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class StaffMainWindowView : Window
    {
        public StaffMainWindowView()
        {
            InitializeComponent();

            DataContext = new StaffMainWindowViewModel();
        }

        private void rd_Click(object sender, RoutedEventArgs e)
        {
            lbTitle.Content = (sender as RadioButton).Content;
        }
    }
}
