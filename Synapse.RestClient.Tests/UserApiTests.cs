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
        public async Task RefreshToken()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());
            var resp = await this._user.RefreshTokenAsync(apiUser, user.Id, new RefreshTokenRequest() { RefreshToken = user.RefreshToken });

            resp.ShouldNotBeNull();
            resp.OAuthKey.ShouldNotBeNull();
            resp.RefreshToken.ShouldNotBeNull();
            resp.ExpiresAt.ShouldBeGreaterThan(DateTimeOffset.UtcNow);
        }

        [TestMethod]
        public async Task CreatesUser()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());

            user.ShouldNotBeNull();
            user.RefreshToken.ShouldNotBeEmpty();
            user.Permission.ShouldEqual(SynapsePermission.Unverified);
        }

        [TestMethod]
        public async Task GetsUser()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());
            user = await this._user.GetUserAsync(apiUser, user.Id);

            user.ShouldNotBeNull();
            user.RefreshToken.ShouldNotBeEmpty();
            user.Permission.ShouldEqual(SynapsePermission.Unverified);
        }

        [TestMethod]
        public async Task AddsDocuments()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());
            var refreshToken = await this._user.RefreshTokenAsync(apiUser, user.Id, new RefreshTokenRequest() { RefreshToken = user.RefreshToken });
            apiUser.OAuthKey = refreshToken.OAuthKey;

            user = await this._user.AddDocumentsAsync(apiUser, user.Id, this.CreateAddDocumentsRequest());
            user.ShouldNotBeNull();
            user.Documents.ShouldNotBeEmpty();
            user.Documents[0].PhysicalDocs.ShouldNotBeEmpty();
            user.Documents[0].VirtualDocs.ShouldNotBeEmpty();
            user.Documents[0].SocialDocs.Length.ShouldEqual(2);
            //user.Permission.ShouldEqual(SynapsePermission.ReceiveOnly); //TODO: Discuss
            user.HasKBAQuestions.ShouldBeFalse();
        }

        [TestMethod]
        public async Task AddKBADocumentAndUpdateAnswer()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());
            var refreshToken = await this._user.RefreshTokenAsync(apiUser, user.Id, new RefreshTokenRequest() { RefreshToken = user.RefreshToken });
            apiUser.OAuthKey = refreshToken.OAuthKey;
            var createDocumentsRequest = this.CreateAddDocumentsRequest();
            createDocumentsRequest.Documents[0].VirtualDocs[0].DocumentValue = SynapseTestDocumentValues.PassValidationButVerificationRequired;
            user = await this._user.AddDocumentsAsync(apiUser, user.Id, createDocumentsRequest);

            user.ShouldNotBeNull();
            user.Documents[0].VirtualDocs[0].Meta.ShouldNotBeNull();
            user.HasKBAQuestions.ShouldBeTrue();
            user.Documents[0].VirtualDocs[0].Meta.QuestionSet.Questions.Length.ShouldEqual(5);

            var updateDocumentsRequest = new UpdateDocumentsRequest()
            {
                Documents = new UpdateDocumentsRequestDocument[]
                {
                    new UpdateDocumentsRequestDocument()
                    {
                        Id = user.Documents[0].Id,
                        Email = "me@joaomsa.com",
                        VirtualDocs = new UpdateDocumentsRequestVirtualDocumentSubmission[]
                        {
                            new UpdateDocumentsRequestVirtualDocumentSubmission()
                            {
                                Id = user.Documents[0].VirtualDocs[0].Id,
                                Meta = new UpdateDocumentsRequestVirtualDocumentMeta()
                                {
                                    QuestionSet = new UpdateDocumentsRequestVirtualDocumentQuestionSet()
                                    {
                                        Answers = new UpdateDocumentsRequestVirtualDocumentAnswer[]
                                        {
                                            new UpdateDocumentsRequestVirtualDocumentAnswer()
                                            {
                                                QuestionId = user.Documents[0].VirtualDocs[0].Meta.QuestionSet.Questions[0].Id,
                                                AnswerId = user.Documents[0].VirtualDocs[0].Meta.QuestionSet.Questions[0].Answers[2].Id,
                                            },
                                            new UpdateDocumentsRequestVirtualDocumentAnswer()
                                            {
                                                QuestionId = user.Documents[0].VirtualDocs[0].Meta.QuestionSet.Questions[1].Id,
                                                AnswerId = user.Documents[0].VirtualDocs[0].Meta.QuestionSet.Questions[1].Answers[1].Id,
                                            },
                                            new UpdateDocumentsRequestVirtualDocumentAnswer()
                                            {
                                                QuestionId = user.Documents[0].VirtualDocs[0].Meta.QuestionSet.Questions[3].Id,
                                                AnswerId = user.Documents[0].VirtualDocs[0].Meta.QuestionSet.Questions[3].Answers[4].Id,
                                            },
                                            new UpdateDocumentsRequestVirtualDocumentAnswer()
                                            {
                                                QuestionId = user.Documents[0].VirtualDocs[0].Meta.QuestionSet.Questions[2].Id,
                                                AnswerId = user.Documents[0].VirtualDocs[0].Meta.QuestionSet.Questions[2].Answers[2].Id,
                                            },
                                            new UpdateDocumentsRequestVirtualDocumentAnswer()
                                            {
                                                QuestionId = user.Documents[0].VirtualDocs[0].Meta.QuestionSet.Questions[4].Id,
                                                AnswerId = user.Documents[0].VirtualDocs[0].Meta.QuestionSet.Questions[4].Answers[4].Id,
                                            },
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var updatedUser = await this._user.UpdateDocumentsAsync(apiUser, user.Id, updateDocumentsRequest);

            updatedUser.ShouldNotBeNull();
            updatedUser.Documents[0].VirtualDocs[0].Meta.ShouldBeNull();
            updatedUser.HasKBAQuestions.ShouldBeFalse();
            updatedUser.Documents[0].SocialDocs.First(d => d.DocumentType == SynapseSocialDocumentType.Email).LastUpdated.
                ShouldBeGreaterThan(user.Documents[0].SocialDocs.First(d => d.DocumentType == SynapseSocialDocumentType.Email).LastUpdated);
            updatedUser.Documents[0].SocialDocs.First(d => d.DocumentType == SynapseSocialDocumentType.PhoneNumber).LastUpdated.
                ShouldEqual(user.Documents[0].SocialDocs.First(d => d.DocumentType == SynapseSocialDocumentType.PhoneNumber).LastUpdated);
        }

        [TestMethod]
        public async Task UpdateUser()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());

            var updateUserRequest = new UpdateUserRequest()
            {
                RefreshToken = user.RefreshToken,
                Update = new UpdateUserRequestUpdate()
                {
                    PhoneNumber = "555-555-555",
                    RemovePhoneNumber = "555-123-1233",
                    Login = new UpdateUserRequestLogin()
                    {
                        Email = "me+test@joaomsa.com",
                        ReadOnly = true
                    }
                }
            };
            var updatedUser = await this._user.UpdateUserAsync(apiUser, user.Id, updateUserRequest);
            var createDocumentsRequest = this.CreateAddDocumentsRequest();

            updatedUser.ShouldNotBeNull();
            updatedUser.Id.ShouldEqual(user.Id);
            updatedUser.Logins.Length.ShouldEqual(2);
            updatedUser.PhoneNumbers.Length.ShouldEqual(1);
            updatedUser.PhoneNumbers[0].ShouldEqual("555-555-555");
        }

        [TestMethod]
        public async Task SearchExistingUser()
        {
            var apiUser = this.CreateSynapseApiUser();
            var user = await this._user.CreateUserAsync(apiUser, this.CreateUserRequest());

            var results = await this._user.SearchUsers(apiUser, query: this.Person.EmailAddress, page: 1);

            results.Success.ShouldBeTrue();
            results.Page.ShouldEqual(1);
            results.Users.ShouldNotBeNull();
            results.Users.Length.ShouldEqual(1);

            var found = results.Users.First();
            found.Id.ShouldEqual(user.Id);
            found.Permission.ShouldEqual(user.Permission);
            found.Extra.DateJoined.ShouldBeGreaterThan(DateTime.UtcNow.AddSeconds(-2)); //TODO: Hacky
        }

        [TestMethod]
        public async Task SearchNonExistingUser()
        {
            var apiUser = this.CreateSynapseApiUser();

            var results = await this._user.SearchUsers(apiUser, query: this.Person.EmailAddress);

            results.Success.ShouldBeTrue();
            results.Users.ShouldNotBeNull();
            results.Users.Length.ShouldEqual(0);
        }
    }
}