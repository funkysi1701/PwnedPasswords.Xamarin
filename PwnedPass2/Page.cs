using Autofac;
using PwnedPass2.Interfaces;
using PwnedPass2.Models;
using PwnedPasswords.Core;
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
        public static async Task<string> GetBreach()
        {
            DependencyService.Get<ILog>().SendTracking("Get Number of Breaches from Cache");
            int count = 0;
            try
            {
                var table = App.Database.GetHIBP();

                count = table.TotalBreaches;
            }
            catch (Exception e)
            {
                DependencyService.Get<ILog>().SendTracking("Error");
                DependencyService.Get<ILog>().SendTracking(e.Message, e);
                await Cache.SaveData();
            }
            if (count == 0)
            {
                count = await Cache.GetBreach();
            }
            return count.ToString() + " data breaches";
        }

        /// <summary>
        /// GetAccountsRaw
        /// </summary>
        /// <returns>long</returns>
        public static long GetAccountsRaw()
        {
            long count = 0;
            try
            {
                var table = App.Database.GetHIBP();

                count = table.TotalAccounts;
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
        public static async Task<string> GetAccounts()
        {
            DependencyService.Get<ILog>().SendTracking("Get Number of Accounts from Cache");
            long count = GetAccountsRaw();
            if (count == 0)
            {
                count = await Cache.GetAccounts();
            }
            return string.Format("{0:n0}", count) + " pwned accounts";
        }
    }
}
