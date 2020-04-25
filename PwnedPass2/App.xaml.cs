using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using PwnedPass2.Interfaces;
using PwnedPass2.Models;
using PwnedPass2.Services;
using PwnedPass2.Views;
using Xamarin.Forms;

namespace PwnedPass2
{
    public partial class App : Application
    {
        public static IAPI GetAPI { get; private set; }
        public static IHash GetHash { get; private set; }
        private static Database database;

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
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
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
        }
    }
}