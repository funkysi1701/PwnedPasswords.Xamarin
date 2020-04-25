﻿using Microsoft.AppCenter.Crashes;
using PwnedPass2.Interfaces;
using System;
using System.Net.Http;
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
        public HttpResponseMessage GetAsyncAPI(string url)
        {
            HttpClient client = new HttpClient();
            DependencyService.Get<ILog>().SendTracking("GetAsyncAPI " + url);
            try
            {
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
        public string GetHIBP(string url)
        {
            try
            {
                HttpResponseMessage response = this.GetAsyncAPI(url);
                return response.Content.ReadAsStringAsync().Result;
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