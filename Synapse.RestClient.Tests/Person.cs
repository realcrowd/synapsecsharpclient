using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient
{
    public static class SynapseTestDocumentValues
    {
        public const string FailSSNValidation = "1111";
        public const string PassValidationNoVerification = "2222";
        public const string PassValidationButVerificationRequired = "3333";
    }
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public string DocumentValue { get; set; }

        public static Person CreateRandom(string documentValue)
        {
            return new Person
            {
                FirstName = RandomString(8),
                LastName = RandomString(8),
                EmailAddress = String.Format("{0}@{1}.com", RandomString(10), RandomString(10)),
                DocumentValue = documentValue
            };
        }
        private static string RandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            return new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
        }
    }
}
