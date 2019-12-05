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
using System.Windows.Shapes;
using ListIt_BusinessLogic.Services;
using ListIt_DomainModel.DTO;

namespace ListIt_WindowsBackend
{
    /// <summary>
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin : Window
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (UsernameField.Text == "")
                error += "Fill in the username.\n";
            if (PasswordField.Password == "")
                error += "Fill in the password.\n";
            if (error != "") MessageBox.Show(error);
            else
            {
                AdminLogin adminLogin = new AdminLogin();
                adminService.Create(new AdminDto
                {

                })
            }
        }
    }
}
