using FinancialWPFApp.UI.Public.ViewModels;
using System.Windows;

namespace FinancialWPFApp.UI.Public.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
