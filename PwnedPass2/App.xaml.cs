using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
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
        public static IAPI GetAPI { get; private set; }
        public static IHash GetHash { get; private set; }
        private static Database database;
        private static Stopwatch stopWatch = new Stopwatch();
        private const int defaultTimespan = 35;

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

        public static void InitHash(IHash hashImplementation)
        {
            App.GetHash = hashImplementation;
        }

        protected override void OnStart()
        {
            AppCenter.Start("uwp=f497a9fd-3c8b-4072-87ea-2b6e8d057a52;" + "android=29b4ff89-6554-4d25-bb78-93cd14a3b280;", typeof(Analytics));
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
                        Cache.SaveData();
                    });
                    // Perform your long running operations here.

                    Xamarin.Forms.Device.InvokeOnMainThreadAsync(() => {
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

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database();
                }

                return database;
            }
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            stopWatch.Start();
        }
    }
}
