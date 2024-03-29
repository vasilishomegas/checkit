﻿using System.Windows;
using ListIt_BusinessLogic.Services;
using ListIt_DomainModel.DTO;

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
            ChainService chainService = new ChainService();
            ChainDropdown.ItemsSource = chainService.GetAll();
            ChainDropdown.SelectedValuePath = "Id";
            ChainDropdown.DisplayMemberPath = "Name";
        }


        private void AddShop_Click(object sender, RoutedEventArgs e)
        {
            // Input validation
            string error = "";
            if (ChainDropdown.SelectedIndex < 0)
                error += "Select a chain\n";
            if (StreetTextbox.Text == "")
                error += "Fill in a street\n";
            if (NumberTextbox.Text == "")
                error += "Fill in a house number\n";
            if (ZipTextbox.Text == "")
                error += "Fill in a zipcode\n";
            if (CityTextbox.Text == "")
                error += "Fill in a city";
            if (error != "") MessageBox.Show(error);
            else
            {
                ShopService shopService = new ShopService();
                try
                {
                    // Create Shop
                    shopService.Create(new ShopDto
                    {
                        Street = StreetTextbox.Text,
                        Number = NumberTextbox.Text,
                        Zipcode = ZipTextbox.Text,
                        City = CityTextbox.Text,
                        Chain_id = (int)ChainDropdown.SelectedValue
                    });
                    MessageBox.Show("Shop added");
                    // Reset form
                    ChainDropdown.SelectedIndex = -1;
                    StreetTextbox.Text = "";
                    NumberTextbox.Text = "";
                    ZipTextbox.Text = "";
                    CityTextbox.Text = "";
                }
                catch
                {
                    MessageBox.Show("Failed to add shop");
                }
            }
        }
    }
}
