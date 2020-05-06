using PwnedPass2.Models;
using System.Collections.Generic;
using System.ComponentModel;

using Xamarin.Forms;

namespace PwnedPass2.Views
{
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.EmailSearch, Title="Search by Email" },
                new HomeMenuItem {Id = MenuItemType.PasswordSearch, Title="Search by Password" },
                new HomeMenuItem {Id = MenuItemType.List, Title="List of Data Breaches" },
                new HomeMenuItem {Id = MenuItemType.About, Title="About" },
                new HomeMenuItem {Id = MenuItemType.Rate, Title="Rate Pwned Pass" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}