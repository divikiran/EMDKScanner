using EDMKScannerProj.Services;
using EDMKScannerProj.Viewmodels;
using Xamarin.Forms;

namespace EDMKScannerProj
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //var sccan = DependencyService.Get<IScannerService>();
            BindingContext = new MainViewModel(App.Scanner);
        }
    }
}
