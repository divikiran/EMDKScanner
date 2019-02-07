using System;
namespace EDMKScannerProj.Viewmodels
{
    public class MainViewModel : BaseViewModel
    {
        private string _scannerStatus;

        public string ScannerStatus
        {
            get { return _scannerStatus; }
            set
            {
                _scannerStatus = value;
                NotifyPropertyChanged(nameof(ScannerStatus));
            }
        }

        public MainViewModel()
        {
            ScannerStatus = "Scanner text from MainViewModel";
        }
    }
}
