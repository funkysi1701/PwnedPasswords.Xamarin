using PwnedPass2.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(PwnedPass2.Android.AppRating))]
namespace PwnedPass2.Android
{
    public class AppRating : IAppRating
    {
        public void RateApp()
        {
            Plugin.StoreReview.CrossStoreReview.Current.OpenStoreReviewPage("pwnedpasswords.pwnedpasswords");
        }
    }
}
