using Newtonsoft.Json;
using PwnedPass2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PwnedPass2.Services
{
    public class HIBPDataStore : IDataStore<HIBPModel>
    {
        private readonly IList<HIBPModel> items;

        public HIBPDataStore()
        {
            string result = App.GetAPI.GetHIBP("https://pwnedpassapifsi.azurewebsites.net/api/v2/HIBP/GetBreaches");
            if (result != null && result.Length > 0)
            {
                var job = JsonConvert.DeserializeObject<HIBPResult>(result);
                foreach (var item in job.HIBP)
                {
                    item.Description = Regex.Replace(item.Description.ToString().Replace("&quot;", "'"), "<.*?>", string.Empty);
                }
                items = job.HIBP.OrderByDescending(s => s.AddedDate).ToList();
            }
        }

        public async Task<HIBPModel> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Name == id));
        }

        public async Task<IEnumerable<HIBPModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}