﻿using Microsoft.AppCenter.Crashes;
using Plugin.CurrentActivity;
using PwnedPass2.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PwnedPass2.Android
{
    public class GetApi : IApi
    {
        /// <summary>
        /// GetAsyncAPI.
        /// </summary>
        /// <param name="url">url goes here.</param>
        /// <returns>HttpResponseMessage.</returns>
        public HttpResponseMessage GetAsyncAPI(string url, string version)
        {
            HttpClient client = new HttpClient();
            DependencyService.Get<ILog>().SendTracking("GetAsyncAPI " + url);
            try
            {
                client = GetHeaders(client, url, version);

                return client.GetAsync(url).Result;
            }
            catch (Exception e)
            {
                DependencyService.Get<ILog>().SendTracking(e.Message, e);
                Crashes.TrackError(e);
                return new HttpResponseMessage();
            }
        }

        private HttpClient GetDeviceInfoHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Idiom", DeviceInfo.Idiom.ToString());
            client.DefaultRequestHeaders.Add("DeviceType", DeviceInfo.DeviceType.ToString());
            client.DefaultRequestHeaders.Add("Manufacturer", DeviceInfo.Manufacturer);
            client.DefaultRequestHeaders.Add("Model", DeviceInfo.Model);
            client.DefaultRequestHeaders.Add("Name", DeviceInfo.Name);
            client.DefaultRequestHeaders.Add("Platform", DeviceInfo.Platform.ToString());
            client.DefaultRequestHeaders.Add("OSVersionString", DeviceInfo.VersionString);

            return client;
        }

        private HttpClient GetDeviceDisplayHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Density", DeviceDisplay.MainDisplayInfo.Density.ToString());
            client.DefaultRequestHeaders.Add("Height", DeviceDisplay.MainDisplayInfo.Height.ToString());
            client.DefaultRequestHeaders.Add("Orientation", DeviceDisplay.MainDisplayInfo.Orientation.ToString());
            client.DefaultRequestHeaders.Add("Rotation", DeviceDisplay.MainDisplayInfo.Rotation.ToString());
            client.DefaultRequestHeaders.Add("Width", DeviceDisplay.MainDisplayInfo.Width.ToString());

            return client;
        }

        private HttpClient GetHeaders(HttpClient client, string url, string version)
        {
            client.DefaultRequestHeaders.Add("Id", App.Database.GetUniqueId().Id.ToString());
            client.DefaultRequestHeaders.Add("Version", version);
            client.DefaultRequestHeaders.Add("URL", url);
            client = GetDeviceInfoHeaders(client);
            client = GetDeviceDisplayHeaders(client);
            client.DefaultRequestHeaders.Add("BuildString", AppInfo.BuildString);
            client.DefaultRequestHeaders.Add("AppName", AppInfo.Name);
            client.DefaultRequestHeaders.Add("PackageName", AppInfo.PackageName);
            client.DefaultRequestHeaders.Add("AppBuildString", AppInfo.BuildString);
            return client;
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
