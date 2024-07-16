using DataAccess.Models;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFApp.UI.Admin.ViewModels.Pages
{

    public partial class DashboardPage : Page
    {
        public LiveCharts.SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public int currentYear = DateTime.Now.Year;

        public DashboardPage()
        {
            InitializeComponent();
            LoadYears();
            CreateBarChart();
            DataContext = this;
        }

        public void LoadYears()
        {

            using (var context = new CourseManagementDBContext())
            {
                List<Student> accounts = context.Students.ToList();
                var min = accounts.Min(o => o.Birthdate);
                var max = accounts.Max(o => o.Birthdate);

                int minYear = DateTime.Parse(min.ToString()).Year;
                int maxYear = DateTime.Parse(max.ToString()).Year;

                List<int> years = new();
                for (int i = maxYear; i >= minYear; i--)
                {
                    years.Add(i);
                }

                cbYears.ItemsSource = years;
                cbYears.SelectedIndex = 0;
            }
        }

        public void CreateBarChart()
        {

            SeriesCollection = new LiveCharts.SeriesCollection();
            using (var context = new CourseManagementDBContext())
            {

                List<Student> accounts = context.Students.Where(x => x.Birthdate!.Value.Year == currentYear).ToList();
                List<int> users = new();

                lbTotalTransaction.Content = context.Courses.Count();
                lbTotalUser.Content = context.Students.Count();
                lbNewUser.Content = "5".ToString();

                for (int i = 1; i <= 12; i++)
                {
                    int totalUser = 0;
                    foreach (Student ac in accounts)
                    {
                        if (ac.Birthdate?.Month == i)
                        {
                            totalUser++;
                        }
                    }
                    users.Add(totalUser);

                }

                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "Students",
                    Foreground = System.Windows.Application.Current.Resources["PrimaryTextColor"] as Brush,
                    MaxColumnWidth = 35,
                    Values = new ChartValues<int>(users)
                });

                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                chartHorizontal.Series = SeriesCollection;
            }
        }

        private void cbYears_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedValue.ToString();
            if (String.IsNullOrEmpty(text) == false)
            {
                currentYear = int.Parse(text);

                CreateBarChart();
            }

        }


    }
}
