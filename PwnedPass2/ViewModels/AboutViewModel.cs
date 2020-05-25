using PwnedPass2.Interfaces;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PwnedPass2.ViewModels
{
    public class AboutViewModel
    {
        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Launcher.OpenAsync(new Uri(url));
        });

        public ICommand RateCommand => new Command(() =>
        {
            DependencyService.Get<IAppRating>().RateApp();
        });
    }
}
