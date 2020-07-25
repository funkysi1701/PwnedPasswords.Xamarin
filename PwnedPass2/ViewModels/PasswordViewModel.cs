using PwnedPass2.Interfaces;
using PwnedPass2.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PwnedPass2.ViewModels
{
    public class PasswordViewModel : BaseViewModel
    {
        public ObservableCollection<Passwords> Passwords { get; set; }
        public Command LoadPasswordsCommand { get; set; }

        public string PasswordInput { get; set; }
        public string DateSort { get; set; }
        public string NameSort { get; set; }
        public string CountSort { get; set; }
        public string Breach { get; set; }
        public string Account { get; set; }

        public PasswordViewModel(string PasswordInp)
        {
            Title = "';** pwned pass";
            PasswordInput = PasswordInp;
            Passwords = new ObservableCollection<Passwords>();
            LoadPasswordsCommand = new Command(async () => await ExecuteLoadItemsCommand(PasswordInput));
        }

        private async Task ExecuteLoadItemsCommand(string password)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Passwords.Clear();
                string hash = App.GetHash.Get(password);
                var items = await DataStore.GetPasswordAsync(hash);
                Passwords.Add(items);
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILog>().SendTracking("Error");
                DependencyService.Get<ILog>().SendTracking(ex.Message, ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}