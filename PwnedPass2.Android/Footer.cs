using Plugin.CurrentActivity;
using PwnedPass2.Interfaces;
using PwnedPass2.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(PwnedPass2.Droid.Footer))]

namespace PwnedPass2.Droid
{
    public class Footer : IFooter
    {
        public void AddFooter(ItemsPage mainPage, StackLayout stack)
        {
            var context = CrossCurrentActivity.Current.Activity;
            var versioncode = new Label
            {
                Text = " Version: " + context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Small, mainPage),
            };
            var about = new Label
            {
                Text = "  ';** Pwned Pass created by Simon Foster.",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Small, mainPage),
            };
            stack.Children.Add(about);
            stack.Children.Add(versioncode);
        }

        public void AddFooter(EmailCheck mainPage, StackLayout stack)
        {
            var context = CrossCurrentActivity.Current.Activity;
            var versioncode = new Label
            {
                Text = " Version: " + context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Small, mainPage),
            };
            var about = new Label
            {
                Text = "  ';** Pwned Pass created by Simon Foster.",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Small, mainPage),
            };
            stack.Children.Add(about);
            stack.Children.Add(versioncode);
        }
    }
}