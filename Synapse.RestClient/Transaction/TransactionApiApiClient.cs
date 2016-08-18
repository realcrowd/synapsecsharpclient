using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions.MonoHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Transaction
{
    public interface ISynapseTransactionApiClient
    {
        Task<SearchTransactionsResponse> SearchTransactions(SynapseApiUserCredentials apiUser, string userId, string nodeId, int? page = null, int? perPage = null);

        Task<TransactionResponse> AddTransactionAsync(SynapseApiUserCredentials apiUser, string userId, string nodeId, AddTransactionRequest msg);

        Task<TransactionResponse> GetTransactionAsync(SynapseApiUserCredentials apiUser, string userId, string nodeId, string transId);

        Task<CommentOnTransactionResponse> CommentOnTransactionAsync(SynapseApiUserCredentials apiUser, string userId, string nodeId, string transId, CommentOnTransactionRequest msg);

        Task<TransactionResponse> DeleteTransactionAsync(SynapseApiUserCredentials apiUser, string userId, string nodeId, string transId);

        event RequestEventHandler OnAfterRequest;
    }

    public class SynapseTransactionApiClient : SynapseBaseApiClient, ISynapseTransactionApiClient
    {
        public SynapseTransactionApiClient(SynapseApiClientCredentials creds, string baseUrl, Action<SynapseApiErrorException> apiErrorLogger = null)
            : base (creds, baseUrl, apiErrorLogger)
        {
        }

        public SynapseTransactionApiClient(SynapseApiClientCredentials creds, IRestClient client, Action<SynapseApiErrorException> apiErrorLogger = null)
            : base(creds, client, apiErrorLogger)
        {
        }

        public async Task<SearchTransactionsResponse> SearchTransactions(SynapseApiUserCredentials apiUser, string userId, string nodeId, int? page = null, int? perPage = null)
        {
            var req = new RestRequest(String.Format("users/{0}/nodes/{1}/trans", 
                HttpUtility.UrlPathEncode(userId), HttpUtility.UrlPathEncode(nodeId)), Method.GET);
            if (page.HasValue)
            {
                req.AddQueryParameter("page", page.Value.ToString());
            }
            if (perPage.HasValue)
            {
                req.AddQueryParameter("per_page", perPage.Value.ToString());
            }

            var resp = await ExecuteRequestAsync(apiUser, null, req);
            return ParseResponse<SearchTransactionsResponse>(resp);
        }

        public async Task<TransactionResponse> AddTransactionAsync(SynapseApiUserCredentials apiUser, string userId, string nodeId, AddTransactionRequest msg)
        {
            var req = new RestRequest(String.Format("users/{0}/nodes/{1}/trans", 
                HttpUtility.UrlPathEncode(userId), HttpUtility.UrlPathEncode(nodeId)), Method.POST);

            var resp = await ExecuteRequestAsync(apiUser, msg, req);
            return ParseResponse<TransactionResponse>(resp);
        }

        public async Task<TransactionResponse> GetTransactionAsync(SynapseApiUserCredentials apiUser, string userId, string nodeId, string transId)
        {
            var req = new RestRequest(String.Format("users/{0}/nodes/{1}/trans/{2}", 
                HttpUtility.UrlPathEncode(userId), HttpUtility.UrlPathEncode(nodeId), HttpUtility.UrlPathEncode(transId)), Method.GET);

            var resp = await ExecuteRequestAsync(apiUser, null, req);
            return ParseResponse<TransactionResponse>(resp);
        }

        public async Task<CommentOnTransactionResponse> CommentOnTransactionAsync(SynapseApiUserCredentials apiUser, string userId, string nodeId, string transId, CommentOnTransactionRequest msg)
        {
            var req = new RestRequest(String.Format("users/{0}/nodes/{1}/trans/{2}", 
                HttpUtility.UrlPathEncode(userId), HttpUtility.UrlPathEncode(nodeId), HttpUtility.UrlPathEncode(transId)), Method.PATCH);

            var resp = await ExecuteRequestAsync(apiUser, msg, req);
            return ParseResponse<CommentOnTransactionResponse>(resp);
        }

        public async Task<TransactionResponse> DeleteTransactionAsync(SynapseApiUserCredentials apiUser, string userId, string nodeId, string transId)
        {
            var req = new RestRequest(String.Format("users/{0}/nodes/{1}/trans/{2}", 
                HttpUtility.UrlPathEncode(userId), HttpUtility.UrlPathEncode(nodeId), HttpUtility.UrlPathEncode(transId)), Method.DELETE);

            var resp = await ExecuteRequestAsync(apiUser, null, req);
            return ParseResponse<TransactionResponse>(resp);
        }

        /*
        public async Task<AddTransactionResponse> AddTransactionAsync(SynapseApiUser user, AddTransactionRequest msg)
        {
            var req = new RestRequest("trans/add", Method.POST);
            dynamic body = new
            {
                login = new {
                    //oauth_key = msg.OAuth.Key,
                },
                user = new {
                    fingerprint = msg.Fingerprint
                },
                trans = new
                {
                    from = new
                    {
                        type = Translate(msg.FromNodeType),
                        id = msg.FromNodeId
                    },
                    to = new
                    {
                        type = Translate(msg.ToNodeType),
                        id = msg.ToNodeId
                    },
                    amount = new
                    {
                        amount = msg.Amount,
                        currency = msg.Currency.ToString(),
                    },
                    extra = new
                    {
                        supp_id = msg.LocalId,
                        note = msg.Note,
                        ip = msg.IpAddress,
                        process_on = msg.ProcessOn
                    }
                }
                
            };
            if(msg.Fee > 0)
            {
                body.fees = new[] {new {fee = msg.Fee, note = msg.Note, to = new {id = msg.FeeNodeId}}};
            }
            req.AddJsonBody(body);

            var resp = await this._api.ExecuteTaskAsync(req);
            RaiseOnAfterRequest(body, req, resp);
            dynamic data = SimpleJson.DeserializeObject(resp.Content);

            if(resp.IsHttpOk() && data.success)
            {
                var oauth = data.oauth;
                var trans = data.trans;
                var status = trans.recent_status;
                return new AddTransactionResponse
                {
                    Success = true,
                    Message = ApiHelper.TryGetMessage(data),
                    TransactionOId = trans._id["$oid"],
                    Status =
                        new SynapseTransactionStatus()
                        {
                            OnUtc = ApiHelper.UnixTimestampInMillisecondsToUtc(Convert.ToInt64(status.date["$date"])),
                            Note = status.note,
                            StatusDescription = status.status,
                            Status = Enum.Parse(typeof(SynapseTransactionStatusCode), Convert.ToInt32(status.status_id).ToString())
                        }
                };
            }
            else
            {
                return new AddTransactionResponse() {Success = false, Message = ApiHelper.TryGetError(data)};
            }
        }
        */

        /*
        public async Task<CancelTransactionResponse> CancelTransactionAsync(SynapseApiUser user, CancelTransactionRequest msg)
        {
            var req = new RestRequest("trans/cancel", Method.POST);
            var _id = new Dictionary<string, string>()
            {
                { "$oid", msg.TransactionOId }
            };  
            dynamic body = new
            {
                login = new {
                    //oauth_key = msg.OAuth.Key,
                },
                user = new {
                    fingerprint = msg.Fingerprint
                },
                trans = new
                {
                    _id = _id,
                }
                
            };
            req.AddJsonBody(body);

            var resp = await this._api.ExecuteTaskAsync(req);
            RaiseOnAfterRequest(body, req, resp);
            dynamic data = SimpleJson.DeserializeObject(resp.Content);

            if(resp.IsHttpOk() && data.success)
            {
                var oauth = data.oauth;
                var trans = data.trans;
                var status = trans.recent_status;
                return new CancelTransactionResponse
                {
                    Success = true,
                    Message = ApiHelper.TryGetMessage(data),
                };
            }
            else
            {
                return new CancelTransactionResponse { Success = false, Message = ApiHelper.TryGetError(data) };
            }
        }
        */
    }
}
