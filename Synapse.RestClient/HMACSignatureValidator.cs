using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient
{
    public static class HMACSignatureValidator
    {
        public static bool IsValid(string signature, string secret, string oid, string date)
        {
            if (String.IsNullOrEmpty(signature) || String.IsNullOrEmpty(secret) || String.IsNullOrEmpty(oid) || String.IsNullOrEmpty(date))
                return false;

            var encoding = Encoding.UTF8;
            var key = encoding.GetBytes(secret);
            var hmac = new HMACSHA1(key);
            var raw = encoding.GetBytes(String.Format("{0}+{1}", oid, date));
            var hash = hmac.ComputeHash(raw);
            var digest = BitConverter.ToString(hash).Replace("-", "").ToLower();
            var base64 = Convert.ToBase64String(encoding.GetBytes(digest));

            return signature == base64;
        }

    }
}
