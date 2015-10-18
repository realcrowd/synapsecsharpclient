using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Transaction
{
    using User;
    public class AddTransactionRequest
    {
        public SynapseUserOAuth OAuth { get; set; }
        public string IpAddress { get; set; }
        public string LocalId { get; set; }
        public string Fingerprint { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string FromType { get; set; }
        public string FromId { get; set; }
        public string ToType { get; set; }
        public string ToId { get; set; }

        public string Memo { get; set; }
        public string WebhookUrl { get; set; }
    }

    public class AddTransactionResponse
    {

    }

}
