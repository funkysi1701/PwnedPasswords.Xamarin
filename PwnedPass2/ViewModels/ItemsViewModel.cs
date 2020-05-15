using PwnedPass2.Models;
using PwnedPasswords.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PwnedPass2.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<HIBP> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public string DateSort { get; set; }
        public string NameSort { get; set; }
        public string CountSort { get; set; }
        public string Breach { get; set; }
        public string Account { get; set; }

        public ItemsViewModel(string filter, bool order, string orderby)
        {
            Title = "';** pwned pass";
            Items = new ObservableCollection<HIBP>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(order, orderby, filter));
            SetSort(order, orderby);
            SetSort(order, orderby);
            SetSort(order, orderby);
            Breach = SetBreach().Result;
            Account = SetAccount().Result;
        }

        private async Task<string> SetAccount()
        {
            return await Page.GetAccounts();
        }

        private async Task<string> SetBreach()
        {
            return await Page.GetBreach();
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

        private async Task ExecuteLoadItemsCommand(bool sortdir, string orderby, string filter)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(orderby, sortdir, true);
                if (filter != null)
                {
                    items = items.Where(x => x.Description.ToLower().Contains(filter.ToLower()) || x.Title.ToLower().Contains(filter.ToLower()) || x.Name.ToLower().Contains(filter.ToLower()));
                }

                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}