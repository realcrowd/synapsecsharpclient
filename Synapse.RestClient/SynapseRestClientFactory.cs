using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient
{
    using User;
    public class SynapseRestClientFactory
    {
        private SynapseApiCredentials _creds;
        private string _baseUrl;
        public SynapseRestClientFactory(SynapseApiCredentials credentials, string baseUrl)
        {
            this._creds = credentials;
            this._baseUrl = baseUrl;
        }

        public ISynapseUserApiClient CreateUserClient()
        {
            return new SynapseUserApiClient(this._creds, this._baseUrl);
        }
    }
}
