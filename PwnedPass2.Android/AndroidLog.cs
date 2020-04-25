﻿using Microsoft.AppCenter.Analytics;
using Plugin.CurrentActivity;
using PwnedPass2.Interfaces;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

[assembly: Dependency(typeof(PwnedPass2.Droid.AndroidLog))]

namespace PwnedPass2.Droid
{
    public class AndroidLog : ILog
    {
        /// <summary>
        /// SendTracking.
        /// </summary>
        /// <param name="message">message.</param>
        public void SendTracking(string message)
        {
            var crosscontext = CrossCurrentActivity.Current.Activity;
            var details = new Dictionary<string, string>
                {
                        { "VersionName", crosscontext.PackageManager.GetPackageInfo(crosscontext.PackageName, 0).VersionName },
                        { "LastUpdateTime", crosscontext.PackageManager.GetPackageInfo(crosscontext.PackageName, 0).LastUpdateTime.ToString() },
                        { "PackageName", crosscontext.PackageManager.GetPackageInfo(crosscontext.PackageName, 0).PackageName },
                };
            Analytics.TrackEvent(message, details);
        }

        /// <summary>
        /// SendTracking.
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="e">Exception.</param>
        public void SendTracking(string message, Exception e)
        {
            var crosscontext = CrossCurrentActivity.Current.Activity;
            var details = new Dictionary<string, string>
                {
                        { "VersionName", crosscontext.PackageManager.GetPackageInfo(crosscontext.PackageName, 0).VersionName },
                        { "LastUpdateTime", crosscontext.PackageManager.GetPackageInfo(crosscontext.PackageName, 0).LastUpdateTime.ToString() },
                        { "PackageName", crosscontext.PackageManager.GetPackageInfo(crosscontext.PackageName, 0).PackageName },
                        { "StackTrace", e.StackTrace },
                        { "Revision", e.InnerException.Message },
                };
            Analytics.TrackEvent(message, details);
        }
    }
}