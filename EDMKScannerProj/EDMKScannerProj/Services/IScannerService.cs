using System;
using EDMKScannerProj.Services.Models;

namespace EDMKScannerProj.Services
{
    public interface IScannerService
    {
        event EventHandler<OnBarcodeScannedEventArgs> OnBarcodeScanned;
    }
}
