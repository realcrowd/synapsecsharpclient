using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Synapse.RestClient.User
{

    public interface ISynapseUserApiClient
    {
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest req);
        Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest req);

        Task<AddKycResponse> AddKycAsync(AddKycRequest req);
        Task<AddDocResponse> AddDocAsync(AddDocRequest req);
        Task<VerifyKYCInfoResponse> VerifyKYCInfo(VerifyKYCInfoRequest req);
        event RequestEventHandler OnAfterRequest;
    }    

    public class SynapseUserApiClient : ISynapseUserApiClient
    {
        SynapseApiCredentials _creds;
        IRestClient _api;
        public event RequestEventHandler OnAfterRequest = delegate { };
        public SynapseUserApiClient(SynapseApiCredentials creds, string baseUrl) : this(creds, new RestSharp.RestClient(new Uri(baseUrl, UriKind.Absolute)))
        {

        }

        public SynapseUserApiClient(SynapseApiCredentials creds, IRestClient client)
        {

            this._creds = creds;
            this._api = client;
        }


        public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest msg)
        {
            var req = new RestRequest("user/create", Method.POST);
            var body = new
            {
                client = new
                {
                    client_id = this._creds.ClientId,
                    client_secret = this._creds.ClientSecret
                },
                logins = new[] {
                    new {
                            email = msg.EmailAddress,
                            read_only = true
                        }
                },
                legal_names = new []
                {
                    String.Format("{0} {1}", msg.FirstName, msg.LastName)
                },
                phone_numbers = new[] { msg.PhoneNumber },
                fingerprints = new[] {
                    new {
                        fingerprint =  msg.Fingerprint
                    }
                },
                ips = new[] { msg.IpAddress },
                extra = new
                {
                    supp_id = msg.LocalId,
                    is_business = false

                }
            };
            req.AddJsonBody(body);

            var resp = await this._api.ExecuteTaskAsync(req);
            dynamic data = SimpleJson.DeserializeObject(resp.Content);
            RaiseOnAfterRequest(body, req, resp);

            if (resp.IsHttpOk() && data.success)
            {
                var oauth = data.oauth;
                return new CreateUserResponse
                {
                    Success = true,
                    Message = ApiHelper.TryGetMessage(data),
                    SynapseOId = data.user._id["$oid"],
                    SynapseClientId = data.user.client.id.ToString(),
                    OAuth = new SynapseUserOAuth
                    {                        
                        Key = oauth.oauth_key,                         
                        RefreshToken = oauth.refresh_token,
                        ExpirationUtc = ApiHelper.UnixTimestampToUtc(oauth.expires_at)
                    },
                    Permission = ParsePermission(data.user.permission)
                };
            }
            else
            {
                return new CreateUserResponse
                {
                    Message = ApiHelper.TryGetError(data),
                    Success = false
                };
            };
            
        }

        public async Task<AddKycResponse> AddKycAsync(AddKycRequest msg)
        {
            var req = new RestRequest("user/doc/add", Method.POST);
            var body = new
            {
                login = new
                {
                    oauth_key = msg.OAuth.Key
                },
                user = new
                {
                    doc = new
                    {
                        birth_day = msg.DateOfBirth.Day,
                        birth_month = msg.DateOfBirth.Month,
                        birth_year = msg.DateOfBirth.Year,
                        name_first = msg.FirstName,
                        name_last = msg.LastName,
                        address_street1 = msg.Address1,
                        address_postal_code = msg.PostalCode,
                        address_country_code = msg.CountryCode,
                        document_value = msg.DocumentValue,
                        document_type = msg.DocumentType.ToString().ToUpper()
                    },
                    fingerprint = msg.Fingerprint
                }
            };
            req.AddJsonBody(body);

            var resp = await this._api.ExecuteTaskAsync(req);
            dynamic data = SimpleJson.DeserializeObject(resp.Content);
            RaiseOnAfterRequest(body, req, resp);
            if (resp.IsHttpOk() && data.success)
            {                
                return new AddKycResponse
                {
                    Success = true,
                    Message = ApiHelper.TryGetMessage(data),
                    Permission = ParsePermission(data.user.permission),
                    HasKBAQuestions = false
                };
            }
            else
            {
                if (resp.StatusCode == System.Net.HttpStatusCode.Accepted && data.success)
                {
                    if (data.error_code == "10" && ApiHelper.PropertyExists(data, "question_set")) //Magic number for "need KBA"
                    {
                        var questions = new List<Question>();

                        foreach (dynamic q in data.question_set.questions)
                        {
                            var answers = new List<Answer>();
                            var question = new Question
                            {
                                Id = Convert.ToInt32(q.id),
                                Text = q.question
                            };
                            
                            foreach(dynamic a in q.answers)
                            {
                                answers.Add(new Answer {
                                    Id = Convert.ToInt32(a.id),
                                    Text = a.answer });
                            }

                            question.Answers = answers.OrderBy(c => c.Id).ToArray();
                            questions.Add(question);
                        }
                        var response = new AddKycResponse
                        {
                            HasKBAQuestions = true,
                            KBAQuestionSet = new QuestionSet {
                                Id = data.question_set.id,
                                Questions = questions.ToArray(),
                                CreatedAtUtc = DateTime.UtcNow //TODO: there is a timestamp from synapse, might be important                               
                            },
                            Success = true,
                            Message = ApiHelper.TryGetMessage(data),
                            Permission = SynapsePermission.Unverified

                        };
                        return response;
                    }
                }
                return new AddKycResponse
                {
                    Success = false,
                    Message = ApiHelper.TryGetError(data)
                };
            }
        }

        public async Task<VerifyKYCInfoResponse> VerifyKYCInfo(VerifyKYCInfoRequest msg)
        {
            var req = new RestRequest("user/doc/verify", Method.POST);
            var body = new
            {
                login = new
                {
                    oauth_key = msg.OAuth.Key,
                },
                user = new
                {
                    doc = new
                    {
                        question_set_id = msg.QuestionSetId,
                        answers = msg.Answers.Select(c=>new {  question_id = c.QuestionId, answer_id = c.AnswerId }).ToArray()
                    },
                    fingerprint = msg.Fingerprint
                },

            };
            req.AddJsonBody(body);

            var resp = await this._api.ExecuteTaskAsync(req);
            dynamic data = SimpleJson.DeserializeObject(resp.Content);
            RaiseOnAfterRequest(body, req, resp);

            if (resp.IsHttpOk() && data.success)
            {
                return new VerifyKYCInfoResponse
                {
                    Success = true,
                    Permission = ParsePermission(data.user.permission),
                    Message = ApiHelper.TryGetMessage(data)
                };
            }
            else
            {
                return new VerifyKYCInfoResponse
                {
                    Success = false,
                    Message = ApiHelper.TryGetError(data)
                };
            }

        }

        public async Task<AddDocResponse> AddDocAsync(AddDocRequest msg)
        {
            var req = new RestRequest("user/doc/attachments/add", Method.POST);
            var body = new
            {
                login = new
                {
                    oauth_key = msg.OAuth.Key,
                },
                user = new
                {
                    doc = new
                    {
                        attachment = msg.Attachment
                    },
                    fingerprint = msg.Fingerprint
                },
                
            };
            req.AddJsonBody(body);

            var resp = await this._api.ExecuteTaskAsync(req);
            dynamic data = SimpleJson.DeserializeObject(resp.Content);
            RaiseOnAfterRequest(body, req, resp);

            if(resp.IsHttpOk() && data.success)
            {
                return new AddDocResponse
                {
                    Success = true,
                    Permission = ParsePermission(data.user.permission),
                    Message = ApiHelper.TryGetMessage(data)
                };
            } else
            {
                return new AddDocResponse
                {
                    Success = false,
                    Message = ApiHelper.TryGetError(data)
                };
            }
        }

        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest msg)
        {
            var req = new RestRequest("user/signin", Method.POST);
            var _id = new Dictionary<string, string>()
            {
                { "$oid", msg.SynapseOId }
            };            

            dynamic body = new
            {
                client = new
                {
                    client_id = this._creds.ClientId,
                    client_secret = this._creds.ClientSecret
                },
                login = new
                {
                    refresh_token = msg.RefreshToken,
                },
                user = new
                {
                    _id = _id,
                    ip = msg.IPAddress,
                    fingerprint = msg.Fingerprint
                },

            };

            req.AddJsonBody(body);

            var resp = await this._api.ExecuteTaskAsync(req);
            dynamic data = SimpleJson.DeserializeObject(resp.Content);
            RaiseOnAfterRequest(body, req, resp);

            if (resp.IsHttpOk() && data.success)
            {
                var oauth = data.oauth;
                return new RefreshTokenResponse
                {
                    Success = true,
                    OAuth = new SynapseUserOAuth
                    {
                        Key = oauth.oauth_key,
                        RefreshToken = oauth.refresh_token,
                        ExpirationUtc = ApiHelper.UnixTimestampToUtc(Convert.ToInt64(oauth.expires_at))
                    },
                    Message = ApiHelper.TryGetMessage(data)
                };
            }
            else
            {
                return new RefreshTokenResponse
                {
                    Success = false,
                    Message = ApiHelper.TryGetError(data)
                };
            }
        }

        private void RaiseOnAfterRequest(object body, IRestRequest req, IRestResponse resp)
        {
            OnAfterRequest(resp.ResponseUri.ToString(), resp.StatusCode, SimpleJson.SerializeObject(body), resp.Content);
        }

        private static SynapsePermission ParsePermission(string permission)
        {
            if (permission == "UNVERIFIED") return SynapsePermission.Unverified;
            else if (permission == "RECEIVE") return SynapsePermission.ReceiveOnly;
            else if (permission == "SEND-AND-RECEIVE") return SynapsePermission.SendAndReceive;
            else if (permission == "LOCKED") return SynapsePermission.Locked;
            throw new InvalidOperationException(String.Format("Unknown permission type {0}", permission));
        }
    }
}
