using Autofac;
using Plugin.CurrentActivity;
using PwnedPass2.Interfaces;
using PwnedPass2.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(PwnedPass2.Droid.Footer))]

namespace PwnedPass2.Droid
{
    public class Footer : IFooter
    {
        private readonly IConfiguration config;
        private readonly string beta;
        public Footer()
        {
            config = AppContainer.Container.Resolve<IConfiguration>();
            beta = string.Empty;
            if (config.Beta)
            {
                beta = " Beta Version";
            }
        }
        public void AddFooter(ItemsPage mainPage, StackLayout stack)
        {
            var context = CrossCurrentActivity.Current.Activity;
            var about = new Button
            {
                Text = "  ';** Pwned Pass created by Simon Foster. \r\nVersion: " + context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName + beta,
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, mainPage),
            };

            about.Clicked += mainPage.Clicked;
            stack.Children.Add(about);
        }

        public void AddFooter(EmailCheck mainPage, StackLayout stack)
        {
            var context = CrossCurrentActivity.Current.Activity;
            var about = new Button
            {
                Text = "  ';** Pwned Pass created by Simon Foster. \r\nVersion: " + context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName + beta,
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, mainPage),
            };

            about.Clicked += mainPage.Clicked;
            stack.Children.Add(about);
        }

        public void AddFooter(PasswordCheck mainPage, StackLayout stack)
        {
            var context = CrossCurrentActivity.Current.Activity;
            var about = new Button
            {
                Text = "  ';** Pwned Pass created by Simon Foster. \r\nVersion: " + context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName + beta,
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, mainPage),
            };

            about.Clicked += mainPage.Clicked;
            stack.Children.Add(about);
        }
    }
}
