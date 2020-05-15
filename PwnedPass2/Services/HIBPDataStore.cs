using Newtonsoft.Json;
using PwnedPass2.Models;
using PwnedPasswords.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PwnedPass2.Services
{
    public class HIBPDataStore : IDataStore<HIBP>
    {
        public IEnumerable<HIBP> items;
        public IEnumerable<HIBP> emails;
        public string passwords;

        public HIBPDataStore()
        {
        }

        public async Task<HIBP> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Name == id));
        }

        public async Task<IEnumerable<HIBP>> GetItemsAsync(string orderby, bool orderdir, bool forceRefresh = false)
        {
            string result = await App.GetAPI.GetHIBP("https://pwnedpassapifsi.azurewebsites.net/api/v2/HIBP/GetBreaches");
            if (result != null && result.Length > 0)
            {
                var job = JsonConvert.DeserializeObject<HIBPResult>(result);
                foreach (var item in job.HIBP)
                {
                    item.Description = item.Description.ToString().Replace("&quot;", "'"); //Regex.Replace(, "<.*?>", string.Empty);
                }
                items = job.HIBP.OrderByDescending(s => s.AddedDate).ToList();
            }
            else
            {
                var table = App.Database.GetAll();
                var hibp = new List<HIBP>();
                
                foreach (var item in table)
                {
                    var temp = new HIBP
                    {
                        AddedDate = item.AddedDate,
                        BreachDate = item.BreachDate,
                        Name = item.Name,
                        Description = item.Description,
                        Title = item.Title,
                        PwnCount = item.PwnCount
                    };
                    hibp.Add(temp);
                }
                items = hibp;
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
            string result = await App.GetAPI.GetHIBP("https://pwnedpassapifsi.azurewebsites.net/api/v2/HIBP/CheckPasswords?hash=" + hash.Substring(0, 5));
            var passwords = new Passwords();
            if(string.IsNullOrEmpty(result))
            {
                passwords.Text = "No Connection, Please reconnect to the internet to check if this password has been pwned";
                passwords.BgColor = Color.DarkOrange;
                return await Task.FromResult(passwords);
            }
            string count = this.GetCount(result, hash);
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

        public async Task<IEnumerable<HIBP>> GetEmailsAsync(string emailsInp, string orderby, bool orderdir, bool forceRefresh = false)
        {
            string result = await App.GetAPI.GetHIBP("https://pwnedpassapifsi.azurewebsites.net/api/v2/HIBP/CheckEmail?email=" + emailsInp);
            if (result != null && result.Length > 0)
            {
                var job = JsonConvert.DeserializeObject<HIBPResult>(result);
                foreach (var item in job.HIBP)
                {
                    item.Description = Regex.Replace(item.Description.ToString().Replace("&quot;", "'"), "<.*?>", string.Empty);
                }
                emails = job.HIBP.OrderByDescending(s => s.AddedDate).ToList();
            }
            else
            {
                var error = new HIBP
                {
                    Title = "No Connection",
                    Description = "Please reconnect to the internet to check if this email has been pwned",
                    PwnCount = 0,
                    AddedDate = DateTime.UtcNow
                };

                var emailsmodel = new List<HIBP>
                {
                    error
                };
                emails = emailsmodel;
            }
            emails = OrderResults(emails, orderby, orderdir);
            return await Task.FromResult(emails);
        }

        public static IEnumerable<HIBP> OrderResults(IEnumerable<HIBP> items, string orderby, bool orderdir)
        {
            GenericSort<HIBP> gs = new GenericSort<HIBP>();
            items = gs.Sort(items, orderby, orderdir).ToList();

            return items;
        }
    }
}