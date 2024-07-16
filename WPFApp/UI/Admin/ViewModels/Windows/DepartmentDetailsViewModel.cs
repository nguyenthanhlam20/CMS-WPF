using DataAccess.Models;
using Services;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using WPFApp.UI.Admin.ViewModels.Pages;
using static WPFApp.Constants.AppConstants;

namespace WPFApp.UI.Admin.ViewModels.Windows
{
    public class DepartmentDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IDepartmentService _service;
        private DepartmentViewModel _viewModel;

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
        public string _name = null!;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public ReplayCommand? CloseWindowCommand { get; set; }

        public ReplayCommand? SaveCommand { get; set; }

        public ActionType? actionType { get; set; }

        public string Id { get; set; }

        public DepartmentDetailsViewModel(ActionType aType, string id, DepartmentViewModel viewModel)
        {
            _viewModel = viewModel;
            _service = new DepartmentService();

            actionType = aType;
            Id = id;

            CloseWindowCommand = new ReplayCommand(CloseWindow);
            SaveCommand = new ReplayCommand(Save);
            if (aType == ActionType.Insert)
                Title = "create new department".ToUpper();

            if (aType == ActionType.Edit)
            {
                Title = "edit department".ToUpper();
                GetDetails();
            }

            if (aType == ActionType.View)
            {
                Title = "view department".ToUpper();
                GetDetails();
            }
        }

        public async void Save(object parameter)
        {
            if (actionType == ActionType.Insert)
            {
                await _service.AddNew(new Department()
                {
                    Code = Code,
                    Name = Name,
                });
            }

            if (actionType == ActionType.Edit)
            {
                await _service.Update(new Department()
                {
                    Code = Code,
                    Name = Name,
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
            var item = data.SingleOrDefault(x => x.Code == Id);
            if (item != null)
            {
                Code = item.Code ?? "";
                Name = item.Name;
            }
        }

    }
}
