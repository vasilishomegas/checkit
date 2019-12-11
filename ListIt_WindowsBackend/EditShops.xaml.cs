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
    /// Interaction logic for EditShops.xaml
    /// </summary>
    public partial class EditShops : Window
    {
        public EditShops()
        {
            InitializeComponent();
            Load_Data();
        }

        public void Load_Data()
        {
            var shopService = new ShopService();
            ShopData.ItemsSource = shopService.GetAll();
        }
    }
}
