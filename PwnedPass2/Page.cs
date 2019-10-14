using PwnedPass2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PwnedPass2
{
    public static class Page
    {
        /// <summary>
        /// GetBreach
        /// </summary>
        /// <returns>string</returns>
        public static string GetBreach()
        {
            DependencyService.Get<ILog>().SendTracking("Get Number of Breaches from Cache");
            int count = 0;
            try
            {
                var table = App.Database.GetHIBP();

                foreach (var s in table)
                {
                    count = s.TotalBreaches;
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<ILog>().SendTracking("Error");
                DependencyService.Get<ILog>().SendTracking(e.Message, e);
            }

            return count.ToString() + " data breaches";
        }
    }
}