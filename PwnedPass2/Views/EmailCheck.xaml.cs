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
        public bool Order { get; set; }

        public string EmailInp { get; set; }

        public EmailCheck()
        {
            InitializeComponent();

            BindingContext = viewModel = new EmailViewModel(EmailInp, true, "AddedDate");
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

            if (viewModel.Emails.Count == 0)
                viewModel.LoadEmailsCommand.Execute(null);
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
            BindingContext = viewModel = new EmailViewModel(EmailInp, Order, "AddedDate");
            viewModel.LoadEmailsCommand.Execute(null);
            emailEntry.Text = EmailInp;
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
            BindingContext = viewModel = new EmailViewModel(EmailInp, Order, "Name");
            viewModel.LoadEmailsCommand.Execute(null);
            emailEntry.Text = EmailInp;
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
            BindingContext = viewModel = new EmailViewModel(EmailInp, Order, "PwnCount");
            viewModel.LoadEmailsCommand.Execute(null);
            emailEntry.Text = EmailInp;
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            EmailInp = emailEntry.Text;
            BindingContext = viewModel = new EmailViewModel(EmailInp, Order, "AddedDate");
            viewModel.LoadEmailsCommand.Execute(null);
            emailEntry.Text = EmailInp;
        }

        private void Search(object sender, EventArgs e)
        {
            EmailInp = emailEntry.Text;
            BindingContext = viewModel = new EmailViewModel(EmailInp, Order, "AddedDate");
            viewModel.LoadEmailsCommand.Execute(null);
            emailEntry.Text = EmailInp;
        }
    }
}