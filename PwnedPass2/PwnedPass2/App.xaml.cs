using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PwnedPass2.Services;
using PwnedPass2.Views;
using PwnedPass2.Interfaces;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;

namespace PwnedPass2
{
    public partial class App : Application
    {
        public static IAPI GetAPI { get; private set; }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<HIBPDataStore>();
            MainPage = new MainPage();
        }

        public static void InitAPI(IAPI apiImplementation)
        {
            App.GetAPI = apiImplementation;
        }

        protected override void OnStart()
        {
            AppCenter.Start("uwp=f497a9fd-3c8b-4072-87ea-2b6e8d057a52;" + "android=29b4ff89-6554-4d25-bb78-93cd14a3b280;", typeof(Analytics));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}