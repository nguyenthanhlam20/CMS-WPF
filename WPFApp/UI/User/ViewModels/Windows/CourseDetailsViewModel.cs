using Services;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using DataAccess.Models;
using static WPFApp.Constants.AppConstants;
using WPFApp.UI.User.ViewModels.Pages;

namespace WPFApp.UI.User.ViewModels.Windows
{
    public class CourseDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly ICourseService _service;
        private CourseViewModel _viewModel;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string _title = null!;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public string _courseTitle = null!;

        public string CourseTitle
        {
            get { return _courseTitle; }
            set
            {
                _courseTitle = value;
                OnPropertyChanged("CourseTitle");
            }
        }

        public string _code = null!;

        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                OnPropertyChanged("Code");
            }
        }

        public byte? _credits;

        public byte? Credits
        {
            get { return _credits; }
            set
            {
                _credits = value;
                OnPropertyChanged("Credits");
            }
        }

        public ReplayCommand? CloseWindowCommand { get; set; }

        public ReplayCommand? SaveCommand { get; set; }

        public ActionType? actionType { get; set; }

        public int Id { get; set; }

        public CourseDetailsViewModel(ActionType aType, int id, CourseViewModel viewModel)
        {
            _viewModel = viewModel;
            _service = new CourseService();

            actionType = aType;
            Id = id;

            CloseWindowCommand = new ReplayCommand(CloseWindow);
            SaveCommand = new ReplayCommand(Save);
            if (aType == ActionType.Insert)
                Title = "create new course".ToUpper();

            if (aType == ActionType.Edit)
            {
                Title = "edit course".ToUpper();
                GetDetails();
            }

            if (aType == ActionType.View)
            {
                Title = "view course".ToUpper();
                GetDetails();
            }
        }

        public async void Save(object parameter)
        {
            if (actionType == ActionType.Insert)
            {
                await _service.AddNew(new Course()
                {
                    Code = Code,
                    Title = CourseTitle,
                    Credits = Credits,
                });
            }

            if (actionType == ActionType.Edit)
            {
                await _service.Update(new Course()
                {
                    Id = Id,
                    Code = Code,
                    Title = CourseTitle,
                    Credits = Credits,
                });
            }
            var w = parameter as Window;
            await _viewModel.LoadItems();
            w?.Close();
        }


        public void CloseWindow(object parameter)
        {
            var w = parameter as Window;
            w?.Close();
        }

        public async void GetDetails()
        {
            var data = await _service.GetAll();
            var item = data.SingleOrDefault(x => x.Id == Id);
            if (item != null)
            {
                Code = item.Code;
                CourseTitle = item.Title;
                Credits = item.Credits;
            }
        }

    }
}
