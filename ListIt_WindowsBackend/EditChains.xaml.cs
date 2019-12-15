using System.Windows;
using ListIt_BusinessLogic.Services;
using ListIt_DomainModel.DTO;

namespace ListIt_WindowsBackend
{
    /// <summary>
    /// Interaction logic for EditChains.xaml
    /// </summary>
    public partial class EditChains : Window
    {
        public EditChains()
        {
            InitializeComponent();
            Load_Data();
        }
        public void Load_Data()
        {
            ChainService chainService = new ChainService();
            ChainData.ItemsSource = chainService.GetAll();
        }
    }
}
