using System.Windows;
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
