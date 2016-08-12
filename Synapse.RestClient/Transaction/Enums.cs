using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Transaction
{
    public enum SynapseTransactionStatusCode
    {
        QueuedBySynapse = -1,
        QueuedByReceiver = 0,
        Created = 1,
        ProcessingDebit = 2,
        ProcesingCredit = 3,
        Settled = 4,
        Cancelled = 5,
        Returned = 6
    }

    internal class SynapseTransactionStatusCodeEnumConverter : EnumConverter<SynapseTransactionStatusCode>
    {
        protected override Dictionary<SynapseTransactionStatusCode, string> Lookup { get { return null; } }

        public override SynapseTransactionStatusCode ToEnum(string str)
        {
            return (SynapseTransactionStatusCode)Convert.ToInt32(str);
        }

        public override string ToString(SynapseTransactionStatusCode val)
        {
            return ((int)val).ToString();
        }
    }
}
