using DataAccess.Models;
using Services;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using WPFApp.UI.Admin.ViewModels.Pages;
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.Admin.ViewModels.Windows
{
    public class AssessmentDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IAssessmentService _service;
        private readonly ICourseService _courseService;
        private AssessmentViewModel _viewModel;

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

        public string _type = null!;

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }

        public string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public double? _percent;

        public double? Percent
        {
            get { return _percent; }
            set
            {
                _percent = value;
                OnPropertyChanged("Percent");
            }
        }

        public int? _courseId;

        public int? CourseId
        {
            get { return _courseId; }
            set
            {
                _courseId = value;
                OnPropertyChanged("CourseId");
            }
        }

        public ReplayCommand? CloseWindowCommand { get; set; }

        public ReplayCommand? SaveCommand { get; set; }

        public ActionType? actionType { get; set; }

        public int Id { get; set; }

        public AssessmentDetailsViewModel(ActionType aType, int id, AssessmentViewModel viewModel)
        {
            _viewModel = viewModel;
            _service = new AssessmentService();
            _courseService = new CourseService();

            actionType = aType;
            Id = id;

            CloseWindowCommand = new ReplayCommand(CloseWindow);
            SaveCommand = new ReplayCommand(Save);
            if (aType == ActionType.Insert)
            {
                Title = "create new assessment".ToUpper();
                GetId();
            }

            if (aType == ActionType.Edit)
            {
                Title = "edit assessment".ToUpper();
                GetDetails();
            }

            if (aType == ActionType.View)
            {
                Title = "view assessment".ToUpper();
                GetDetails();
            }
        }

        public async void GetId()
        {
            var data = await _courseService.GetAll();
            var item = data.First();
            if (item != null)
            {
                CourseId = item.Id;
            }
        }

        public async void Save(object parameter)
        {
            if (actionType == ActionType.Insert)
            {
                await _service.AddNew(new Assessment()
                {
                    Type = Type,
                    Name = Name,
                    Percent = Percent,
                    CourseId = CourseId,
                });
            }

            if (actionType == ActionType.Edit)
            {
                await _service.Update(new Assessment()
                {
                    Id = Id,
                    Type = Type,
                    Name = Name,
                    Percent = Percent,
                    CourseId = CourseId,

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
                Type = item.Type ?? "";
                Name = item.Name ?? "";
                Percent = item.Percent;
                CourseId = item.CourseId;
            }
        }

    }
}
