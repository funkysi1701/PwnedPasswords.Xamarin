using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using PwnedPass2.Models;
using PwnedPass2.Views;

namespace PwnedPass2.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<HIBPModel> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public string DateSort { get; set; }
        public string NameSort { get; set; }
        public string CountSort { get; set; }
        public string Breach { get; set; }

        public ItemsViewModel(bool order, string orderby)
        {
            Title = "';** pwned pass";
            Items = new ObservableCollection<HIBPModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(order, orderby));
            SetSort(order, orderby);
            SetSort(order, orderby);
            SetSort(order, orderby);
            Breach = SetBreach();
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

        private async Task ExecuteLoadItemsCommand(bool sortdir, string orderby)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(orderby, sortdir, true);
                foreach (var item in items)
                {
                    Items.Add(item);
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