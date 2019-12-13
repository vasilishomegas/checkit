using System.Windows;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  WHAT WE NEED
            //validate input
            //tell user if something's wrong
            //send to DB
            //verify response
            //inform user of response
            //clear form for next product

            string error = "";
            if (ChainNameTextBox.Text == "") 
                error += "Fill in Chain name.\n";
            if (LogoLinkTextBox.Text == "")
                error += "Fill in the logo's link.\n";
            if (error != "") MessageBox.Show(error);
            else
            {
                ChainService chainService = new ChainService();
                try
                {
                    ChainDto chainDto = new ChainDto
                    {
                        Name = ChainNameTextBox.Text,
                        Logo = LogoLinkTextBox.Text
                    };
                    if (APIdropdown.SelectedIndex >= 0)
                        chainDto.ShopApi_Id = (int)APIdropdown.SelectedValue;
                    chainService.Create(chainDto);
                    MessageBox.Show("Chain was successfully created!");
                    ChainNameTextBox.Text = "";
                    LogoLinkTextBox.Text = "";
                    APIdropdown.SelectedIndex = -1;
            }
                catch
            {
                MessageBox.Show("Failed to add a chain.");
            }
        }
        }

        private void Load_Data()
        {
            ShopApiService shopApiService = new ShopApiService();
            APIdropdown.ItemsSource = shopApiService.GetAll();
            //APIdropdown.SelectedIndex = -1;
            APIdropdown.SelectedValuePath = "Id";
            APIdropdown.DisplayMemberPath = "Url";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //((App)Application.Current).Back_To_Menu(sender);
        }
    }
}
