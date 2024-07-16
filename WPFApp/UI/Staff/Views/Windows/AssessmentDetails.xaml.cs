using System.Windows;
using WPFApp.UI.Staff.ViewModels.Pages;
using WPFApp.UI.Staff.ViewModels.Windows;
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.Staff.Views.Windows
{
    public partial class AssessmentDetails : Window
    {
        private AssessmentViewModel _viewModel;
        public ActionType AType { get; set; }

        public int Id { get; set; }

        public AssessmentDetails(AssessmentViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }

        public void SetDataContext()
        {
            DataContext = new AssessmentDetailsViewModel(AType, Id, _viewModel);

            if (AType == ActionType.View)
            {
                btnSave.Visibility = Visibility.Collapsed;
                txt1.IsEnabled = false;
                txt2.IsEnabled = false;
                txt3.IsEnabled = false;
                txt4.IsEnabled = false;
            }
            
            txt4.ItemsSource = _viewModel.Courses;
            txt4.DisplayMemberPath = "Title";
            txt4.SelectedValuePath = "Id";
        }
    }
}
