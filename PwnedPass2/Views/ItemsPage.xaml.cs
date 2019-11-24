using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PwnedPass2.Models;
using PwnedPass2.Views;
using PwnedPass2.ViewModels;
using PwnedPass2.Interfaces;

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
            BindingContext = viewModel = new ItemsViewModel(BreachInp, true, "AddedDate");
            DependencyService.Get<IFooter>().AddFooter(this, this.stack);
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is HIBPModel item))
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
    }
}