using WPFApp.Constants;
using WPFApp.Models;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFApp.UI.User.Views.Pages
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : System.Windows.Controls.Page
    {
        public LiveCharts.SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public Func<string, string> FormatterLabel { get; set; }

        public int currentYear = DateTime.Now.Year;

        public DashboardView()
        {
            InitializeComponent();
            GetCardData();
            LoadYears();
            CreateBarChart();
            DataContext = this;
        }

        public void LoadYears()
        {

            using (var context = new FinancialManagementContext())
            {
                List<int> years = new();
                List<Transaction> transactions = context.Transactions.Where(tr => tr.Owner == "").ToList();
               if(transactions.Count > 0)
                {
                    var min = transactions.Min(o => o.TransactionDate);
                    var max = transactions.Max(o => o.TransactionDate);

                    transactions.Where(tr => tr.TransactionDate.Year == currentYear).ToList();

                    int minYear = DateTime.Parse(min.ToString()).Year;
                    int maxYear = DateTime.Parse(max.ToString()).Year;

                   
                    for (int i = maxYear; i >= minYear; i--)
                    {
                        years.Add(i);
                    }

                   
                } else
                {
                    years.Add(DateTime.Now.Year);

                }
                cbYears.ItemsSource = years;
                cbYears.SelectedIndex = 0;
            }
        }

        public void CreateBarChart()
        {

            SeriesCollection = new LiveCharts.SeriesCollection();
            using (var context = new FinancialManagementContext())
            {

                List<Transaction> transactions = context.Transactions.Where(tr => tr.Owner == "").ToList();
                transactions = transactions.Where(tr => tr.TransactionDate.Year == currentYear).ToList();

                List<double> incomes = new();
                List<double> expenses = new();

                for (int i = 1; i <= 12; i++)
                {

                    double totalIncome = 0;
                    double totalExpense = 0;

                    foreach (Transaction tr in transactions)
                    {
                        if (DateTime.Parse(tr.TransactionDate.ToString()).Month == i)
                        {
                            if (tr.TransactionTypeId == (int)AppConstants.TransactionType.Income
                                && tr.TransactionStatusId == (int)AppConstants.TransctionStatus.Completed)
                            {
                                totalIncome += tr.Amount;
                            }

                            if (tr.TransactionTypeId == (int)AppConstants.TransactionType.Expense
                               && tr.TransactionStatusId == (int)AppConstants.TransctionStatus.Completed)
                            {
                                totalExpense += tr.Amount;
                            }
                        }
                    }

                    incomes.Add(totalIncome);
                    expenses.Add(totalExpense);

                }





                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "Income",
                    Foreground = System.Windows.Application.Current.Resources["PrimaryTextColor"] as Brush,
                    MaxColumnWidth = 25,
                    Values = new ChartValues<double>(incomes)
                });

                //adding series will update and animate the chart automatically
                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "Expense",
                    MaxColumnWidth = 25,

                    Foreground = System.Windows.Application.Current.Resources["PrimaryTextColor"] as Brush,
                    Values = new ChartValues<double>(expenses)
                });



                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                Formatter = value => value.ToString("C2");
                FormatterLabel = value => value.ToLower();
                chartHorizontal.Series = SeriesCollection;
            }




        }


        public void GetCardData()
        {
        
            
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
