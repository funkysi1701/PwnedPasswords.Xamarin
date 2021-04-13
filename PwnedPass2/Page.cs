using PwnedPass2.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PwnedPass2
{
    public static class Page
    {
        /// <summary>
        /// GetBreach
        /// </summary>
        /// <returns>string</returns>
        public static async Task<string> GetBreach(string url)
        {
            DependencyService.Get<ILog>().SendTracking("Get Number of Breaches from Cache");
            int count = 0;
            try
            {
                count = await Cache.GetBreach(url);
            }
            catch (Exception e)
            {
                DependencyService.Get<ILog>().SendTracking("Error");
                DependencyService.Get<ILog>().SendTracking(e.Message, e);
                await Cache.SaveData(url);
            }
            return count.ToString() + " data breaches";
        }

        /// <summary>
        /// GetAccountsRaw
        /// </summary>
        /// <returns>long</returns>
        public static async Task<long> GetAccountsRaw(string url)
        {
            long count = 0;
            try
            {
                count = await Cache.GetAccounts(url);
            }
            catch (Exception e)
            {
                DependencyService.Get<ILog>().SendTracking("Error");
                DependencyService.Get<ILog>().SendTracking(e.Message, e);
            }

            return count;
        }

        /// <summary>
        /// GetAccounts
        /// </summary>
        /// <returns>string</returns>
        public static async Task<string> GetAccounts(string url)
        {
            DependencyService.Get<ILog>().SendTracking("Get Number of Accounts from Cache");
            long count = await GetAccountsRaw(url);
            return string.Format("{0:n0}", count) + " pwned accounts";
        }
    }
}
