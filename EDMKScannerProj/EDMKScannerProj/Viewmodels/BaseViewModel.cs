using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using EDMKScannerProj.Services;

namespace EDMKScannerProj.Viewmodels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private string _scannedBarcodeText;

        public string ScannedBarcodeText
        {
            get { return _scannedBarcodeText; }
            set
            {
                _scannedBarcodeText = value;
                NotifyPropertyChanged(nameof(ScannedBarcodeText));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        IScannerService _scannerService;

        public BaseViewModel(IScannerService scannerService)
        {
            _scannerService = scannerService;
            _scannerService.OnBarcodeScanned += _scannerService_OnBarcodeScanned;
        }

        //~BaseViewModel()
        //{
        //    _scannerService.OnBarcodeScanned -= _scannerService_OnBarcodeScanned;
        //}

        void _scannerService_OnBarcodeScanned(object sender, Services.Models.OnBarcodeScannedEventArgs e)
        {
            var scannedBarcode = e?.BarCodes?.FirstOrDefault();
            if (scannedBarcode == null)
                return;

            var searchCode = scannedBarcode.Barcode;
            var symbology = scannedBarcode.Symbology;


            ScannedBarcodeText = searchCode;
        }


        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
