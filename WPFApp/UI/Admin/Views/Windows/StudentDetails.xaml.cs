using System.Windows;
using WPFApp.UI.Admin.ViewModels.Pages;
using WPFApp.UI.Admin.ViewModels.Windows;
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.Admin.Views.Windows
{
    public partial class StudentDetails : Window
    {
        private StudentViewModel _viewModel;
        public ActionType AType { get; set; }

        public int Id { get; set; }

        public StudentDetails(StudentViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }

        public void SetDataContext()
        {
            DataContext = new StudentDetailsViewModel(AType, Id, _viewModel);

            if (AType == ActionType.View)
            {
                btnSave.Visibility = Visibility.Collapsed;
                txt1.IsEnabled = false;
                txt2.IsEnabled = false;
                txt3.IsEnabled = false;
                txt4.IsEnabled = false;
                txt5.IsEnabled = false;
                txt6.IsEnabled = false;
                txt7.IsEnabled = false;
            }

            txt7.ItemsSource = _viewModel.Departments;
            txt7.DisplayMemberPath = "Name";
            txt7.SelectedValuePath = "Code";
        }
    }
}
