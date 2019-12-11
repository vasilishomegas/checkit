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

            // Input validation (and parsing)
            float price = 0;
            string error = "";
            if (TypeBox.SelectedIndex < 0)
                error += "Select a product type\n";
            if (NameField.Text == "")
                error += "Fill in a product name\n";
            if (CategoryBox.SelectedIndex < 0)
                error += "Select a category\n";
            if (UnitBox.SelectedIndex < 0)
                error += "Select a unit\n";
            if (LanguageBox.SelectedIndex < 0)
                error += "Select a language\n";
            if (CurrencyBox.SelectedIndex < 0)
                error += "Select a currency\n";
            if (PriceField.Text == "")
                error += "Fill in a price\n";
            else if (!float.TryParse(PriceField.Text, out price))
                error += "Invalid price";
            if (error != "") MessageBox.Show(error);
            // Normal execution if no errors occurred
            else
            {
                ProductService productService = new ProductService();
                // Currently, every product is added as if it's a DefaultProduct,
                // even though it's possible to make it as another type of Product
                try
                {
                   // Create Product
                    productService.Create(new DefaultProductDto
                    {
                        ProductTypeId = (int)TypeBox.SelectedValue,
                        Name = NameField.Text,
                        Currency_Id = (int)CurrencyBox.SelectedValue,
                        Unit_Id = (int)UnitBox.SelectedValue,
                        Price = (decimal)price,
                        Category_Id = (int)CategoryBox.SelectedValue
                    }, (int)LanguageBox.SelectedValue);
                    MessageBox.Show("Product added!");
                    TypeBox.SelectedIndex = 0;
                    NameField.Text = "";
                    UnitBox.SelectedIndex = -1;
                    CategoryBox.SelectedIndex = -1;
                    LanguageBox.SelectedIndex = 0;
                    CurrencyBox.SelectedIndex = 0;
                    PriceField.Text = "";
                }
                catch
                {
                    MessageBox.Show("Failed to add product");
                }
            }
        }

        private void Load_Data()
        {
            ProductTypeService productTypeService = new ProductTypeService();
            UnitTypeService unitService = new UnitTypeService();
            LanguageService languageService = new LanguageService();
            CurrencyService currencyService = new CurrencyService();
            CategoryService categoryService = new CategoryService();

            TypeBox.ItemsSource = productTypeService.GetAll();
            TypeBox.SelectedValuePath = "Id";
            TypeBox.DisplayMemberPath = "Name";
            TypeBox.SelectedIndex = 0;
            UnitBox.ItemsSource = unitService.GetUnitTypesByLanguage(2);
            UnitBox.SelectedValuePath = "Id";
            UnitBox.DisplayMemberPath = "Name";
            LanguageBox.ItemsSource = languageService.GetAll();
            LanguageBox.SelectedValuePath = "Id";
            LanguageBox.DisplayMemberPath = "Name";
            LanguageBox.SelectedIndex = 0;
            CurrencyBox.ItemsSource = currencyService.GetAll();
            CurrencyBox.SelectedValuePath = "Id";
            CurrencyBox.DisplayMemberPath = "Name";
            CurrencyBox.SelectedIndex = 0;
            CategoryBox.ItemsSource = categoryService.GetDefaultCategories(2);
            CategoryBox.SelectedValuePath = "Id";
            CategoryBox.DisplayMemberPath = "Name";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //((App)Application.Current).Back_To_Menu(sender);
        }
    }
}
