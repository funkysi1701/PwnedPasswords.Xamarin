using System;

using PwnedPass2.Models;

namespace PwnedPass2.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public HIBPModel Item { get; set; }

        public ItemDetailViewModel(HIBPModel item = null)
        {
            Title = item?.Name;
            Item = item;
        }
    }
}