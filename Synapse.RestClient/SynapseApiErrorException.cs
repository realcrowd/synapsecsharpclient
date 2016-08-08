using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient
{
    public class SynapseApiErrorException : Exception
    {
        public SynapseApiErrorResponse ApiErrorResponse { get; set; }

        public SynapseApiErrorException(SynapseApiErrorResponse response)
        {
            this.ApiErrorResponse = response;
        }

        public SynapseApiErrorException(SynapseApiErrorResponse response, string message)
            : base(message)
        {
            this.ApiErrorResponse = response;
        }

        public SynapseApiErrorException(SynapseApiErrorResponse response, string message, Exception inner)
            : base(message, inner)
        {
            this.ApiErrorResponse = response;
        }
    }
}
