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

        protected const string Fingerprint = "suasusau21324redakufejfjsf";
        [TestInitialize]
        public virtual void Init()
        {
            this.BaseUrl = ConfigurationManager.AppSettings["SynapseBaseUrl"];
            this.Credentials = new SynapseApiCredentials
            {
                ClientId = ConfigurationManager.AppSettings["SynapseClientId"],
                ClientSecret = ConfigurationManager.AppSettings["SynapseClientSecret"]
            };
        }
        

        protected string GetTextResource(string name)
        {
            using (var res = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Synapse.RestClient.Resources." + name))
            {
                using (var s = new System.IO.StreamReader(res))
                {
                    return s.ReadToEnd();
                }
            }
        }
    }
}
