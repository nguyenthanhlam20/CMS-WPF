using DataAccess.Models;
using Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using WPFApp.UI.Staff.Views.Windows;

namespace WPFApp.UI.Staff.ViewModels.Pages
{
    public class MarkViewModel : INotifyPropertyChanged
    {
        private readonly IMarkService _service;
        private readonly IAssessmentService _assessmentService;
        private readonly IEnrollmentService _enrollmentService;
        private MarkDetails w;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) =>
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public List<Assessment> _assessments { get; set; }

        public List<Assessment> Assessments
        {
            get { return _assessments; }
            set
            {
                _assessments = value;
                OnPropertyChanged("Assessments");

            }
        }

        public List<Enrollment> _enrollments { get; set; }
        public List<Enrollment> Enrollments
        {
            get { return _enrollments; }
            set
            {
                _enrollments = value;
                OnPropertyChanged("Enrollments");

            }
        }

        public List<Mark> _items { get; set; }
        public List<Mark> Items
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

        public class MarkModel
        {

        }

        public async Task LoadItems()
        {
            var items = await _service.GetAll();
            var list = items;

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


        public MarkViewModel()
        {
            _service = new MarkService();
            _enrollmentService = new EnrollmentService();
            _assessmentService = new AssessmentService();
            SetCommands();
            Task.Run(() => LoadItems()).Wait();
            Task.Run(() => LoadAssessments()).Wait();
            Task.Run(() => LoadEnrollments()).Wait();
        }

        public async Task LoadAssessments()
        {
            Assessments = await _assessmentService.GetAll();
        }

        public async Task LoadEnrollments()
        {
            Enrollments = await _enrollmentService.GetAll();
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
                if (window.GetType() == typeof(MarkDetails)) isExist = true;
            }
            if (isExist == false) ShowDetails(Constants.AppConstants.ActionType.Insert, null);
        }

        private void Edit(object parameter)
        {
            bool isExist = false;
            var para = parameter.ToString() ?? "0";
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MarkDetails)) isExist = true;
            }
            if (!isExist) ShowDetails(Constants.AppConstants.ActionType.Edit, parameter);
        }

        public void ShowDetails(Constants.AppConstants.ActionType type, object parameter)
        {
            w = new(this);
            w.AType = type;

            var parameters = parameter as object[];
            if (parameters != null && parameters.Length == 2)
            {
                w.EnrollmentId = int.Parse(parameters[0].ToString());
                w.AssessmentId = int.Parse(parameters[1].ToString());
            }

            w.SetDataContext();
            w.Show();
        }


        private void View(object parameter)
        {
            bool isExist = false;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MarkDetails)) isExist = true;
            }
            if (!isExist) ShowDetails(Constants.AppConstants.ActionType.View, parameter);
        }
    }
}
