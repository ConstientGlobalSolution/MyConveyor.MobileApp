using System.Threading.Tasks;

namespace MyConveyor.MobileApp.DependencyInterfaces
{
    public interface IQRScanner
    {
        Task<string> ScanQRAndBarCode();
    }
}
