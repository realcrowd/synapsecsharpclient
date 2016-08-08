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
    public class NodeApiTests : BaseScenarioTest
    {
        ISynapseNodeApiClient _node;
        ISynapseUserApiClient _user;

        [TestInitialize]
        public override void Init()
        {
            base.Init();
            this._node = this.Factory.CreateNodeClient();
            this._user = this.Factory.CreateUserClient();
        }

        [TestMethod]
        public async Task AddsACHNode()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());
            var refreshToken = await this._user.RefreshTokenAsync(apiUser, user.Id, new RefreshTokenRequest() { RefreshToken = user.RefreshToken });
            apiUser.OAuthKey = refreshToken.OAuthKey;
            user = await this._user.AddDocumentsAsync(apiUser, user.Id, this.CreateAddDocumentsRequest());

            var result = await this._node.AddACHNodeAsync(apiUser, user.Id, this.CreateAddACHNodeRequest());

            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
            result.Nodes.ShouldNotBeNull();
            result.Nodes.Length.ShouldEqual(1);
            result.Nodes[0].Id.ShouldNotBeEmpty();
            result.Nodes[0].Type = "ACH-US";
            result.Nodes[0].Info.Type.ShouldEqual(SynapseNodeType.Personal);
            result.Nodes[0].Allowed.ShouldEqual(SynapseNodePermission.Credit);
        }

        [TestMethod]
        public async Task AddACHNodeWithInvalidRoutingNumber()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());
            var refreshToken = await this._user.RefreshTokenAsync(apiUser, user.Id, new RefreshTokenRequest() { RefreshToken = user.RefreshToken });
            apiUser.OAuthKey = refreshToken.OAuthKey;

            try
            {
                var req = this.CreateAddACHNodeRequest();
                req.Info.RoutingNum = "123456";
                var result = await this._node.AddACHNodeAsync(apiUser, user.Id, req);

                // exception should be thrown and code never reached;
                (true).ShouldBeFalse();
            } catch (Exception ex)
            {
                ex.ShouldBeType(typeof(SynapseApiErrorException));
                var synapseEx = ex as SynapseApiErrorException;
                synapseEx.ApiErrorResponse.ErrorCode.ShouldEqual("200");
                synapseEx.ApiErrorResponse.HttpCode.ShouldEqual("400");
            }

        }

        [TestMethod]
        public async Task GetACHNode()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());
            var refreshToken = await this._user.RefreshTokenAsync(apiUser, user.Id, new RefreshTokenRequest() { RefreshToken = user.RefreshToken });
            apiUser.OAuthKey = refreshToken.OAuthKey;
            var result = await this._node.AddACHNodeAsync(apiUser, user.Id, this.CreateAddACHNodeRequest());

            var node = await this._node.GetNodeAsync(apiUser, user.Id, result.Nodes[0].Id);

            node.ShouldNotBeNull();
            node.Id.ShouldEqual(result.Nodes[0].Id);
            node.Type = "ACH-US";
            node.Info.Type.ShouldEqual(SynapseNodeType.Personal);
            node.Allowed.ShouldEqual(SynapseNodePermission.Credit);
        }

        [TestMethod]
        public async Task VerifiesACHNode()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());
            var refreshToken = await this._user.RefreshTokenAsync(apiUser, user.Id, new RefreshTokenRequest() { RefreshToken = user.RefreshToken });
            apiUser.OAuthKey = refreshToken.OAuthKey;
            user = await this._user.AddDocumentsAsync(apiUser, user.Id, this.CreateAddDocumentsRequest());
            var result = await this._node.AddACHNodeAsync(apiUser, user.Id, this.CreateAddACHNodeRequest());

            var node = await this._node.VerifyNodeAsync(apiUser, user.Id, result.Nodes[0].Id, new VerifyNodeRequest()
            {
                MicroDeposits = new decimal[] { 0.10m, 0.10m }
            });

            node.ShouldNotBeNull();
            node.IsActive.ShouldBeTrue();
            node.Allowed.ShouldEqual(SynapseNodePermission.CreditAndDebit);
        }

        [TestMethod]
        public async Task SearchExistingNode()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());
            var refreshToken = await this._user.RefreshTokenAsync(apiUser, user.Id, new RefreshTokenRequest() { RefreshToken = user.RefreshToken });
            apiUser.OAuthKey = refreshToken.OAuthKey;
            user = await this._user.AddDocumentsAsync(apiUser, user.Id, this.CreateAddDocumentsRequest());
            var result = await this._node.AddACHNodeAsync(apiUser, user.Id, this.CreateAddACHNodeRequest());

            var nodes = await this._node.SearchNodes(apiUser, user.Id);

            nodes.Success.ShouldBeTrue();
            nodes.Page.ShouldEqual(1);
            nodes.Nodes.ShouldNotBeNull();
            nodes.Nodes.Length.ShouldEqual(1);

            var found = nodes.Nodes.First();
            found.Id.ShouldEqual(result.Nodes[0].Id);
            found.Allowed.ShouldEqual(result.Nodes[0].Allowed);
        }

        [TestMethod]
        public async Task DeletesNode()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());
            var refreshToken = await this._user.RefreshTokenAsync(apiUser, user.Id, new RefreshTokenRequest() { RefreshToken = user.RefreshToken });
            apiUser.OAuthKey = refreshToken.OAuthKey;
            user = await this._user.AddDocumentsAsync(apiUser, user.Id, this.CreateAddDocumentsRequest());
            var result = await this._node.AddACHNodeAsync(apiUser, user.Id, this.CreateAddACHNodeRequest());

            var status  = await this._node.DeleteNodeAsync(apiUser, user.Id, result.Nodes[0].Id);
            status.Success.ShouldBeTrue();

            var nodes = await this._node.SearchNodes(apiUser, user.Id);

            nodes.Success.ShouldBeTrue();
            nodes.Page.ShouldEqual(1);
            nodes.Nodes.ShouldNotBeNull();
            nodes.Nodes.Length.ShouldEqual(0);
        }
    }
}
