using DataAccess.Models;
using Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using WPFApp.UI.Admin.ViewModels.Pages;
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.Admin.ViewModels.Windows
{
    public class SemesterDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly ISemesterService _service;
        private SemesterViewModel _viewModel;

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

        public int _year;

        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged("Year");
            }
        }

        public DateTime? _beginDate;

        public DateTime? BeginDate
        {
            get { return _beginDate; }
            set
            {
                _beginDate = value;
                OnPropertyChanged("BeginDate");
            }
        }

        public DateTime? _endDate;

        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        public ReplayCommand? CloseWindowCommand { get; set; }

        public ReplayCommand? SaveCommand { get; set; }

        public ActionType? actionType { get; set; }

        public int Id { get; set; }

        public SemesterDetailsViewModel(ActionType aType, int id, SemesterViewModel viewModel)
        {
            _viewModel = viewModel;
            _service = new SemesterService();

            actionType = aType;
            Id = id;

            CloseWindowCommand = new ReplayCommand(CloseWindow);
            SaveCommand = new ReplayCommand(Save);
            if (aType == ActionType.Insert)
                Title = "create new semester".ToUpper();

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
                await _service.AddNew(new Semester()
                {
                    Code = Code,
                    Year = Year,
                    BeginDate = BeginDate,
                    EndDate = EndDate,
                });
            }

            if (actionType == ActionType.Edit)
            {
                await _service.Update(new Semester()
                {
                    Id = Id,
                    Code = Code,
                    Year = Year,
                    BeginDate = BeginDate,
                    EndDate = EndDate,
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
                Code = item.Code ?? "";
                Year = item?.Year ?? 0;
                BeginDate = item?.BeginDate;
                EndDate = item?.EndDate;
            }
        }

    }
}
