using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Net.Http;
using System.Net;

namespace Synapse.RestClient
{
    public delegate void RequestEventHandler(string url, HttpStatusCode httpStatus, string requestBody, string responseBody);
    public abstract class BaseSynapseApiClient
    {
        public event RequestEventHandler OnAfterRequest;
        protected async Task ExecutePostAsync(IRestClient client, IRestRequest request, dynamic body, Action<dynamic> httpOk, Action<dynamic> httpErr)
        {
            request.AddJsonBody(body);
            var result = await client.ExecuteTaskAsync(request);
            var json = SimpleJson.SerializeObject(body);
            dynamic data = SimpleJson.DeserializeObject(result.Content);
            if (OnAfterRequest != null)
            {
                this.OnAfterRequest(result.ResponseUri.ToString(), result.StatusCode, json, result.Content);
            }
            if(result.StatusCode == HttpStatusCode.OK)
            {
                httpOk(data);
            }
            else
            {
                httpErr(data);
            }

        }
    }
}
