using PwnedPass2.Models;
using PwnedPasswords.Core;

namespace PwnedPass2.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public HIBP Item { get; set; }

        public ItemDetailViewModel(HIBP item = null)
        {
            Title = item?.Name;
            Item = item;
        }
    }
}
