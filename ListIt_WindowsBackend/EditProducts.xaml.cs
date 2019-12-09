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
using ListIt_DomainModel.DTO.Interfaces;


namespace ListIt_WindowsBackend
{
    /// <summary>
    /// Interaction logic for EditProducts.xaml
    /// </summary>
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
            ProductData.ItemsSource = productService.GetAll();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //((App)Application.Current).Back_To_Menu(sender);
        }

        private void ProductData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
