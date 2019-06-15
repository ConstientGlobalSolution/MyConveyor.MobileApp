using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using MyConveyor.MobileApp.Classes;
using MyConveyor.MobileApp.StaticClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MyConveyor.MobileApp.Android
{
    [Activity(Label = "MyConveyor.MobileApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const int REQUESTCODESTORAGE = 3;
        private const int REQUESTGROUPPERMISSION = 5;

        private void RequestPermission()
        {
            if (Build.VERSION.SdkInt < global::Android.OS.BuildVersionCodes.M)
            {
                return;
            }
            //
            RequestAllPermissions();
        }

        private void RequestAllPermissions()
        {
            List<string> needPermission = new List<string>();
            List<string> permissions = new List<string>()
                {
                    Manifest.Permission.ReadExternalStorage,
                    Manifest.Permission.WriteExternalStorage,
                    Manifest.Permission.ReadPhoneState,
                    Manifest.Permission.Internet
                };

            foreach (string permission in permissions)
            {
                if (ContextCompat.CheckSelfPermission(this, permission) != Permission.Granted)
                {
                    needPermission.Add(permission);
                }
            }

            if (needPermission.Count > 0)
            {
                RequestGroupPermission(needPermission);
            }
        }

        private void RequestGroupPermission(List<string> permissions)
        {
            string[] permissionsNeed = permissions.ToArray();
            ActivityCompat.RequestPermissions(this, permissionsNeed, REQUESTGROUPPERMISSION);
        }

        private void HandleUnhandledException(object sender, RaiseThrowableEventArgs e)
        {
            IOException ioex;
            Exception ex;
            Task task;
            ErrorReport report;
            StringBuilder sb;
            try
            {
                ioex = e.Exception as IOException;
                ex = e.Exception;
                if (ioex != null)
                {
                    report = new ErrorReport("Unhandled Native Exception", ioex);
                }
                else if (ex != null)
                {
                    report = new ErrorReport("Unhandled Managed Exception", ex);
                }
                else
                {
                    report = new ErrorReport("Unhandled Unrecognised Exception", e.ToString());
                }

                // Make a record of the state of the data when the error occurred.
                sb = new StringBuilder();
                sb.AppendLine();
                report.AddFurtherInfo(sb.ToString());
                task = new Task(delegate { Writingfile(report); });
                task.Start();
                task.Wait(1000);
            }
            catch (Exception WriteError)
            {
                report = new ErrorReport("Error File Creation Exception", WriteError.ToString());
                task = new Task(delegate { Writingfile(report); });
                task.Start();
                task.Wait(1000);
            }
        }

        private static void Writingfile(ErrorReport report)
        {
            AppData.FileAccess.Save("error.txt", report.Serialised);
        }

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (Build.VERSION.SdkInt < global::Android.OS.BuildVersionCodes.M)
            {
                return;
            }

            switch (requestCode)
            {
                case REQUESTCODESTORAGE:
                    if (grantResults.Length > 0 && grantResults[0] == Permission.Denied)
                    {
                        if (ShouldShowRequestPermissionRationale(Manifest.Permission.ReadExternalStorage))
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", "You need to allow the storage permissions for the App to access the device storage.", "Ok");
                            RequestPermissions(new string[] { Manifest.Permission.Camera }, REQUESTCODESTORAGE);
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", "You need to allow the storage permissions for the App to access the device storage, Please open settings and enable storage permission.", "Open Settings");
                            StartActivity(new Intent(global::Android.Provider.Settings.ActionApplicationDetailsSettings,
                                global::Android.Net.Uri.Parse("package:" + global::Android.App.Application.Context.PackageName)));
                        }
                    }

                    break;
                case REQUESTGROUPPERMISSION:
                    foreach (string permission in permissions)
                    {
                        if ((permission == Manifest.Permission.ReadExternalStorage || permission == Manifest.Permission.WriteExternalStorage) && (ContextCompat.CheckSelfPermission(this, permission) != Permission.Granted))
                        {
                            RequestPermissions(new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage }, REQUESTCODESTORAGE);
                        }

                    }
                    break;
                default:
                    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
                    break;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            AndroidEnvironment.UnhandledExceptionRaiser += HandleUnhandledException;
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
            RequestPermission();
            LoadApplication(new App());
        }

        public override async void OnBackPressed()
        {
            try
            {
                if (App.Current.MainPage.Navigation.ModalStack.Count < 1)
                {
                    if (await App.Current.MainPage.DisplayAlert("Exit", "Do you want to exit the application?", "YES", "NO"))
                    {
                        int pid = global::Android.OS.Process.MyPid();
                        global::Android.OS.Process.KillProcess(pid);
                        base.OnBackPressed();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

    }
}

