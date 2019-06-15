using Android.App;
using Android.OS;

namespace MyConveyor.MobileApp.Android
{
    [Activity(Theme = "@style/MyTheme.Splash", Label = "MyConveyor.MobileApp", MainLauncher = true, NoHistory = true)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
        }
    }
}