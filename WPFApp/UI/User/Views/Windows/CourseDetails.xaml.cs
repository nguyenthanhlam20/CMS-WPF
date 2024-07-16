using Services;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFApp.Contexts;
using WPFApp.UI.User.ViewModels.Pages;
using WPFApp.UI.User.ViewModels.Windows;
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.User.Views.Windows
{
    public partial class CourseDetails : Window
    {

        private readonly IMarkService _serive;
        private readonly ICourseService _courseService;
        private readonly IAssessmentService _assessmentSerive;
        private readonly IEnrollmentService _enrollmentService;

        private CourseViewModel _viewModel;
        public ActionType AType { get; set; }

        public int Id { get; set; }

        public CourseDetails(CourseViewModel viewModel)
        {
            _viewModel = viewModel;
            _serive = new MarkService();
            _assessmentSerive = new AssessmentService();
            _enrollmentService = new EnrollmentService();
            _courseService = new CourseService();
            DataContext = new CourseDetailsViewModel(AType, Id, _viewModel);

            InitializeComponent();
            AddLabels();
        }

        private async void AddLabels()
        {
            var courses = await _courseService.GetAll();
            var currentCourse = courses.FirstOrDefault(x => x.Id == Id);
            txtTitle.Text = currentCourse?.Title;
            var assessments = await _assessmentSerive.GetAll();
            var marks = await _serive.GetAll();
            var enrollments = await _enrollmentService.GetAll();
            var courseAssessemts = assessments.OrderBy(x => x.Id).Where(x => x.CourseId == Id);

            foreach(var item in courseAssessemts)
            {
                var labelTitle = new Label
                {
                    Content = $"{item.Name} ({item.Percent * 100}%): ",
                    FontWeight = FontWeights.Bold,
                    Foreground = (Brush)FindResource("SecondaryTextColor"),
                    Height = 35,
                    FontSize = (double)FindResource("textFontSize"),
                    VerticalContentAlignment = VerticalAlignment.Center
                };

                var enroll = enrollments.FirstOrDefault( x=> x.StudentId == GlobalVariables.StudentId && x.CourseId == Id );
                var mark = marks.FirstOrDefault(x => x.AssessmentId == item.Id && x.EnrollmentId == enroll.EnrollmentId);

                var labelValue = new Label
                {
                    Content = mark?.Mark1,
                    FontWeight = FontWeights.Bold,
                    Foreground = (Brush)FindResource("SecondaryTextColor"),
                    Height = 35,
                    FontSize = (double)FindResource("textFontSize"),
                    VerticalContentAlignment = VerticalAlignment.Center
                };

                scoreTitle.Children.Add(labelTitle);
                ScoreValue.Children.Add(labelValue);
            }
            lbLoading.Visibility = Visibility.Collapsed;

        }
    }
}
