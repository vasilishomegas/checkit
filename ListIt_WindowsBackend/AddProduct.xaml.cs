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

namespace ListIt_WindowsBackend
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
            Load_Data();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Load_Data()
        {
            ProductTypeService productTypeService = new ProductTypeService();
            //UnitService unitService = new UnitService();
            LanguageService languageService = new LanguageService();
            // CurrencyService currencyService = new CurrencyService();
            //TypeBox.ItemsSource =

            //IList<ListIt_DomainModel.DTO.ProductTypeDto> productTypes = productTypeService.GetAll().ToList();
            TypeBox.ItemsSource = productTypeService.GetAll().ToList();
            //TypeBox.ItemsSource = productTypes;
            TypeBox.SelectedValuePath = "Id";
            TypeBox.DisplayMemberPath = "Name";
            TypeBox.SelectedIndex = 0;
            LanguageBox.ItemsSource = languageService.GetAll().ToList();
            LanguageBox.SelectedValuePath = "Id";
            LanguageBox.DisplayMemberPath = "Name";
            LanguageBox.SelectedIndex = 0;
        }
    }
}
