using System;
using System.Collections;
using System.Collections.Generic;

namespace EDMKScannerProj.Services.Models
{
    public class OnBarcodeScannedEventArgs : EventArgs
    {
        public OnBarcodeScannedEventArgs(IEnumerable<ScannedBarCode> barcodes)
        {
        }
    }
}
