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

            ACHNodeResponse node = (ACHNodeResponse)result.Nodes[0];
            node.Id.ShouldNotBeEmpty();
            node.Type.ShouldEqual(SynapseNodeType.ACHUS);
            node.Info.Type.ShouldEqual(SynapseACHNodeType.Personal);
            node.Allowed.ShouldEqual(SynapseNodePermission.Credit);
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
        public async Task AddsACHNodeWithLogin()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());
            var refreshToken = await this._user.RefreshTokenAsync(apiUser, user.Id, new RefreshTokenRequest() { RefreshToken = user.RefreshToken });
            apiUser.OAuthKey = refreshToken.OAuthKey;
            user = await this._user.AddDocumentsAsync(apiUser, user.Id, this.CreateAddDocumentsRequest());

            var result = await this._node.AddACHNodeWithLoginAsync(apiUser, user.Id, new AddACHNodeWithLoginRequest()
            {
                Info = new AddACHNodeWithLoginRequestInfo()
                {
                    BankId = "synapse_nomfa",
                    BankPassword = "test1234",
                    BankName = "bofa",
                }
            });

            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
            result.Nodes.ShouldNotBeNull();
            result.Nodes.Length.ShouldEqual(2); // both checking and savings account are added

            ACHNodeResponse node1 = (ACHNodeResponse)result.Nodes[0];
            ACHNodeResponse node2 = (ACHNodeResponse)result.Nodes[1];
            node1.Id.ShouldNotBeEmpty();
            node2.Id.ShouldNotBeEmpty();
            node1.Id.ShouldNotEqual(node2.Id);
            node1.Type.ShouldEqual(SynapseNodeType.ACHUS);
            node2.Type.ShouldEqual(SynapseNodeType.ACHUS);
            if (node1.Info.Class == SynapseACHNodeClass.Checking)
                node2.Info.Class.ShouldEqual(SynapseACHNodeClass.Savings);
            else
            {
                node1.Info.Class.ShouldEqual(SynapseACHNodeClass.Savings);
                node2.Info.Class.ShouldEqual(SynapseACHNodeClass.Checking);
            }
            node1.Info.Type.ShouldEqual(SynapseACHNodeType.Personal);
            node2.Info.Type.ShouldEqual(SynapseACHNodeType.Personal);
            node1.Allowed.ShouldEqual(SynapseNodePermission.CreditAndDebit);
            node2.Allowed.ShouldEqual(SynapseNodePermission.CreditAndDebit);
        }

        [TestMethod]
        public async Task AddsWireUSNode()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());
            var refreshToken = await this._user.RefreshTokenAsync(apiUser, user.Id, new RefreshTokenRequest() { RefreshToken = user.RefreshToken });
            apiUser.OAuthKey = refreshToken.OAuthKey;
            user = await this._user.AddDocumentsAsync(apiUser, user.Id, this.CreateAddDocumentsRequest());

            var result = await this._node.AddWireUSNodeAsync(apiUser, user.Id, new AddWireUSNodeRequest()
            {
                Info = new AddWireUSNodeRequestInfo()
                {
                    Nickname = "Some Account",
                    NameOnAccount = "Some Name",
                    AccountNum = "1235674434",
                    RoutingNum = "026009593",
                    BankName = "Bank of America",
                    Address = "452 Fifth Ave, NY",
                    CorrespondentInfo = new AddWireUSNodeRequestCorrespondingInfo()
                    {
                        RoutingNum = "026009593",
                        BankName = "Bank of America",
                        Address = "452 Fifth Ave, NY"
                    }
                },
                Extra = new AddWireUSNodeRequestExtra()
                {
                    SuppId = "1234"
                }
            });

            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
            result.Nodes.ShouldNotBeNull();
            result.Nodes.Length.ShouldEqual(1);

            WireUSNodeResponse node = (WireUSNodeResponse)result.Nodes[0];
            node.Id.ShouldNotBeEmpty();
            node.Type.ShouldEqual(SynapseNodeType.WireUS);
            node.Allowed.ShouldEqual(SynapseNodePermission.CreditAndDebit);
        }

        [TestMethod]
        public async Task GetACHNode()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());
            var refreshToken = await this._user.RefreshTokenAsync(apiUser, user.Id, new RefreshTokenRequest() { RefreshToken = user.RefreshToken });
            apiUser.OAuthKey = refreshToken.OAuthKey;
            var result = await this._node.AddACHNodeAsync(apiUser, user.Id, this.CreateAddACHNodeRequest());

            ACHNodeResponse node = (ACHNodeResponse)await this._node.GetNodeAsync(apiUser, user.Id, result.Nodes[0].Id);

            node.ShouldNotBeNull();
            node.Id.ShouldEqual(result.Nodes[0].Id);
            node.Type.ShouldEqual(SynapseNodeType.ACHUS);
            node.Info.Type.ShouldEqual(SynapseACHNodeType.Personal);
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
