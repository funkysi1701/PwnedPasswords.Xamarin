using System;
using PwnedPass2.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(PwnedPass2.iOS.Log))]
namespace PwnedPass2.iOS
{
    public class Log : ILog
    {
        

        public void SendTracking(string message)
        {
            //var crosscontext = CrossCurrentActivity.Current.Activity;
            //var details = new Dictionary<string, string>
                //{
                //        { "VersionName", crosscontext.PackageManager.GetPackageInfo(crosscontext.PackageName, 0).VersionName },
                //        { "LastUpdateTime", crosscontext.PackageManager.GetPackageInfo(crosscontext.PackageName, 0).LastUpdateTime.ToString() },
                //        { "PackageName", crosscontext.PackageManager.GetPackageInfo(crosscontext.PackageName, 0).PackageName },
                //};
            //Analytics.TrackEvent(message, details);
        }

        public void SendTracking(string message, Exception e)
        {
            //var crosscontext = CrossCurrentActivity.Current.Activity;
            //var details = new Dictionary<string, string>
                //{
                //        { "VersionName", crosscontext.PackageManager.GetPackageInfo(crosscontext.PackageName, 0).VersionName },
                //        { "LastUpdateTime", crosscontext.PackageManager.GetPackageInfo(crosscontext.PackageName, 0).LastUpdateTime.ToString() },
                //        { "PackageName", crosscontext.PackageManager.GetPackageInfo(crosscontext.PackageName, 0).PackageName },
                //        { "StackTrace", e?.StackTrace },
                //        { "Revision", e?.InnerException?.Message },
                //};
            //Analytics.TrackEvent(message, details);
        }
    }
}
