using PwnedPass2.Interfaces;
using PwnedPass2.Models;
using PwnedPass2.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

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
            var lastemail = viewModel.LoadLastEmail();
            if (lastemail != null && !string.IsNullOrEmpty(lastemail.Email))
            {
                EmailInp = lastemail.Email;
                emailEntry.Text = EmailInp;
            }
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
            var lastemail = viewModel.LoadLastEmail();
            if (lastemail != null && !string.IsNullOrEmpty(lastemail.Email) && string.IsNullOrEmpty(EmailInp))
            {
                EmailInp = lastemail.Email;
            }
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
            var lastemail = viewModel.LoadLastEmail();
            if (lastemail != null && !string.IsNullOrEmpty(lastemail.Email) && string.IsNullOrEmpty(EmailInp))
            {
                EmailInp = lastemail.Email;
            }
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
            var lastemail = viewModel.LoadLastEmail();
            if (lastemail != null && !string.IsNullOrEmpty(lastemail.Email) && string.IsNullOrEmpty(EmailInp))
            {
                EmailInp = lastemail.Email;
            }
            viewModel.LoadEmailsCommand.Execute(null);
            emailEntry.Text = EmailInp;
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            EmailInp = emailEntry.Text;
            BindingContext = viewModel = new EmailViewModel(EmailInp, Order, "AddedDate");
            var lastemail = viewModel.LoadLastEmail();
            if (lastemail != null && !string.IsNullOrEmpty(lastemail.Email) && string.IsNullOrEmpty(EmailInp))
            {
                EmailInp = lastemail.Email;
            }
            viewModel.LoadEmailsCommand.Execute(null);
            emailEntry.Text = EmailInp;
        }

        private void Search(object sender, EventArgs e)
        {
            EmailInp = emailEntry.Text;
            BindingContext = viewModel = new EmailViewModel(EmailInp, Order, "AddedDate");
            var lastemail = viewModel.LoadLastEmail();
            if (lastemail != null && !string.IsNullOrEmpty(lastemail.Email) && string.IsNullOrEmpty(EmailInp))
            {
                EmailInp = lastemail.Email;
            }
            viewModel.LoadEmailsCommand.Execute(null);
            emailEntry.Text = EmailInp;
        }
    }
}