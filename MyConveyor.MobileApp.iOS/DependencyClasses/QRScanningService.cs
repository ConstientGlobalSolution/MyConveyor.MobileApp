using MyConveyor.MobileApp.DependencyInterfaces;
using System;
using System.Threading.Tasks;
using ZXing.Mobile;

[assembly: Xamarin.Forms.Dependency(typeof(MyConveyor.MobileApp.iOS.DependencyClasses.QRScanningService))]

namespace MyConveyor.MobileApp.iOS.DependencyClasses
{
    public class QRScanningService : IQRScanner
    {
        public async Task<string> ScanQRAndBarCode()
        {
            try
            {
                MobileBarcodeScanningOptions optionsCustom = new MobileBarcodeScanningOptions();
                MobileBarcodeScanner scanner = new MobileBarcodeScanner()
                {
                    TopText = "Scan The QR Code",
                    BottomText = "Please Wait..!",
                };
                ZXing.Result scanResult = await scanner?.Scan(optionsCustom);
                if (scanResult != null)
                    return scanResult.Text;

                return null;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}
