using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp.Extensions.MonoHttp;

namespace Synapse.RestClient.User
{
    public interface ISynapseUserApiClient
    {
        Task<RefreshTokenResponse> RefreshTokenAsync(SynapseApiUserCredentials user, string userId, RefreshTokenRequest msg);

        Task<SearchUsersResponse> SearchUsers(SynapseApiUserCredentials apiUser, string query = null, int? page = null, int? perPage = null);

        Task<UserResponse> CreateUserAsync(SynapseApiUserCredentials user, CreateUserRequest msg);

        Task<UserResponse> UpdateUserAsync(SynapseApiUserCredentials user, string userId, UpdateUserRequest msg);

        Task<UserResponse> GetUserAsync(SynapseApiUserCredentials user, string userId);

        Task<UserResponse> AddDocumentsAsync(SynapseApiUserCredentials apiUser, string userId, AddDocumentsRequest msg);

        Task<UserResponse> UpdateDocumentsAsync(SynapseApiUserCredentials apiUser, string userId, UpdateDocumentsRequest msg);

        event RequestEventHandler OnAfterRequest;
    }

    public class SynapseUserApiClient : SynapseBaseApiClient, ISynapseUserApiClient
    {
        public SynapseUserApiClient(SynapseApiClientCredentials creds, string baseUrl)
            : base (creds, baseUrl)
        {
        }

        public SynapseUserApiClient(SynapseApiClientCredentials creds, IRestClient client)
            : base(creds, client)
        {
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(SynapseApiUserCredentials apiUser, string userId, RefreshTokenRequest msg)
        {
            var req = new RestRequest(String.Format("oauth/{0}", HttpUtility.UrlPathEncode(userId)), Method.POST);

            var resp = await ExecuteRequestAsync(apiUser, msg, req);
            return ParseResponse<RefreshTokenResponse>(resp);
        }

        public async Task<SearchUsersResponse> SearchUsers(SynapseApiUserCredentials apiUser, string query = null, int? page = null, int? perPage = null)
        {
            var req = new RestRequest("users", Method.GET);
            if (!String.IsNullOrEmpty(query))
            {
                req.AddQueryParameter("query", query);
            }
            if (page.HasValue)
            {
                req.AddQueryParameter("page", page.Value.ToString());
            }
            if (perPage.HasValue)
            {
                req.AddQueryParameter("per_page", perPage.Value.ToString());
            }

            var resp = await ExecuteRequestAsync(apiUser, null, req);
            return ParseResponse<SearchUsersResponse>(resp);
        }

        public async Task<UserResponse> CreateUserAsync(SynapseApiUserCredentials apiUser, CreateUserRequest msg)
        {
            var req = new RestRequest("users", Method.POST);

            var resp = await ExecuteRequestAsync(apiUser, msg, req);
            return ParseResponse<UserResponse>(resp);
        }

        public async Task<UserResponse> UpdateUserAsync(SynapseApiUserCredentials apiUser, string userId, UpdateUserRequest msg)
        {
            var req = new RestRequest(String.Format("users/{0}", HttpUtility.UrlPathEncode(userId)), Method.PATCH);

            var resp = await ExecuteRequestAsync(apiUser, msg, req);
            return ParseResponse<UserResponse>(resp);
        }

        public async Task<UserResponse> GetUserAsync(SynapseApiUserCredentials apiUser, string userId)
        {
            var req = new RestRequest(String.Format("users/{0}", HttpUtility.UrlPathEncode(userId)), Method.GET);

            var resp = await ExecuteRequestAsync(apiUser, null, req);
            return ParseResponse<UserResponse>(resp);
        }

        public async Task<UserResponse> AddDocumentsAsync(SynapseApiUserCredentials apiUser, string userId, AddDocumentsRequest msg)
        {
            var req = new RestRequest(String.Format("users/{0}", HttpUtility.UrlPathEncode(userId)), Method.PATCH);

            var resp = await ExecuteRequestAsync(apiUser, msg, req);
            return ParseResponse<UserResponse>(resp);
        }

        public async Task<UserResponse> UpdateDocumentsAsync(SynapseApiUserCredentials apiUser, string userId, UpdateDocumentsRequest msg)
        {
            var req = new RestRequest(String.Format("users/{0}", HttpUtility.UrlPathEncode(userId)), Method.PATCH);

            var resp = await ExecuteRequestAsync(apiUser, msg, req);
            return ParseResponse<UserResponse>(resp);
        }
    }
}