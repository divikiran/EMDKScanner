using System;
namespace EDMKScannerProj.Services.Models
{
    public class ScannedBarCode
    {
        public string Barcode { get; }
        public string Symbology { get; }

        public ScannedBarCode(string barcode, string symbology)
        {
            this.Barcode = barcode;
            this.Symbology = symbology;
        }
    }
}
