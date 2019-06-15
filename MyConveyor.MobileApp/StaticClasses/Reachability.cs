using Plugin.Connectivity;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using static MyConveyor.MobileApp.StaticClasses.ModEnumerations;

namespace MyConveyor.MobileApp.StaticClasses
{
    public static class Reachability
    {
        /// ------------------------------------------------------------------------------------------------
        /// Name		HostName
        /// 
        /// <summary>	Gets and sets the HostName.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------
        public static string HostName
        {
            get;
            set;
        }

        /// ------------------------------------------------------------------------------------------------
        /// Name		CurrentConnection
        /// 
        /// <summary>	Gets and sets the CurrentConnection.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------
        public static Plugin.Connectivity.Abstractions.ConnectionType CurrentConnection { get; set; }

        /// ------------------------------------------------------------------------------------------------
        /// 
        /// <summary>  It is used to check the network status and update it.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------
        static Reachability()
        {
            try
            {
                HostName = Constants.HostName;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// ------------------------------------------------------------------------------------------------
        /// Name		InternetConnectionStatus
        /// 
        /// <summary>	Gets the Network status for the internet connection.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------
        public static ReachabilityNetworkStatus InternetConnectionStatus()
        {
            foreach (Plugin.Connectivity.Abstractions.ConnectionType item in CrossConnectivity.Current.ConnectionTypes)
            {
                CurrentConnection = item;
                if (!CrossConnectivity.Current.IsConnected)
                {
                    return ReachabilityNetworkStatus.NotReachable;
                }
                else if ((CurrentConnection == Plugin.Connectivity.Abstractions.ConnectionType.Desktop) ||
                         (CurrentConnection == Plugin.Connectivity.Abstractions.ConnectionType.Cellular))
                {
                    return ReachabilityNetworkStatus.ReachableViaCarrierDataNetwork;
                }
                else if (CurrentConnection == Plugin.Connectivity.Abstractions.ConnectionType.WiFi)
                {
                    return ReachabilityNetworkStatus.ReachableViaWiFiNetwork;
                }
                else
                {
                    return ReachabilityNetworkStatus.NotReachable;
                }
            }
            return ReachabilityNetworkStatus.NotReachable;
        }

        /// ------------------------------------------------------------------------------------------------
        /// Name		HostReachableStatus
        /// 
        /// <summary>
        ///             Gets the Host status 
        /// </summary>
        /// ------------------------------------------------------------------------------------------------
        public static async Task<ReachabilityNetworkStatus> HostReachableStatus()
        {
            if (await IsHostReachable(HostName))
            {
                return ReachabilityNetworkStatus.HostReachable;
            }
            else
            {
                return ReachabilityNetworkStatus.NotReachable;
            }
        }

        /// ------------------------------------------------------------------------------------------------
        /// Name		OnNetworkChanged
        /// 
        /// <summary>	Executes when the network type is changed.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------
        public static void OnNetworkChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            try
            {
                Debug.WriteLine("");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// ------------------------------------------------------------------------------------------------
        /// Name		IsHostReachable
        /// 
        /// <summary>	Is the host reachable with the current network configuration.
        /// </summary>
        /// <param name="host">			The host.</param>
        /// 
        /// <returns>	If it's reachable.
        /// </returns>
        /// ------------------------------------------------------------------------------------------------
        public static async Task<bool> IsHostReachable(string host)
        {
            try
            {
                Uri uri = new Uri(host);
                if (string.IsNullOrEmpty(uri.Host))
                {
                    return false;
                }

                return await CrossConnectivity.Current.IsRemoteReachable(uri.Host);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
