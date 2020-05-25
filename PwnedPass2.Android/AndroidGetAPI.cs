using Microsoft.AppCenter.Crashes;
using Plugin.CurrentActivity;
using PwnedPass2.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PwnedPass2.Droid
{
    public class AndroidGetAPI : IAPI
    {
        /// <summary>
        /// GetAsyncAPI.
        /// </summary>
        /// <param name="url">url goes here.</param>
        /// <returns>HttpResponseMessage.</returns>
        public HttpResponseMessage GetAsyncAPI(string url,string version)
        {
            HttpClient client = new HttpClient();
            DependencyService.Get<ILog>().SendTracking("GetAsyncAPI " + url);
            try
            {
                client.DefaultRequestHeaders.Add("Version", version);
                return client.GetAsync(url).Result;
            }
            catch (Exception e)
            {
                DependencyService.Get<ILog>().SendTracking(e.Message, e);
                Crashes.TrackError(e);
                return new HttpResponseMessage();
            }
        }

        /// <summary>
        /// GetHIBP.
        /// </summary>
        /// <param name="url">url goes here.</param>
        /// <returns>string.</returns>
        public async Task<string> GetHIBP(string url)
        {
            try
            {
                var crosscontext = CrossCurrentActivity.Current.Activity;
                var version = crosscontext.PackageManager.GetPackageInfo(crosscontext.PackageName, 0).VersionName;
                HttpResponseMessage response = GetAsyncAPI(url, version);
                if (response.Content == null)
                {
                    return string.Empty;
                }
                return await response.Content?.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                DependencyService.Get<ILog>().SendTracking("Error");
                Crashes.TrackError(e);
                DependencyService.Get<ILog>().SendTracking(e.Message, e);
                DependencyService.Get<ILog>().SendTracking("Details");
                return null;
            }
        }
    }
}
