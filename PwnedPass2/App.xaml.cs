﻿using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using PwnedPass2.Interfaces;
using PwnedPass2.Models;
using PwnedPass2.Services;
using PwnedPass2.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PwnedPass2
{
    public partial class App : Application
    {
        public static IApi GetApi { get; private set; }
        public static IHash GetHash { get; private set; }
        public static Database Database { get; set; }
        private static readonly Stopwatch stopWatch = new Stopwatch();
        private const int defaultTimespan = 5;
        private readonly string Url = "https://haveibeenpwned.com/api/v3/breaches";

        public App(AppSetup setup)
        {
            AppContainer.Container = setup.CreateContainer();
            InitializeComponent();

            DependencyService.Register<HibpDataStore>();
            MainPage = new MainPage();
        }

        public static Database GetDatabase()
        {
            if (Database == null)
            {
                Database = new Database();
            }

            return Database;
        }

        public static void InitAPI(IApi apiImplementation)
        {
            App.GetApi = apiImplementation;
        }

        public static void InitHash(IHash hashImplementation)
        {
            App.GetHash = hashImplementation;
        }

        protected override void OnStart()
        {
            AppCenter.Start("uwp=f497a9fd-3c8b-4072-87ea-2b6e8d057a52;" + "android=29b4ff89-6554-4d25-bb78-93cd14a3b280;", typeof(Analytics), typeof(Crashes));
            var item = new UniqueId();
            GetDatabase();
            App.Database.SaveUniqueId(item);
            if (!stopWatch.IsRunning)
            {
                stopWatch.Start();
            }

            Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                // Logic for logging out if the device is inactive for a period of time.

                if (stopWatch.IsRunning && stopWatch.Elapsed.Minutes >= defaultTimespan)
                {
                    //prepare to perform your data pull here as we have hit the 1 minute mark
                    Task.Run(async () =>
                    {
                        await Cache.SaveData(Url);
                    });
                    // Perform your long running operations here.

                    Xamarin.Forms.Device.InvokeOnMainThreadAsync(() =>
                    {
                        // If you need to do anything with your UI, you need to wrap it in this.
                    });

                    stopWatch.Restart();
                }

                // Always return true as to keep our device timer running.
                return true;
            });
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            stopWatch.Reset();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            stopWatch.Start();
        }
    }
}
