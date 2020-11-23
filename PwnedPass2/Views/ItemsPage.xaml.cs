using PwnedPass2.Interfaces;
using PwnedPass2.Models;
using PwnedPass2.ViewModels;
using PwnedPasswords.Core;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace PwnedPass2.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        private ItemsViewModel viewModel;
        public bool Order { get; set; }

        public string BreachInp { get; set; }

        public ItemsPage()
        {
            InitializeComponent();
            BreachInp = breachEntry.Text;
            BindingContext = viewModel = new ItemsViewModel(BreachInp, false, "AddedDate");
            DependencyService.Get<IFooter>().AddFooter(this, this.stack);
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is HIBP item))
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private void SortClickedDate(object sender, EventArgs e)
        {
            if (Order)
            {
                Order = false;
            }
            else
            {
                Order = true;
            }
            BreachInp = breachEntry.Text;
            BindingContext = viewModel = new ItemsViewModel(BreachInp, Order, "AddedDate");
            viewModel.LoadItemsCommand.Execute(null);
            breachEntry.Text = BreachInp;
        }

        private void SortClickedName(object sender, EventArgs e)
        {
            if (Order)
            {
                Order = false;
            }
            else
            {
                Order = true;
            }
            BreachInp = breachEntry.Text;
            BindingContext = viewModel = new ItemsViewModel(BreachInp, Order, "Name");
            viewModel.LoadItemsCommand.Execute(null);
            breachEntry.Text = BreachInp;
        }

        private void SortClickedPwned(object sender, EventArgs e)
        {
            if (Order)
            {
                Order = false;
            }
            else
            {
                Order = true;
            }
            BreachInp = breachEntry.Text;
            BindingContext = viewModel = new ItemsViewModel(BreachInp, Order, "PwnCount");
            viewModel.LoadItemsCommand.Execute(null);
            breachEntry.Text = BreachInp;
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            BreachInp = breachEntry.Text;
            BindingContext = viewModel = new ItemsViewModel(BreachInp, Order, "AddedDate");
            viewModel.LoadItemsCommand.Execute(null);
            breachEntry.Text = BreachInp;
        }

        private void Search(object sender, EventArgs e)
        {
            BreachInp = breachEntry.Text;
            BindingContext = viewModel = new ItemsViewModel(BreachInp, Order, "AddedDate");
            viewModel.LoadItemsCommand.Execute(null);
            breachEntry.Text = BreachInp;
        }

        public async void Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new About());
        }
    }
}
