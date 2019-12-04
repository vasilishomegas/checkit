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

namespace ListIt_WindowsFrontend
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Load_Data(object sender, EventArgs e)
        {
            ProductTypeService productTypeService = new ProductTypeService();
            //UnitService unitService = new UnitService();
            LanguageService languageService = new LanguageService();
            // CurrencyService currencyService = new CurrencyService();
            //TypeBox.ItemsSource =
            
        }
    }
}
