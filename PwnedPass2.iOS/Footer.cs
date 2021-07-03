using System;
using Autofac;
using PwnedPass2.Interfaces;
using PwnedPass2.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(PwnedPass2.iOS.Footer))]
namespace PwnedPass2.iOS
{
    public class Footer : IFooter
    {


        private readonly string beta;

        public Footer()
        {
            IConfiguration config = AppContainer.Container.Resolve<IConfiguration>();
            beta = string.Empty;
            if (config.Beta)
            {
                beta = " Beta Version";
            }
        }

        public void AddFooter(ItemsPage mainPage, StackLayout stack)
        {
            //var context = CrossCurrentActivity.Current.Activity;
            var about = new Button
            {
                Text = "  ';** Pwned Pass created by Simon Foster. \r\nVersion: " ,
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, mainPage),
                TextTransform = TextTransform.None
            };

            about.Clicked += mainPage.Clicked;
            stack.Children.Add(about);
        }

        public void AddFooter(EmailCheck mainPage, StackLayout stack)
        {
            //var context = CrossCurrentActivity.Current.Activity;
            var about = new Button
            {
                Text = "  ';** Pwned Pass created by Simon Foster. \r\nVersion: " ,
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, mainPage),
                TextTransform = TextTransform.None
            };

            about.Clicked += mainPage.Clicked;
            stack.Children.Add(about);
        }

        public void AddFooter(PasswordCheck mainPage, StackLayout stack)
        {
            //var context = CrossCurrentActivity.Current.Activity;
            var about = new Button
            {
                Text = "  ';** Pwned Pass created by Simon Foster. \r\nVersion: " ,
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, mainPage),
                TextTransform = TextTransform.None
            };

            about.Clicked += mainPage.Clicked;
            stack.Children.Add(about);
        }
    }
}
