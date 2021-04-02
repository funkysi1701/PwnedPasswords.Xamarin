using Autofac;
using Newtonsoft.Json;
using PwnedPass2.Interfaces;
using PwnedPass2.Models;
using PwnedPasswords.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PwnedPass2
{
    public static class Cache
    {
        public static async Task SaveData()
        {
            try
            {
                HibpTotals data = new HibpTotals();
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
                    string result = await App.GetApi.GetHIBP("https://haveibeenpwned.com/api/v3/breaches");
                    if (result != null && result.Length > 0)
                    {
                        var job = JsonConvert.DeserializeObject<List<HIBP>>(result);
                        foreach (var item in job)
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
            var networkAccess = Connectivity.NetworkAccess;
            string result = null;
            long count = 0;
            if (networkAccess == NetworkAccess.Internet)
            {
                result = await App.GetApi.GetHIBP("https://haveibeenpwned.com/api/v3/breaches");
            }
            if (result != null && result.Length > 0)
            {
                var job = JsonConvert.DeserializeObject<List<HIBP>>(result);

                foreach (var item in job)
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
            var networkAccess = Connectivity.NetworkAccess;
            string result = null;
            int count = 0;
            if (networkAccess == NetworkAccess.Internet)
            {
                result = await App.GetApi.GetHIBP("https://haveibeenpwned.com/api/v3/breaches");
            }
            if (result != null && result.Length > 0)
            {
                var job = JsonConvert.DeserializeObject<List<HIBP>>(result);

                foreach (var item in job)
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
