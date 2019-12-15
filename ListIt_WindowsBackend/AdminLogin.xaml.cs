using System.Windows;
using ListIt_BusinessLogic.Services;

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
                AdminService adminService = new AdminService();
                try
                {
                    adminService.Login(UsernameField.Text, PasswordField.Password);
                    var window = new MenuWindow();
                    window.Show();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Invalid credentials");
                }
            }
        }
    }
}