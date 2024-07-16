using DataAccess.Models;
using Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFApp.UI.Staff.Views.Windows;

namespace WPFApp.UI.Staff.ViewModels.Pages
{
    public class EnrollmentViewModel : INotifyPropertyChanged
    {
        private readonly IEnrollmentService _service;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly ISemesterService _semesterService;
        private EnrollmentDetails w;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) =>
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public List<Student> _students { get; set; }

        public List<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged("Students");

            }
        }

        public List<Course> _courses { get; set; }
        public List<Course> Courses
        {
            get { return _courses; }
            set
            {
                _courses = value;
                OnPropertyChanged("Course");

            }
        }

        public List<Semester> _semesters { get; set; }
        public List<Semester> Semesters
        {
            get { return _semesters; }
            set
            {
                _semesters = value;
                OnPropertyChanged("Semesters");
            }
        }


        public List<Enrollment> _items { get; set; }
        public List<Enrollment> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged("Items");

            }
        }

        public int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        public int _fromIndex;
        public int FromIndex
        {
            get { return _fromIndex; }
            set
            {
                _fromIndex = value;
                OnPropertyChanged("FromIndex");

            }
        }

        public int _toIndex;
        public int ToIndex
        {
            get { return _toIndex; }
            set
            {
                _toIndex = value;
                OnPropertyChanged("ToIndex");

            }
        }

        public int _totalItem;
        public int TotalItem
        {
            get { return _totalItem; }
            set
            {
                _totalItem = value;
                OnPropertyChanged("TotalItem");

            }
        }

        public int PageSize { get; set; } = 10;
        public int TotalPage { get; set; } = 0;
        public string FilterSearch { get; set; } = "";
        public ReplayCommand AddCommand { get; set; }
        public ReplayCommand EditCommand { get; set; }
        public ReplayCommand ViewCommand { get; set; }

        public async Task LoadItems()
        {
            var items = await _service.GetAll();
            var list = items.Where(x => (x.Student?.Name ?? "").Contains(FilterSearch)).ToList();

            TotalItem = list.Count;
            int from = (CurrentPage - 1) * PageSize;

            if (CurrentPage * PageSize >= TotalItem)
            {
                int step = TotalItem - from;
                Items = list.GetRange(from, step);

                FromIndex = (CurrentPage - 1) * PageSize + 1;
                ToIndex = TotalItem;
            }
            else
            {
                Items = list.GetRange(from, PageSize);
                FromIndex = (CurrentPage - 1) * PageSize + 1;
                ToIndex = CurrentPage * PageSize;
            }

            TotalPage = TotalItem % PageSize != 0 ? (TotalItem / PageSize) + 1 : TotalItem / PageSize;

        }


        public EnrollmentViewModel()
        {
            _service = new EnrollmentService();
            _courseService = new CourseService();
            _studentService = new StudentService();
            _semesterService = new SemesterService();
            SetCommands();
            Task.Run(() => LoadItems()).Wait();
            Task.Run(() => LoadStudents()).Wait();
            Task.Run(() => LoadSemesters()).Wait();
            Task.Run(() => LoadCourses()).Wait();
        }

        public async Task LoadStudents()
        {
             Students = await _studentService.GetAll();
        }

        public async Task LoadSemesters()
        {
            Semesters = await _semesterService.GetAll();
        }

        public async Task LoadCourses()
        {
            Courses = await _courseService.GetAll();
        }

        private void SetCommands()
        {
            AddCommand = new ReplayCommand(Add);
            EditCommand = new ReplayCommand(Edit);
            ViewCommand = new ReplayCommand(View);
        }

        private void Add(object parameter)
        {
            bool isExist = false;

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(EnrollmentDetails)) isExist = true;
            }
            if (isExist == false) ShowDetails(Constants.AppConstants.ActionType.Insert, null);
        }

        private void Edit(object parameter)
        {
            bool isExist = false;
            var para = parameter.ToString() ?? "0";
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(EnrollmentDetails)) isExist = true;
            }
            if (!isExist) ShowDetails(Constants.AppConstants.ActionType.Edit, para);
        }

        public void ShowDetails(Constants.AppConstants.ActionType type, string? para)
        {
            w = new(this);
            w.AType = type;
            if (para != null)
                w.Id = int.Parse(para);
            w.SetDataContext();
            w.Show();
        }


        private void View(object parameter)
        {
            bool isExist = false;
            var para = parameter.ToString() ?? "0";
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(EnrollmentDetails)) isExist = true;
            }
            if (!isExist) ShowDetails(Constants.AppConstants.ActionType.View, para);
        }
    }
}
