using DataAccess.Models;
using Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFApp.Contexts;

namespace WPFApp.UI.User.Views.Pages
{
    /// <summary>
    /// Interaction logic for InfoSettingView.xaml
    /// </summary>
    public partial class InfoSettingView : System.Windows.Controls.Page
    {
        private readonly IStudentService _service;
        public InfoSettingView()
        {
            _service = new StudentService();
            InitializeComponent();
            LoadData();
        }

        public async void LoadData()
        {
            var data = await _service.GetAll();
            var ac = data.FirstOrDefault(x => x.Name == GlobalVariables.Username);

            if (ac != null)
            {
                GlobalVariables.StudentId = ac.Id;
                txtId.Text = ac.Id.ToString();
                txtAddress.Text = ac.Address;
                txtFullname.Text = ac.Name;
                txtCity.Text = ac.City;
                dpDOB.Text = ac.Birthdate.ToString();
                if (ac.Gender == "Male")
                {
                    rdMale.IsChecked = true;
                }
                else
                {
                    rdFemale.IsChecked = true;
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e) => Update();

        public async void Update()
        {
            await _service.Update(new Student()
            {
                Id = int.Parse(txtId.Text),
                Address = txtAddress.Text,
                City = txtCity.Text,
                Name = txtFullname.Text,
                Birthdate = DateTime.Parse(dpDOB.Text)
            });

            MessageBox.Show("Update info successfully.");
        }
    }
}
