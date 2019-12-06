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
    /// Interaction logic for AddShop.xaml
    /// </summary>
    public partial class AddShop : Window
    {
        public AddShop()
        {
            InitializeComponent();
            Load_Data();
        }

        private void Load_Data()
        {
            ShopApiService shopApiService = new ShopApiService();
            ShopApiDropdown.ItemsSource = shopApiService.GetAll();
            ShopApiDropdown.SelectedValuePath = "Id";
            ShopApiDropdown.DisplayMemberPath = "Url";
        }
        //
      //  private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
       // {

        //}
    }
}
