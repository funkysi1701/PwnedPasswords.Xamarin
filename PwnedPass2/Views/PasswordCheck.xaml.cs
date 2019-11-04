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
    public partial class PasswordCheck : ContentPage
    {
        private PasswordViewModel viewModel;
        public bool order { get; set; }

        public string PasswordInp { get; set; }

        public PasswordCheck()
        {
            InitializeComponent();

            BindingContext = viewModel = new PasswordViewModel(PasswordInp, true, "AddedDate");
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

            if (viewModel.Passwords.Count == 0)
                viewModel.LoadPasswordsCommand.Execute(null);
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
            BindingContext = viewModel = new PasswordViewModel(PasswordInp, order, "AddedDate");
            viewModel.LoadPasswordsCommand.Execute(null);
            passwordEntry.Text = PasswordInp;
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
            BindingContext = viewModel = new PasswordViewModel(PasswordInp, order, "Name");
            viewModel.LoadPasswordsCommand.Execute(null);
            passwordEntry.Text = PasswordInp;
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
            BindingContext = viewModel = new PasswordViewModel(PasswordInp, order, "PwnCount");
            viewModel.LoadPasswordsCommand.Execute(null);
            passwordEntry.Text = PasswordInp;
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            PasswordInp = passwordEntry.Text;
            BindingContext = viewModel = new PasswordViewModel(PasswordInp, order, "AddedDate");
            viewModel.LoadPasswordsCommand.Execute(null);
            passwordEntry.Text = PasswordInp;
        }
    }
}