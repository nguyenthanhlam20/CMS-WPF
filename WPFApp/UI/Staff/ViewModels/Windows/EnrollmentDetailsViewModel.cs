using DataAccess.Models;
using Services;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using WPFApp.UI.Staff.ViewModels.Pages;
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.Staff.ViewModels.Windows
{
    public class EnrollmentDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IEnrollmentService _service;
        private EnrollmentViewModel _viewModel;

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


        public int _studentId;

        public int StudentId
        {
            get { return _studentId; }
            set
            {
                _studentId = value;
                OnPropertyChanged("StudentId");
            }
        }

        public int _courseId;

        public int CourseId
        {
            get { return _courseId; }
            set
            {
                _courseId = value;
                OnPropertyChanged("CourseId");
            }
        }

        public int _semesterId;

        public int SemesterId
        {
            get { return _semesterId; }
            set
            {
                _semesterId = value;
                OnPropertyChanged("SemesterId");
            }
        }


        public ReplayCommand? CloseWindowCommand { get; set; }

        public ReplayCommand? SaveCommand { get; set; }

        public ActionType? actionType { get; set; }

        public int Id { get; set; }

        public EnrollmentDetailsViewModel(ActionType aType, int id, EnrollmentViewModel viewModel)
        {
            _viewModel = viewModel;
            _service = new EnrollmentService();

            actionType = aType;
            Id = id;

            CloseWindowCommand = new ReplayCommand(CloseWindow);
            SaveCommand = new ReplayCommand(Save);
            if (aType == ActionType.Insert)
            {
                Title = "create new semester".ToUpper();
                GetId();
            }

            if (aType == ActionType.Edit)
            {
                Title = "edit semester".ToUpper();
                GetDetails();
            }

            if (aType == ActionType.View)
            {
                Title = "view semester".ToUpper();
                GetDetails();
            }
        }

        public async void Save(object parameter)
        {
            if (actionType == ActionType.Insert)
            {
                await _service.AddNew(new Enrollment()
                {
                    StudentId = StudentId,
                    CourseId = CourseId,
                    SemesterId = SemesterId,
                });
            }

            if (actionType == ActionType.Edit)
            {
                await _service.Update(new Enrollment()
                {
                    EnrollmentId = Id,
                    StudentId = StudentId,
                    CourseId = CourseId,
                    SemesterId = SemesterId,
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

        public async void GetId()
        {
            var data = await _service.GetAll();
            var item = data.First();
            if (item != null)
            {
                CourseId = item.CourseId ?? 0;
                SemesterId = item?.SemesterId ?? 0;
                StudentId = item?.StudentId ?? 0;
            }
        }

        public async void GetDetails()
        {
            var data = await _service.GetAll();
            var item = data.SingleOrDefault(x => x.EnrollmentId == Id);
            if (item != null)
            {
                CourseId = item.CourseId ?? 0;
                SemesterId = item?.SemesterId ?? 0;
                StudentId = item?.StudentId ?? 0;
            }
        }

    }
}
