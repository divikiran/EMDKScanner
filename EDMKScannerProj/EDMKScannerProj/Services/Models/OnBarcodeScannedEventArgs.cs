using System;
using System.Collections;
using System.Collections.Generic;

namespace EDMKScannerProj.Services.Models
{
    public class OnBarcodeScannedEventArgs : EventArgs
    {
        public IEnumerable<ScannedBarCode> BarCodes { get; set; }
        public OnBarcodeScannedEventArgs(IEnumerable<ScannedBarCode> barcodes)
        {
            BarCodes = barcodes;
        }
    }
}
