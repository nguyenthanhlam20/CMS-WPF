using System.Windows;
using WPFApp.UI.Admin.ViewModels.Pages;
using WPFApp.UI.Admin.ViewModels.Windows;
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.Admin.Views.Windows
{
    public partial class CourseDetails : Window
    {
        private CourseViewModel _viewModel;
        public ActionType AType { get; set; }

        public int Id { get; set; }

        public CourseDetails(CourseViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }

        public void SetDataContext()
        {
            DataContext = new CourseDetailsViewModel(AType, Id, _viewModel);

            if (AType == ActionType.View)
            {
                btnSave.Visibility = Visibility.Collapsed;
                txt1.IsEnabled = false;
                txt2.IsEnabled = false;
                txt3.IsEnabled = false;
            }
        }
    }
}
