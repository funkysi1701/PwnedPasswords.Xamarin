using Autofac;
using Newtonsoft.Json;
using PwnedPass2.Interfaces;
using PwnedPass2.Models;
using System;
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
                var config = AppContainer.Container.Resolve<IConfiguration>();
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
                    string result = await App.GetAPI.GetHIBP(config.APIURL + "/api/v2/HIBP/GetBreaches");
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
            var config = AppContainer.Container.Resolve<IConfiguration>();
            var networkAccess = Connectivity.NetworkAccess;
            string result = null;
            long count = 0;
            if (networkAccess == NetworkAccess.Internet)
            {
                result = await App.GetAPI.GetHIBP(config.APIURL + "/api/v2/HIBP/GetBreaches");
            }
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
            var config = AppContainer.Container.Resolve<IConfiguration>();
            var networkAccess = Connectivity.NetworkAccess;
            string result = null;
            int count = 0;
            if (networkAccess == NetworkAccess.Internet)
            {
                result = await App.GetAPI.GetHIBP(config.APIURL + "/api/v2/HIBP/GetBreaches");
            }
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
