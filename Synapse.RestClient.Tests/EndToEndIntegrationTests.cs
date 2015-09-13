using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace Synapse.RestClient
{
    using User;
    using Node;
    [TestClass]
    public class EndToEndIntegrationTests : BaseScenarioTest
    {
        ISynapseUserApiClient _user;
        ISynapseNodeApiClient _node;

        [TestInitialize]
        public override void Init()
        {
            base.Init();
            this._user = Factory.CreateUserClient();
            this._node = Factory.CreateNodeClient();
        }

        
        [TestMethod]
        public async Task IsKosher()
        {
            var user = await this._user.CreateUserAsync(this.CreateUserRequest());
            await this._user.AddKycAsync(this.CreateKycRequest(user.OAuth));
            await this._user.AddDocAsync(this.CreateAddDocRequest(user.OAuth));
            var result = await this._node.AddACHNodeAsync(this.CreateAddACHNodeRequest(user.OAuth));
            result.Success.ShouldBeTrue();
            result.SynapseNodeOId.ShouldNotBeEmpty();

        }
    }
}
