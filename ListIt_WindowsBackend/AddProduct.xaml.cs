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
            // TODO:
            // -- Validate input
            // -- If not gud, tell user
            // Send to DB
            // Verify response
            // Inform user of response
            // Clear form for next product

            float price;
            string error = "";
            if (NameField.Text == "")
                error += "Fill in a product name\n";
            if (PriceField.Text == "")
                error += "Fill in a price\n";
            if (!float.TryParse(PriceField.Text, out price))
                error += "Invalid price";
            if (error != "") MessageBox.Show(error);
            else
            {
                ProductService productService = new ProductService();
                //if (TypeBox.SelectedItem == 1)
                productService.Create(new DefaultProductDto
                {
                    ProductTypeId = (int)TypeBox.SelectedValue,
                    Name = NameField.Text,
                    Currency_Id = (int)CurrencyBox.SelectedValue,
                    Unit_Id = (int)UnitBox.SelectedValue,
                    Price = (decimal)price
                });
                // Create Product
                // Create linked DefaultProduct
                //productService.Create(new DefaultProductDto { })
            }
        }

        private void Load_Data()
        {
            ProductTypeService productTypeService = new ProductTypeService();
            UnitTypeService unitService = new UnitTypeService();
            LanguageService languageService = new LanguageService();
            CurrencyService currencyService = new CurrencyService();

            //IList<ListIt_DomainModel.DTO.ProductTypeDto> productTypes = productTypeService.GetAll().ToList();
            TypeBox.ItemsSource = productTypeService.GetAll();
            TypeBox.SelectedValuePath = "Id";
            TypeBox.DisplayMemberPath = "Name";
            TypeBox.SelectedIndex = 0;
            UnitBox.ItemsSource = unitService.GetUnitTypesByLanguage(2);
            UnitBox.SelectedValuePath = "Id";
            UnitBox.DisplayMemberPath = "Name";
            UnitBox.SelectedIndex = 0;
            LanguageBox.ItemsSource = languageService.GetAll();
            LanguageBox.SelectedValuePath = "Id";
            LanguageBox.DisplayMemberPath = "Name";
            LanguageBox.SelectedIndex = 0;
            CurrencyBox.ItemsSource = currencyService.GetAll();
            CurrencyBox.SelectedValuePath = "Id";
            CurrencyBox.DisplayMemberPath = "Name";
            CurrencyBox.SelectedIndex = 0;
        }
    }
}
