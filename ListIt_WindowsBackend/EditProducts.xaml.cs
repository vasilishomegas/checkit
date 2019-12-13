using System.Windows;
using ListIt_BusinessLogic.Services;
using ListIt_DomainModel.DTO.Interfaces;


namespace ListIt_WindowsBackend
{
    public partial class EditProducts : Window
    {
        public EditProducts()
        {
            InitializeComponent();
            Load_Data();
        }

        private void Load_Data()
        {
            ProductService productService = new ProductService();
            ProductData.ItemsSource = productService.GetDefaultProductDtos();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //((App)Application.Current).Back_To_Menu(sender);
        }
    }
}
