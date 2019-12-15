using System.Windows;

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
        }

        private void AddChain_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddChain();
            window.ShowDialog();
        }

        private void AddShop_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddShop();
            window.ShowDialog();
        }

        private void EditProducts_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditProducts();
            window.ShowDialog();
        }

        private void EditChains_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditChains();
            window.ShowDialog();
        }

        private void EditShops_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditShops();
            window.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var window = new AdminLogin();
            window.Show();
            this.Close();
        }
    }
}
