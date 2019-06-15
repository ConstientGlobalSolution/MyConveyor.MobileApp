using MyConveyor.MobileApp.Classes;
using System;

namespace MyConveyor.MobileApp.StaticClasses
{
    public static class Constants
    {
        public static Api ClientApi { get; set; }
        public const string HostName = " http://122.165.146.75:8080/api/";

        static Constants()
        {
            try
            {
                ClientApi = new Api();
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        public static string GetFilterDataURI(string serialNumber, string itemcode, string itemDescription, bool IsSparePart, int index, int count)
        {
            try
            {
                return string.Format("Products/filter?SerialNumber={0}&ItemCode={1}&ItemDescription={2}&IsSparepart={3}&PageNumber={4}&PageSize={5}", serialNumber, itemcode, itemDescription, IsSparePart, index, count);
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                return null;
            }
        }

        public static string PostOrderListURI()
        {
            try
            {
                return "Order";
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                return null;
            }
        }
    }
}
