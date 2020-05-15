using Newtonsoft.Json;
using PwnedPass2.Interfaces;
using PwnedPass2.Models;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PwnedPass2
{
    public static class Cache
    {
        public static async Task SaveData()
        {
            try
            {
                HIBPTotals data = new HIBPTotals();
                long acc = await GetAccounts();
                int bre = await GetBreach();
                if (acc > 1)
                {
                    data.TotalAccounts = acc;
                }

                if (bre > 1)
                {
                    data.TotalBreaches = bre;
                }

                DependencyService.Get<ILog>().SendTracking("SAVE DB");
                if (acc > 1 && bre > 1)
                {
                    data.Id = 1;
                    App.Database.SaveHIBP(data);
                    App.Database.EmptyDataBreach();
                    string result = await App.GetAPI.GetHIBP("https://pwnedpassapifsi.azurewebsites.net/api/v2/HIBP/GetBreaches");
                    if (result != null && result.Length > 0)
                    {
                        var job = JsonConvert.DeserializeObject<HIBPResult>(result);
                        foreach (var item in job.HIBP)
                        {
                            App.Database.SaveDataBreach(item);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<ILog>().SendTracking("Error");
                DependencyService.Get<ILog>().SendTracking(e.Message, e);
            }
        }

        public static async Task<long> GetAccounts()
        {
            string result = await App.GetAPI.GetHIBP("https://pwnedpassapifsi.azurewebsites.net/api/v2/HIBP/GetBreaches");
            long count = 0;
            if (result != null && result.Length > 0)
            {
                var job = JsonConvert.DeserializeObject<HIBPResult>(result);

                foreach (var item in job.HIBP)
                {
                    count += item.PwnCount;
                }

                DependencyService.Get<ILog>().SendTracking("Get Number of Accounts");
            }
            else
            {
                var table = App.Database.GetHIBP();
                count = table.TotalAccounts;
            }

            return count;
        }

        public static async Task<int> GetBreach()
        {
            string result = await App.GetAPI.GetHIBP("https://pwnedpassapifsi.azurewebsites.net/api/v2/HIBP/GetBreaches");
            int count = 0;
            if (result != null && result.Length > 0)
            {
                var job = JsonConvert.DeserializeObject<HIBPResult>(result);

                foreach (var item in job.HIBP)
                {
                    count++;
                }

                DependencyService.Get<ILog>().SendTracking("Get Number of Breaches");
            }
            else
            {
                var table = App.Database.GetHIBP();
                count = table.TotalBreaches;
            }

            return count;
        }
    }
}