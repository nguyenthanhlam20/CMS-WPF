using System.Windows;
using WPFApp.UI.Admin.ViewModels.Pages;
using WPFApp.UI.Admin.ViewModels.Windows;
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.Admin.Views.Windows
{
    public partial class MarkDetails : Window
    {
        private MarkViewModel _viewModel;
        public ActionType AType { get; set; }

        public int EnrollmentId { get; set; }
        public int AssessmentId { get; set; }

        public MarkDetails(MarkViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }

        public void SetDataContext()
        {
            DataContext = new MarkDetailsViewModel(AType, EnrollmentId, AssessmentId, _viewModel);

            if (AType == ActionType.View)
            {
                btnSave.Visibility = Visibility.Collapsed;
                txt1.IsEnabled = false;
                txt2.IsEnabled = false;
                txt3.IsEnabled = false;
            }

            txt1.ItemsSource = _viewModel.Assessments;
            txt1.DisplayMemberPath = "DisplayName";
            txt1.SelectedValuePath = "Id";

            txt2.ItemsSource = _viewModel.Enrollments;
            txt2.DisplayMemberPath = "DisplayName";
            txt2.SelectedValuePath = "EnrollmentId";
        }
    }
}
