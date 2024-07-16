using System.Windows;
using WPFApp.UI.Admin.ViewModels.Pages;
using WPFApp.UI.Admin.ViewModels.Windows;
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.Admin.Views.Windows
{
    public partial class SemesterDetails : Window
    {
        private SemesterViewModel _viewModel;
        public ActionType AType { get; set; }

        public int Id { get; set; }

        public SemesterDetails(SemesterViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }

        public void SetDataContext()
        {
            DataContext = new SemesterDetailsViewModel(AType, Id, _viewModel);

            if (AType == ActionType.View)
            {
                btnSave.Visibility = Visibility.Collapsed;
                txt1.IsEnabled = false;
                txt2.IsEnabled = false;
                txt3.IsEnabled = false;
                txt4.IsEnabled = false;
            }
        }
    }
}
