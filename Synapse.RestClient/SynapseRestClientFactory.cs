using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synapse.RestClient.Transaction;

namespace Synapse.RestClient
{
    using User;
    using Node;
    public class SynapseRestClientFactory
    {
        private SynapseApiClientCredentials _creds;
        private string _baseUrl;
        private Action<SynapseApiErrorException> _apiErrorLogger;

        public SynapseRestClientFactory(SynapseApiClientCredentials credentials, string baseUrl, Action<SynapseApiErrorException> apiErrorLogger = null)
        {
            this._creds = credentials;
            this._baseUrl = baseUrl;
            this._apiErrorLogger = apiErrorLogger;
        }

        public ISynapseUserApiClient CreateUserClient()
        {
            return new SynapseUserApiClient(this._creds, this._baseUrl, this._apiErrorLogger);
        }

        public ISynapseNodeApiClient CreateNodeClient()
        {
            return new SynapseNodeApiClient(this._creds, this._baseUrl, this._apiErrorLogger);
        }

        public ISynapseTransactionApiClient CreateTransactionClient()
        {
            return new SynapseTransactionApiClient(this._creds, this._baseUrl, this._apiErrorLogger);
        }
    }
}
