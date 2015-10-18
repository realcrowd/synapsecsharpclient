using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Transaction
{
    public interface ISynapseTransactionApiClient
    {
        AddTransactionResponse AddTransactionAsync(AddTransactionRequest msg);
        event RequestEventHandler OnAfterRequest;
    }

    public class SynapseTransactionApiClient : ISynapseTransactionApiClient
    {
        public event RequestEventHandler OnAfterRequest = delegate { };
        public AddTransactionResponse AddTransactionAsync(AddTransactionRequest msg)
        {
            throw new NotImplementedException();
        }
        private void RaiseOnAfterRequest(object body, IRestRequest req, IRestResponse resp)
        {
            OnAfterRequest(resp.ResponseUri.ToString(), resp.StatusCode, SimpleJson.SerializeObject(body), resp.Content);
        }
    }
}
