using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Transaction
{
    public class CommentOnTransactionRequest
    {
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}
