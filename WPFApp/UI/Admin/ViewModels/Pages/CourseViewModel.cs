using DataAccess.Models;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFApp.Models;
using WPFApp.UI.Admin.ViewModels.Windows;
using WPFApp.UI.Admin.Views.Windows;

namespace WPFApp.UI.Admin.ViewModels.Pages
{
    public class CourseViewModel : INotifyPropertyChanged
    {
        private readonly ICourseService _service;
        private CourseDetails w;
        public List<Course> _items { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) =>
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public List<Course> Items
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
            var list = items.Where(x => x.Code.Contains(FilterSearch) || x.Title.Contains(FilterSearch)).ToList();

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

        public List<Wallet> GetAll()
        {
            using (var context = new FinancialManagementContext())
            {
                return context.Wallets.Where(w => w.Email == "" && w.WalletName.Contains(FilterSearch)).ToList();
            }
        }

        public CourseViewModel()
        {
            _service = new CourseService();
            SetCommands();
            Task.Run(() => LoadItems()).Wait();
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
                if (window.GetType() == typeof(CourseDetails)) isExist = true;
            }
            if (isExist == false) ShowDetails(Constants.AppConstants.ActionType.Insert, null);
        }

        private void Edit(object parameter)
        {
            bool isExist = false;
            var para = parameter.ToString() ?? "0";
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(CourseDetails)) isExist = true;
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
                if (window.GetType() == typeof(CourseDetails)) isExist = true;
            }
            if (!isExist) ShowDetails(Constants.AppConstants.ActionType.View, para);
        }
    }
}
