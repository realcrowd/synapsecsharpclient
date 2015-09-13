using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient
{
    public static class ApiHelper
    {
        public static bool IsHttpOk(this IRestResponse r)
        {
            return r.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public static async Task<T> Execute<T>(this IRestClient client, IRestRequest req, Func<dynamic,T> onHttpOk, Func<dynamic,T> onHttpErr)
        {
            var resp = await client.ExecuteTaskAsync(req);
            dynamic data = SimpleJson.DeserializeObject(resp.Content);
            if (resp.IsHttpOk())
            {
                return onHttpOk(data);
            }
            else
            {
                return onHttpErr(data);
            }
        }

        public static string TryGetMessage(dynamic response)
        {
            if (!PropertyExists(response, "message")) return String.Empty;
            return response.message.en;
        }

        public static string TryGetError(dynamic response)
        {
            if (!PropertyExists(response, "error")) return String.Empty;
            return response.error.en;
        }

        public static bool PropertyExists(dynamic settings, string name)
        {
            return settings.ContainsKey(name);
        }

    }
}
