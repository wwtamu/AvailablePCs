using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;

namespace AvailablePCs
{
    public class Network
    {

        public Network() 
        {
        }

        /// <summary>
        /// Determins if there is an internet connection.
        /// </summary>
        /// <returns></returns>
        public bool CheckForInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }
    }
}
