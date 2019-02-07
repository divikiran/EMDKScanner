
using Android.App;
using Android.Content.PM;
using Android.OS;
using EDMKScannerProj.Droid.Services;
using Symbol.XamarinEMDK;
using Xamarin.Forms;

namespace EDMKScannerProj.Droid
{
    [Activity(Name = "EDMKName.MainActivity", Label = "EDMKScannerProj", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        public ScannerService Scanner { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Scanner = DependencyService.Get<ScannerService>();


            var results = EMDKManager.GetEMDKManager(Android.App.Application.Context, Scanner);
            if (results.StatusCode != EMDKResults.STATUS_CODE.Success)
            {
                //statusView.Text = "Status: EMDKManager object creation failed ...";
            }
            else
            {
                //statusView.Text = "Status: EMDKManager object creation succeeded ...";
            }
            //App application = new App();
            App.Scanner = Scanner;
            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();
            Scanner.InitScanner();
            //InitScanner();
        }

        protected override void OnPause()
        {
            base.OnPause();
            Scanner.DeinitScanner();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Scanner.Destroy();
        }
    }
}