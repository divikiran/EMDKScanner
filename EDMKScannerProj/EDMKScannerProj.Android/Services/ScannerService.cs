using System;
using System.Collections.Generic;
using Android.OS;
using Android.Util;
using EDMKScannerProj.Droid.Services;
using EDMKScannerProj.Services;
using EDMKScannerProj.Services.Models;
using Symbol.XamarinEMDK;
using Symbol.XamarinEMDK.Barcode;
using Xamarin.Forms;

[assembly: Dependency(typeof(ScannerService))]
namespace EDMKScannerProj.Droid.Services
{
    public class ScannerService : Java.Lang.Object, EMDKManager.IEMDKListener, IScannerService
    {
        private EMDKManager _emdkManager;
        private BarcodeManager _barcodeManager;
        private Scanner _scanner;

        public event EventHandler<OnBarcodeScannedEventArgs> OnBarcodeScanned;

        public void OnClosed()
        {
            if (_emdkManager != null)
            {
                _emdkManager.Release();
                _emdkManager = null;
            }
        }

        public void OnOpened(EMDKManager emdkManager)
        {
            _emdkManager = emdkManager;
            InitScanner();
        }

        public void InitScanner()
        {
            try
            {
                if (_emdkManager != null)
                {

                    if (_barcodeManager == null)
                    {
                        //Get the feature object such as BarcodeManager object for accessing the feature.
                        _barcodeManager = (BarcodeManager)_emdkManager.GetInstance(EMDKManager.FEATURE_TYPE.Barcode);

                        _scanner = _barcodeManager.GetDevice(BarcodeManager.DeviceIdentifier.Default);

                        if (_scanner != null)
                        {

                            //Attahch the Data Event handler to get the data callbacks.
                            _scanner.Data += scanner_Data;

                            //Attach Scanner Status Event to get the status callbacks.
                            _scanner.Status += scanner_Status;

                            _scanner.Enable();

                            //EMDK: Configure the scanner settings
                            ScannerConfig config = _scanner.GetConfig();
                            config.SkipOnUnsupported = ScannerConfig.SkipOnUnSupported.None;
                            config.ScanParams.DecodeLEDFeedback = true;
                            //config.ReaderParams.ReaderSpecific.ImagerSpecific.PickList = ScannerConfig.PickList.Enabled;
                            config.DecoderParams.Code39.Enabled = true;
                            config.DecoderParams.Code128.Enabled = false;
                            _scanner.SetConfig(config);

                        }
                        else
                        {
                            displayStatus("Failed to enable scanner.\n");
                        }
                    }
                }
            }
            catch (ScannerException e)
            {
                displayStatus("Error: " + e.Message);
            }
            catch (Exception ex)
            {
                displayStatus("Error: " + ex.Message);
            }
        }

        private void scanner_Status(object sender, Scanner.StatusEventArgs e)
        {
            String statusStr = "";

            //EMDK: The status will be returned on multiple cases. Check the state and take the action.
            StatusData.ScannerStates state = e.P0.State;

            if (state == StatusData.ScannerStates.Idle)
            {
                statusStr = "Scanner is idle and ready to submit read.";
                try
                {
                    if (_scanner.IsEnabled && !_scanner.IsReadPending)
                    {
                        _scanner.Read();
                    }
                }
                catch (ScannerException e1)
                {
                    statusStr = e1.Message;
                }
            }
            if (state == StatusData.ScannerStates.Waiting)
            {
                statusStr = "Waiting for Trigger Press to scan";
            }
            if (state == StatusData.ScannerStates.Scanning)
            {
                statusStr = "Scanning in progress...";
            }
            if (state == StatusData.ScannerStates.Disabled)
            {
                statusStr = "Scanner disabled";
            }
            if (state == StatusData.ScannerStates.Error)
            {
                statusStr = "Error occurred during scanning";

            }
            displayStatus(statusStr);
        }

        private void scanner_Data(object sender, Scanner.DataEventArgs e)
        {
            ScanDataCollection scanDataCollection = e.P0;

            if ((scanDataCollection != null) && (scanDataCollection.Result == ScannerResults.Success))
            {
                IList<ScanDataCollection.ScanData> scanData = scanDataCollection.GetScanData();

                List<ScannedBarCode> scannedBarCodes = new List<ScannedBarCode>();

                foreach (ScanDataCollection.ScanData data in scanData)
                {
                    //displaydata(data.LabelType + " : " + data.Data);
                    string barcode = data.Data;
                    string symbology = data.LabelType.Name();

                    scannedBarCodes.Add(new ScannedBarCode(barcode, symbology));
                }

                this.OnBarcodeScanned?.Invoke(this, new OnBarcodeScannedEventArgs(scannedBarCodes));
            }
        }

        internal void DeinitScanner()
        {
            if (_emdkManager != null)
            {

                if (_scanner != null)
                {
                    try
                    {

                        _scanner.Data -= scanner_Data;
                        _scanner.Status -= scanner_Status;
                        _scanner.Disable();
                    }
                    catch (ScannerException e)
                    {
                        //Log.Debug(this.Class.SimpleName, "Exception:" + e.Result.Description);
                    }
                }

                if (_barcodeManager != null)
                {
                    _emdkManager.Release(EMDKManager.FEATURE_TYPE.Barcode);
                }
                _barcodeManager = null;
                _scanner = null;
            }
        }

        void displayStatus(String status)
        {

            if (Looper.MainLooper.Thread == Java.Lang.Thread.CurrentThread())
            {
                //statusView.Text = status;
            }
            else
            {
                //RunOnUiThread(() => statusView.Text = status);
            }
        }

        void displaydata(string data)
        {

            if (Looper.MainLooper.Thread == Java.Lang.Thread.CurrentThread())
            {
                //dataView.Text += (data + "\n");
            }
            else
            {
                // RunOnUiThread(() => dataView.Text += data + "\n");
            }


        }

        public void Destroy()
        {
            //Clean up the emdkManager
            if (_emdkManager != null)
            {
                //EMDK: Release the EMDK manager object
                _emdkManager.Release();
                _emdkManager = null;
            }
        }
    }
}


