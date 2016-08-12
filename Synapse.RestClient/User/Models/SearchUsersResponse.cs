using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.User
{
    public class SearchUsersResponse
    {
        public string ErrorCode { get; set; }

        public string HttpCode { get; set; }

        public int Page { get; set; }

        public int PageCount { get; set; }

        public bool Success { get; set; }

        public UserResponse[] Users { get; set; }

        public int UsersCount { get; set; }
    }
}
