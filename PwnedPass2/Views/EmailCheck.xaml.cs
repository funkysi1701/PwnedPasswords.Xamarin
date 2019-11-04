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
    public partial class EmailCheck : ContentPage
    {
        private EmailViewModel viewModel;
        public bool order { get; set; }

        public EmailCheck()
        {
            InitializeComponent();

            BindingContext = viewModel = new EmailViewModel(true, "AddedDate");
            DependencyService.Get<IFooter>().AddFooter(this, this.stack);
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as HIBPModel;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Emails.Count == 0)
                viewModel.LoadEmailsCommand.Execute(null);
        }

        private void SortClickedDate(object sender, EventArgs e)
        {
            if (order)
            {
                order = false;
            }
            else
            {
                order = true;
            }
            BindingContext = viewModel = new EmailViewModel(order, "AddedDate");
            viewModel.LoadEmailsCommand.Execute(null);
        }

        private void SortClickedName(object sender, EventArgs e)
        {
            if (order)
            {
                order = false;
            }
            else
            {
                order = true;
            }
            BindingContext = viewModel = new EmailViewModel(order, "Name");
            viewModel.LoadEmailsCommand.Execute(null);
        }

        private void SortClickedPwned(object sender, EventArgs e)
        {
            if (order)
            {
                order = false;
            }
            else
            {
                order = true;
            }
            BindingContext = viewModel = new EmailViewModel(order, "PwnCount");
            viewModel.LoadEmailsCommand.Execute(null);
        }
    }
}