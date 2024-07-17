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
    public class StudentDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IStudentService _service;
        private StudentViewModel _viewModel;

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

        public DateTime _birthDate;

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }


        public string _gender;

        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged("Gender");
            }
        }


        public string _city;

        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }


        public string _country;

        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged("Country");
            }
        }


        public string _departmentId;

        public string DepartmentId
        {
            get { return _departmentId; }
            set
            {
                _departmentId = value;
                OnPropertyChanged("DepartmentId");
            }
        }


        public string _address = null!;

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }



        public ReplayCommand? CloseWindowCommand { get; set; }

        public ReplayCommand? SaveCommand { get; set; }

        public ActionType? actionType { get; set; }

        public int Id { get; set; }

        public StudentDetailsViewModel(ActionType aType, int id, StudentViewModel viewModel)
        {
            _viewModel = viewModel;
            _service = new StudentService();

            actionType = aType;
            Id = id;

            CloseWindowCommand = new ReplayCommand(CloseWindow);
            SaveCommand = new ReplayCommand(Save);
            if (aType == ActionType.Insert)
            {
                Title = "create new student".ToUpper();
                BirthDate = DateTime.Now;
                DepartmentId = _viewModel.Departments.First().Code;
            }

            if (aType == ActionType.Edit)
            {
                Title = "edit student".ToUpper();
                GetDetails();
            }

            if (aType == ActionType.View)
            {
                Title = "view student".ToUpper();
                GetDetails();
            }
        }

        public async void Save(object parameter)
        {
            if (actionType == ActionType.Insert)
            {
                await _service.AddNew(new Student()
                {
                    Name = Name,
                    Birthdate = BirthDate,
                    Address = Address,
                    City = City,
                    Country = Country,
                    Department = DepartmentId,
                    Gender = Gender,
                });
            }

            if (actionType == ActionType.Edit)
            {
                await _service.Update(new Student()
                {
                    Id = Id,
                    Name = Name,
                    Birthdate = BirthDate,
                    Address = Address,
                    City = City,
                    Country = Country,
                    Department = DepartmentId,
                    Gender = Gender,
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
                Name = item.Name ?? "";
                BirthDate = item.Birthdate ?? DateTime.MinValue;
                City = item.City ?? "";
                Country = item.Country ?? "";
                DepartmentId = item.Department ?? "";
                Gender = item.Gender ?? "";
                Address = item.Address ?? "";
            }
        }

    }
}
