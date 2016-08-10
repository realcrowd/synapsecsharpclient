using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using Synapse.RestClient.Node;
using Synapse.RestClient.Transaction;
using Synapse.RestClient.User;

namespace Synapse.RestClient
{
    [TestClass]
    public class TransactionApiTests : BaseScenarioTest
    {
        private UserResponse Sender;
        private ACHNodeResponse SenderNode;

        private ISynapseTransactionApiClient _api;
        private SynapseApiUserCredentials _apiUser;

        [TestInitialize]
        public override void Init()
        {
            base.Init();
        }

        private async Task InitializeAsync()
        {
            var userApi = this.Factory.CreateUserClient();
            var nodeApi = this.Factory.CreateNodeClient();

            var apiUser = this.CreateSynapseApiUser();
            var user = await userApi.CreateUserAsync(apiUser, this.CreateUserRequest());
            var refreshToken = await userApi.RefreshTokenAsync(apiUser, user.Id, new RefreshTokenRequest() { RefreshToken = user.RefreshToken });
            apiUser.OAuthKey = refreshToken.OAuthKey;
            user = await userApi.AddDocumentsAsync(apiUser, user.Id, this.CreateAddDocumentsRequest());
            var result = await nodeApi.AddACHNodeAsync(apiUser, user.Id, this.CreateAddACHNodeRequest());
            var node = await nodeApi.VerifyNodeAsync(apiUser, user.Id, result.Nodes[0].Id, new VerifyNodeRequest()
            {
                MicroDeposits = new decimal[] { 0.10m, 0.10m }
            });

            this._apiUser = apiUser;
            this.Sender = user;
            this.SenderNode = (ACHNodeResponse)node;

            this._api = Factory.CreateTransactionClient();
        }

        [TestMethod]
        public async Task AddsTransaction()
        {
            await InitializeAsync();
            var trans = await this._api.AddTransactionAsync(this._apiUser, this.Sender.Id, this.SenderNode.Id, new AddTransactionRequest()
            {
                To = new AddTransactionRequestTo()
                {
                    Id =  "55e9da8a86c2733ede53dac8",
                    Type = SynapseNodeTransactionType.SYNAPSEUS
                },
                Amount = new AddTransactionRequestAmount()
                {
                    Amount = 109.10m,
                    //Currency = SynapseCurrencies.USD,
                },
                Extra = new AddTransactionRequestExtra()
                {
                    Ip = "10.0.0.1",
                    ProcessOn = 0,
                    Note = "Offer ALKJ-JA98",
                    SuppId = "1234"
                }
            });

            trans.ShouldNotBeNull();
            trans.Id.ShouldNotBeEmpty();
            trans.RecentStatus.Status.ShouldEqual(SynapseTransactionStatusCode.Created);

            var resp = await this._api.CommentOnTransactionAsync(this._apiUser, this.Sender.Id, this.SenderNode.Id, trans.Id, new CommentOnTransactionRequest()
            {
                Comment = "New test comment"
            });

            resp.Success.ShouldBeTrue();
            resp.HttpCode.ShouldEqual("200");
            resp.Transaction.Id.ShouldEqual(trans.Id);
            resp.Transaction.RecentStatus.Note.ShouldContain("New test comment");

            var results = await this._api.SearchTransactions(this._apiUser, this.Sender.Id, this.SenderNode.Id);

            results.Success.ShouldBeTrue();
            results.Page.ShouldEqual(1);
            results.Transactions.ShouldNotBeNull();
            results.Transactions.Length.ShouldEqual(1);
            results.Transactions[0].Id.ShouldEqual(trans.Id);

            var trans2 = await this._api.GetTransactionAsync(this._apiUser, this.Sender.Id, this.SenderNode.Id, trans.Id);

            trans2.Id.ShouldNotBeNull();
            trans2.Id.ShouldEqual(trans.Id);
            trans2.Amount.Amount.ShouldEqual(trans.Amount.Amount);
            trans2.Amount.Currency.ShouldEqual("USD");

            var trans3 = await this._api.DeleteTransactionAsync(this._apiUser, this.Sender.Id, this.SenderNode.Id, trans.Id);

            trans3.Id.ShouldNotBeNull();
            trans3.Id.ShouldEqual(trans.Id);
            trans3.RecentStatus.Status.ShouldEqual(SynapseTransactionStatusCode.Cancelled);

            results = await this._api.SearchTransactions(this._apiUser, this.Sender.Id, this.SenderNode.Id);

            results.Success.ShouldBeTrue();
            // Cancelled transactions still show up
            results.Page.ShouldEqual(1);
            results.Transactions.ShouldNotBeNull();
            results.Transactions.Length.ShouldEqual(1);
            results.Transactions[0].Id.ShouldEqual(trans.Id);
            results.Transactions[0].RecentStatus.Status.ShouldEqual(SynapseTransactionStatusCode.Cancelled);
        }
    }
}
