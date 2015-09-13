using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace Synapse.RestClient.User
{
    using User;
    using Node;
    [TestClass]
    public class UserApiTests : BaseScenarioTest
    {
        ISynapseUserApiClient _user;

        [TestInitialize]
        public override void Init()
        {
            base.Init();
            this._user = new SynapseUserApiClient(this.Credentials, this.BaseUrl);
        }


        [TestMethod]
        public async Task CreatesUser()
        {
            var result = await this._user.CreateUserAsync(this.CreateUserRequest());

            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
            result.OAuth.ShouldNotBeNull();
            result.OAuth.Key.ShouldNotBeEmpty();
            result.OAuth.RefreshToken.ShouldNotBeEmpty();
            result.Permission.ShouldEqual(SynapsePermission.Unverified);
        }

        [TestMethod]
        public async Task AddsKycSuccessfully()
        {
            var user = await this._user.CreateUserAsync(this.CreateUserRequest());
            var result = await this._user.AddKycAsync(this.CreateKycRequest(user.OAuth));

            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
            //result.Permission.ShouldEqual(SynapsePermission.ReceiveOnly); //TODO: Discuss
            result.NeedsValidation.ShouldBeFalse();
        }

        [TestMethod]
        public async Task AddsDocumentation()
        {
            var user = await this._user.CreateUserAsync(this.CreateUserRequest());
            var kyc = await this._user.AddKycAsync(this.CreateKycRequest(user.OAuth));
            var result = await this._user.AddDocAsync(this.CreateAddDocRequest(user.OAuth));
            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
            //result.Permission.ShouldEqual(SynapsePermission.SendAndReceive);  //TODO: Discuss
        }

    }
}
