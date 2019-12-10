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

namespace ListIt_WindowsBackend
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddProduct();
            window.ShowDialog();
            //this.Close();
        }

        private void AddChain_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddChain();
            window.ShowDialog();
            //this.Close();
        }

        private void AddShop_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddShop();
            window.ShowDialog();
            //this.Close();
        }

        private void EditProducts_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditProducts();
            window.ShowDialog();
        }

        private void EditChains_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditShops_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var window = new AdminLogin();
            window.Show();
            this.Close();
            //((App)Application.Current).Logout(sender);
        }
    }
}
