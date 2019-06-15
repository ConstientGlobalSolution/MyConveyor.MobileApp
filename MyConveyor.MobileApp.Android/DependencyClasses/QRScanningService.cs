using MyConveyor.MobileApp.DependencyInterfaces;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(MyConveyor.MobileApp.Android.DependencyClasses.QRScanningService))]
namespace MyConveyor.MobileApp.Android.DependencyClasses
{
    public class QRScanningService : IQRScanner
    {
        public async Task<string> ScanQRAndBarCode()
        {
            MobileBarcodeScanningOptions optionsCustom = new MobileBarcodeScanningOptions();
            MobileBarcodeScanner scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan The QR Code",
                BottomText = "Please Wait..!",
            };
            ZXing.Result scanResult = await scanner.Scan(optionsCustom);
            if (scanResult != null)
            {
                return scanResult.Text;
            }

            return null;
        }

    }
}