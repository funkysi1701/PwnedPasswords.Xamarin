using Autofac;
using PwnedPass2.Interfaces;
using PwnedPass2.Models;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PwnedPass2.ViewModels
{
    public class AboutViewModel
    {
        public bool Beta
        {
            get
            {
                var config = AppContainer.Container.Resolve<IConfiguration>();
                return config.Beta;
            }
        }

        public int BetaInt
        {
            get
            {
                if(Beta)
                {
                    return 1;
                }
                else return 0;
            }
        }

        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Launcher.OpenAsync(new Uri(url));
        });

        public ICommand RateCommand => new Command(() =>
        {
            DependencyService.Get<IAppRating>().RateApp();
        });

        public ICommand IncreaseCounts => new Command(() =>
        {
            var config = AppContainer.Container.Resolve<IConfiguration>();
            if (config.Beta)
            {
                var table = App.Database.GetHIBP();
                var acc = table.TotalAccounts;
                var breach = table.TotalBreaches;
                acc += 1;
                breach += 1;
                var data = new HIBPTotals
                {
                    TotalBreaches = breach,
                    TotalAccounts = acc
                };
                App.Database.SaveHIBP(data);
            }
        });
    }
}
