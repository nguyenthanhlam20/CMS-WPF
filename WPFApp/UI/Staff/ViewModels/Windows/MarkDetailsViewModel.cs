using DataAccess.Models;
using Services;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using WPFApp.UI.Staff.ViewModels.Pages;
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.Staff.ViewModels.Windows
{
    public class MarkDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IMarkService _service;
        private MarkViewModel _viewModel;

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

        public decimal _mark1;

        public decimal Mark1
        {
            get { return _mark1; }
            set
            {
                _mark1 = value;
                OnPropertyChanged("Mark1");
            }
        }

        public int _enrollmentId;

        public int EnrollmentId
        {
            get { return _enrollmentId; }
            set
            {
                _enrollmentId = value;
                OnPropertyChanged("EnrollmentId");
            }
        }

        public int _assessmentId;

        public int AssessmentId
        {
            get { return _assessmentId; }
            set
            {
                _assessmentId = value;
                OnPropertyChanged("AssessmentId");
            }
        }



        public ReplayCommand? CloseWindowCommand { get; set; }

        public ReplayCommand? SaveCommand { get; set; }

        public ActionType? actionType { get; set; }

        public MarkDetailsViewModel(ActionType aType, int enrollmentId, int assessmentId, MarkViewModel viewModel)
        {
            _viewModel = viewModel;
            _service = new MarkService();

            actionType = aType;
            EnrollmentId = enrollmentId;
            AssessmentId = assessmentId;

            CloseWindowCommand = new ReplayCommand(CloseWindow);
            SaveCommand = new ReplayCommand(Save);
            if (aType == ActionType.Insert)
            {
                Title = "create new mark".ToUpper();
                GetId();
            }

            if (aType == ActionType.Edit)
            {
                Title = "edit mark".ToUpper();
                GetDetails();
            }

            if (aType == ActionType.View)
            {
                Title = "view mark".ToUpper();
                GetDetails();
            }
        }

        public async void Save(object parameter)
        {
            if (actionType == ActionType.Insert)
            {
                await _service.AddNew(new Mark()
                {
                    EnrollmentId = EnrollmentId,
                    AssessmentId = AssessmentId,
                    Mark1 = Mark1,
                });
            }

            if (actionType == ActionType.Edit)
            {
                await _service.Update(new Mark()
                {
                    EnrollmentId = EnrollmentId,
                    AssessmentId = AssessmentId,
                    Mark1 = Mark1,
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
                Mark1 = 0;
                EnrollmentId = item?.EnrollmentId ?? 0;
                AssessmentId = item?.AssessmentId ?? 0;
            }
        }

        public async void GetDetails()
        {
            var data = await _service.GetAll();
            var item = data.SingleOrDefault(
     x => x.EnrollmentId == EnrollmentId 
    && x.AssessmentId == AssessmentId);
            if (item != null)
            {
                EnrollmentId = item.EnrollmentId;
                AssessmentId = item?.AssessmentId ?? 0;
                Mark1 = item?.Mark1 ?? 0;
            }
        }

    }
}
