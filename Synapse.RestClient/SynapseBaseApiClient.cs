using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace Synapse.RestClient
{
    public delegate void RequestEventHandler(string url, HttpStatusCode httpStatus, string requestBody, string responseBody);

    public abstract class SynapseBaseApiClient
    {
        SynapseApiClientCredentials _creds;
        IRestClient _api;

        JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new SnakeCasePropertyNamesContractResolver()
        };

        public SynapseBaseApiClient(SynapseApiClientCredentials creds, string baseUrl) 
            : this(creds, new RestSharp.RestClient(new Uri(baseUrl, UriKind.Absolute)))
        {
        }

        public SynapseBaseApiClient(SynapseApiClientCredentials creds, IRestClient client)
        {
            this._creds = creds;
            this._api = client;
        }

        public event RequestEventHandler OnAfterRequest = delegate { };

        protected async Task<IRestResponse> ExecuteRequestAsync(SynapseApiUserCredentials apiUser, object body, IRestRequest req)
        {
            if (body != null)
            {
                var serializedBody = JsonConvert.SerializeObject(body, this._jsonSettings);
                req.AddParameter("application/json", serializedBody, ParameterType.RequestBody);
            }

            req.AddHeader("X-SP-GATEWAY", String.Format("{0}|{1}", this._creds.ClientId, this._creds.ClientSecret));
            req.AddHeader("X-SP-USER", String.Format("{0}|{1}", apiUser.OAuthKey, apiUser.Fingerprint));
            req.AddHeader("X-SP-USER-IP", apiUser.IpAddressOfUser);

            var resp = await this._api.ExecuteTaskAsync(req);
            RaiseOnAfterRequest(body, req, resp);
            return resp;
        }

        protected T ParseResponse<T>(IRestResponse resp)
        {
            if (resp.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = JsonConvert.DeserializeObject<SynapseApiErrorResponse>(resp.Content, this._jsonSettings);
                throw new SynapseApiErrorException(error);
            }
            var parsed = JsonConvert.DeserializeObject<T>(resp.Content, this._jsonSettings);
            return parsed;
        }

        private void RaiseOnAfterRequest(object body, IRestRequest req, IRestResponse resp)
        {
            OnAfterRequest(resp.ResponseUri.ToString(), resp.StatusCode, SimpleJson.SerializeObject(body), resp.Content);
        }
    }
}
