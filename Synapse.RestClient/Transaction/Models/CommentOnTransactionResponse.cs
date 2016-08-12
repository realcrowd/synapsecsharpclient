using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Transaction
{
    public class CommentOnTransactionResponse
    {
        public string ErrorCode { get; set; }

        public string HttpCode { get; set; }

        [JsonProperty("trans")]
        public TransactionResponse Transaction { get; set; }

        public bool Success { get; set; }
    }
}
