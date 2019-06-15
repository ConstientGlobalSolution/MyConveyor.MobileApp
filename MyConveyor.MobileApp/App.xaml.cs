using MyConveyor.MobileApp.Pages;
using MyConveyor.MobileApp.StaticClasses;
using Xamarin.Forms;

namespace MyConveyor.MobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SearchPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            if (!AppData.FileAccess.Exists("error.txt"))
            {
                AppData.FileAccess.Save("error.txt", "");
            }

            if (!AppData.FileAccess.Exists("log.txt"))
            {
                AppData.FileAccess.Save("log.txt", "Log Details on Exception: ");
            }

            if (!AppData.FileAccess.Exists("CartList.txt"))
            {
                AppData.FileAccess.Save("CartList.txt", "");
            }
            else
            {
                AppData.LoadCartList();
            }
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
