using PwnedPass2.Interfaces;
using PwnedPass2.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace PwnedPass2.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PasswordCheck : ContentPage
    {
        private PasswordViewModel viewModel;

        public string PasswordInp { get; set; }

        public PasswordCheck()
        {
            InitializeComponent();

            BindingContext = viewModel = new PasswordViewModel(PasswordInp);
            DependencyService.Get<IFooter>().AddFooter(this, this.stack);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Passwords.Count == 0)
                viewModel.LoadPasswordsCommand.Execute(null);
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            PasswordInp = passwordEntry.Text;
            BindingContext = viewModel = new PasswordViewModel(PasswordInp);
            viewModel.LoadPasswordsCommand.Execute(null);
            passwordEntry.Text = PasswordInp;
        }

        public async void Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new About());
        }
    }
}
