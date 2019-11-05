using Newtonsoft.Json;
using PwnedPass2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PwnedPass2.Services
{
    public class HIBPDataStore : IDataStore<HIBPModel>
    {
        public IEnumerable<HIBPModel> items;
        public IEnumerable<HIBPModel> emails;
        public string passwords;

        public HIBPDataStore()
        {
        }

        public async Task<HIBPModel> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Name == id));
        }

        public async Task<IEnumerable<HIBPModel>> GetItemsAsync(string orderby, bool orderdir, bool forceRefresh = false)
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
            items = OrderResults(items, orderby, orderdir);
            return await Task.FromResult(items);
        }

        private string GetCount(string output, string hash)
        {
            string[] lines = output.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            string count = "0";
            foreach (var item in lines)
            {
                if (item.Substring(0, 35) == hash.Substring(5))
                {
                    count = item.Substring(36);
                }
            }

            return count;
        }

        public async Task<Passwords> GetPasswordAsync(string hash)
        {
            string result = App.GetAPI.GetHIBP("https://pwnedpassapifsi.azurewebsites.net/api/v2/HIBP/CheckPasswords?hash=" + hash.Substring(0, 5));
            string count = this.GetCount(result, hash);
            var passwords = new Passwords();
            if (count == "0")
            {
                passwords.Text = "This password has not been indexed by haveibeenpwned.com";
                passwords.BgColor = Color.Green;
            }
            else
            {
                passwords.Text = "This password has previously appeared in a data breach " + count + " times and should never be used. ";
                passwords.BgColor = Color.Red;
            }
            return await Task.FromResult(passwords);
        }

        public async Task<IEnumerable<HIBPModel>> GetEmailsAsync(string emailsInp, string orderby, bool orderdir, bool forceRefresh = false)
        {
            string result = App.GetAPI.GetHIBP("https://pwnedpassapifsi.azurewebsites.net/api/v2/HIBP/CheckEmail?email=" + emailsInp);
            if (result != null && result.Length > 0)
            {
                var job = JsonConvert.DeserializeObject<HIBPResult>(result);
                foreach (var item in job.HIBP)
                {
                    item.Description = Regex.Replace(item.Description.ToString().Replace("&quot;", "'"), "<.*?>", string.Empty);
                }
                emails = job.HIBP.OrderByDescending(s => s.AddedDate).ToList();
            }
            emails = OrderResults(emails, orderby, orderdir);
            return await Task.FromResult(emails);
        }

        public static IEnumerable<HIBPModel> OrderResults(IEnumerable<HIBPModel> items, string orderby, bool orderdir)
        {
            GenericSort<HIBPModel> gs = new GenericSort<HIBPModel>();
            items = gs.Sort(items, orderby, orderdir).ToList();

            return items;
        }
    }
}