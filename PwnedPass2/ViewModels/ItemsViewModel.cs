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

        public ItemsViewModel(bool order, string orderby)
        {
            Title = "';** pwned pass";
            Items = new ObservableCollection<HIBPModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(order, orderby));
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