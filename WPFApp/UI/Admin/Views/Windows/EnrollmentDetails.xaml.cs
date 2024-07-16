using System.Windows;
using WPFApp.UI.Admin.ViewModels.Pages;
using WPFApp.UI.Admin.ViewModels.Windows;
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.Admin.Views.Windows
{
    public partial class EnrollmentDetails : Window
    {
        private EnrollmentViewModel _viewModel;
        public ActionType AType { get; set; }

        public int Id { get; set; }

        public EnrollmentDetails(EnrollmentViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }

        public void SetDataContext()
        {
            DataContext = new EnrollmentDetailsViewModel(AType, Id, _viewModel);

            if (AType == ActionType.View)
            {
                btnSave.Visibility = Visibility.Collapsed;
                txt1.IsEnabled = false;
                txt2.IsEnabled = false;
                txt3.IsEnabled = false;
            }

            txt1.ItemsSource = _viewModel.Students;
            txt1.DisplayMemberPath = "Name";
            txt1.SelectedValuePath = "Id";

            txt2.ItemsSource = _viewModel.Courses;
            txt2.DisplayMemberPath = "Title";
            txt2.SelectedValuePath = "Id";

            txt3.ItemsSource = _viewModel.Semesters;
            txt3.DisplayMemberPath = "Code";
            txt3.SelectedValuePath = "Id";
        }
    }
}
