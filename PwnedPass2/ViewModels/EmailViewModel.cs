using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using PwnedPass2.Models;
using PwnedPass2.Views;

namespace PwnedPass2.ViewModels
{
    public class EmailViewModel : BaseViewModel
    {
        public ObservableCollection<HIBPModel> Emails { get; set; }
        public Command LoadEmailsCommand { get; set; }
        public string DateSort { get; set; }
        public string NameSort { get; set; }
        public string CountSort { get; set; }
        public string Breach { get; set; }
        public string Account { get; set; }

        public EmailViewModel(bool order, string orderby)
        {
            Title = "';** pwned pass";
            Emails = new ObservableCollection<HIBPModel>();
            LoadEmailsCommand = new Command(async () => await ExecuteLoadItemsCommand("funkysi1701@gmail.com", order, orderby));
            SetSort(order, orderby);
            SetSort(order, orderby);
            SetSort(order, orderby);
            Breach = SetBreach();
            Account = SetAccount();
        }

        private string SetAccount()
        {
            return Page.GetAccounts();
        }

        private string SetBreach()
        {
            return Page.GetBreach();
        }

        private void SetSort(bool order, string orderby)
        {
            if (orderby == "AddedDate")
            {
                if (order)
                {
                    DateSort = "^ Date";
                    NameSort = "Name";
                    CountSort = "Pwned Acc";
                }
                else
                {
                    DateSort = "v Date";
                    NameSort = "Name";
                    CountSort = "Pwned Acc";
                }
            }
            if (orderby == "Name")
            {
                if (order)
                {
                    DateSort = "Date";
                    NameSort = "^ Name";
                    CountSort = "Pwned Acc";
                }
                else
                {
                    DateSort = "Date";
                    NameSort = "v Name";
                    CountSort = "Pwned Acc";
                }
            }
            if (orderby == "PwnCount")
            {
                if (order)
                {
                    DateSort = "Date";
                    NameSort = "Name";
                    CountSort = "^ Pwned Acc";
                }
                else
                {
                    DateSort = "Date";
                    NameSort = "Name";
                    CountSort = "v Pwned Acc";
                }
            }
        }

        private async Task ExecuteLoadItemsCommand(string email, bool sortdir, string orderby)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Emails.Clear();
                var items = await DataStore.GetEmailsAsync(email, orderby, sortdir, true);
                foreach (var item in items)
                {
                    Emails.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}