using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Symbol.XamarinEMDK;
using Symbol.XamarinEMDK.Barcode;
using EDMKScannerProj.Droid.Services;
using Xamarin.Forms;
using EDMKScannerProj.Services;

namespace EDMKScannerProj.Droid
{
    [Activity(Label = "EDMKScannerProj", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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
            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();
            Scanner.InitScanner();
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