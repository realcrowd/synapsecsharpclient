using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient
{
    public class SynapseApiUserCredentials
    {
        public string OAuthKey { get; set; }
        public string Fingerprint { get; set; }
        public string IpAddressOfUser { get; set; }
    }
}
