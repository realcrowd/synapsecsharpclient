using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Synapse.RestClient
{
    using User;
    [TestClass]
    public abstract class BaseTest
    {
        protected string BaseUrl;
        protected SynapseApiCredentials Credentials;
        protected SynapseRestClientFactory Factory { get; set; }

        protected const string Fingerprint = "suasusau21324redakufejfjsf";
        public const string IpAddress = "10.1.0.1";
        [TestInitialize]
        public virtual void Init()
        {
            this.BaseUrl = ConfigurationManager.AppSettings["SynapseBaseUrl"];
            this.Credentials = new SynapseApiCredentials
            {
                ClientId = ConfigurationManager.AppSettings["SynapseClientId"],
                ClientSecret = ConfigurationManager.AppSettings["SynapseClientSecret"]
            };
            this.Factory = new SynapseRestClientFactory(Credentials, BaseUrl);
        }
        

        protected string GetTextResource(string name)
        {
            using (var res = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Synapse.RestClient.Support.Resources." + name))
            {
                using (var s = new System.IO.StreamReader(res))
                {
                    return s.ReadToEnd();
                }
            }
        }
    }
}
