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
    /// Interaction logic for AddChain.xaml
    /// </summary>
    public partial class AddChain : Window
    {
        public AddChain()
        {
            InitializeComponent();
            Load_Data();
        }

        private void Load_Data()
        {
            ShopApiService shopApiService = new ShopApiService();
            APIdropdown.ItemsSource = shopApiService.GetAll();
            APIdropdown.SelectedValuePath = "Id";
            APIdropdown.DisplayMemberPath = "Url";
        }
    }
}
