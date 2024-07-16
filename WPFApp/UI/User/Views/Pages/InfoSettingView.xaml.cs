using WPFApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFApp.UI.User.Views.Pages
{
    /// <summary>
    /// Interaction logic for InfoSettingView.xaml
    /// </summary>
    public partial class InfoSettingView : System.Windows.Controls.Page
    {
        public InfoSettingView()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            using (var context = new FinancialManagementContext())
            {

                Account ac = context.Accounts.SingleOrDefault(a => a.Email == "");

                if (ac != null)
                {

                    txtEmail.Text = ac.Email;
                    txtFullname.Text = ac.FullName;
                    txtPhone.Text = ac.Phone;
                    txtPassword.Text = ac.Password;
                    txtConfirmPassword.Text = ac.Password;
                    dpDOB.Text = ac.Dob.ToString();
                    if (ac.Gender == true)
                    {
                        rdMale.IsChecked = true;
                    }
                    else
                    {
                        rdFemale.IsChecked = true;
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new FinancialManagementContext())
            {

                Account ac = context.Accounts.SingleOrDefault(a => a.Email == "");

                if (ac != null)
                {

                    ac.Email = txtEmail.Text;
                    ac.FullName = txtFullname.Text;
                    ac.Phone = txtPhone.Text;
                    ac.Password = txtPassword.Text;
                    ac.Dob = DateTime.Parse(dpDOB.Text);
                    ac.Gender = rdMale.IsChecked;

                    context.Update(ac);
                    if (context.SaveChanges() > 0)
                    {
                        MessageBox.Show("Update information succesful");
                    }
                }
            }
        }
    }
}
