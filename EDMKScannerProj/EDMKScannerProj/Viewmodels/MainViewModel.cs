using System;
using EDMKScannerProj.Services;

namespace EDMKScannerProj.Viewmodels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IScannerService scannerService) : base(scannerService)
        {
            ScannedBarcodeText = "Scanner text from MainViewModel";
        }
    }
}
