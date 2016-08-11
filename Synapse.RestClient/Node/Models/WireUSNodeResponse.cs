using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Node
{
    public class WireUSNodeResponse : NodeResponse
    {
        public WireUSNodeResponseInfo Info { get; set; }
    }

    public class WireUSNodeResponseInfo
    {
        public string AccountNum { get; set; }

        public string Address { get; set; }

        public string BankName { get; set; }

        public string BankLongName { get; set; }

        public WireUSNodeResponseCorrespondentInfo CorrespondentInfo { get; set; }

        public string NameOnAccount { get; set; }

        public string Nickname { get; set; }

        public string RoutingNum { get; set; }
    }

    public class WireUSNodeResponseCorrespondentInfo
    {
        public string Address { get; set; }

        public string BankName { get; set; }

        public string RoutingNum { get; set; }
    }
}
