using System.Windows;
using WPFApp.UI.Admin.ViewModels.Pages;
using WPFApp.UI.Admin.ViewModels.Windows;
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.Admin.Views.Windows
{
    public partial class DepartmentDetails : Window
    {
        private DepartmentViewModel _viewModel;
        public ActionType AType { get; set; }

        public string Id { get; set; }

        public DepartmentDetails(DepartmentViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }

        public void SetDataContext()
        {
            DataContext = new DepartmentDetailsViewModel(AType, Id, _viewModel);

            if (AType == ActionType.View)
            {
                btnSave.Visibility = Visibility.Collapsed;
                txt1.IsEnabled = false;
                txt2.IsEnabled = false;
            }

            if (AType == ActionType.Edit)
            {
                txt1.IsEnabled = false;
            }
        }
    }
}
