using Android.App;
using Android.Support.V7.App;

namespace PwnedPass2.Android
{
    [Activity(Label = "Pwned Pass", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            this.StartActivity(typeof(MainActivity));
        }
    }
}