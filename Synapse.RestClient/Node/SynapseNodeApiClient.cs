﻿using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions.MonoHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Node
{
    public interface ISynapseNodeApiClient
    {
        Task<SearchNodesResponse> SearchNodes(SynapseApiUserCredentials apiUser, string userId, string type = "ACH-US", int? page = null, int? perPage = null);

        Task<NodesResponse> AddACHNodeAsync(SynapseApiUserCredentials apiUser, string userId, AddACHNodeRequest msg);

        Task<ACHNodeResponse> GetNodeAsync(SynapseApiUserCredentials apiUser, string userId, string nodeId);

        Task<ACHNodeResponse> VerifyNodeAsync(SynapseApiUserCredentials apiUser, string userId, string nodeId, VerifyNodeRequest msg);

        Task<SynapseApiErrorResponse> DeleteNodeAsync(SynapseApiUserCredentials apiUser, string userId, string nodeId);

        event RequestEventHandler OnAfterRequest;
    }

    public class SynapseNodeApiClient : SynapseBaseApiClient, ISynapseNodeApiClient
    {
        public SynapseNodeApiClient(SynapseApiClientCredentials creds, string baseUrl)
            : base (creds, baseUrl)
        {
        }

        public SynapseNodeApiClient(SynapseApiClientCredentials creds, IRestClient client)
            : base(creds, client)
        {
        }

        public async Task<SearchNodesResponse> SearchNodes(SynapseApiUserCredentials apiUser, string userId, string type = "ACH-US", int? page = null, int? perPage = null)
        {
            var req = new RestRequest(String.Format("users/{0}/nodes", HttpUtility.UrlPathEncode(userId)), Method.GET);
            if (!String.IsNullOrEmpty(type))
            {
                req.AddQueryParameter("query", type);
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
            return ParseResponse<SearchNodesResponse>(resp);
        }

        public async Task<NodesResponse> AddACHNodeAsync(SynapseApiUserCredentials apiUser, string userId, AddACHNodeRequest msg)
        {
            var req = new RestRequest(String.Format("users/{0}/nodes", HttpUtility.UrlPathEncode(userId)), Method.POST);
            var body = JsonConvert.SerializeObject(msg);

            var resp = await ExecuteRequestAsync(apiUser, body, req);
            return ParseResponse<NodesResponse>(resp);
        }
        
        public async Task<ACHNodeResponse> GetNodeAsync(SynapseApiUserCredentials apiUser, string userId, string nodeId)
        {
            var req = new RestRequest(String.Format("users/{0}/nodes/{1}", 
                HttpUtility.UrlPathEncode(userId), HttpUtility.UrlPathEncode(nodeId)), Method.GET);

            var resp = await ExecuteRequestAsync(apiUser, null, req);
            return ParseResponse<ACHNodeResponse>(resp);
        }

        public async Task<ACHNodeResponse> VerifyNodeAsync(SynapseApiUserCredentials apiUser, string userId, string nodeId, VerifyNodeRequest msg)
        {
            var req = new RestRequest(String.Format("users/{0}/nodes/{1}", 
                HttpUtility.UrlPathEncode(userId), HttpUtility.UrlPathEncode(nodeId)), Method.PATCH);
            var body = JsonConvert.SerializeObject(msg);

            var resp = await ExecuteRequestAsync(apiUser, body, req);
            return ParseResponse<ACHNodeResponse>(resp);
        }

        public async Task<SynapseApiErrorResponse> DeleteNodeAsync(SynapseApiUserCredentials apiUser, string userId, string nodeId)
        {
            var req = new RestRequest(String.Format("users/{0}/nodes/{1}", 
                HttpUtility.UrlPathEncode(userId), HttpUtility.UrlPathEncode(nodeId)), Method.DELETE);

            var resp = await ExecuteRequestAsync(apiUser, null, req);
            return ParseResponse<SynapseApiErrorResponse>(resp);
        }
    }
}