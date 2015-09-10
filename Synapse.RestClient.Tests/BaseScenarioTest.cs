using Synapse.RestClient.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient
{
    public abstract class BaseScenarioTest : BaseTest
    {
        protected Person Person { get; set; }

        public override void Init()
        {
            base.Init();
            this.Person = this.CreatePerson();
        }

        protected abstract Person CreatePerson();


        protected CreateUserRequest CreateUserRequest()
        {
            return new CreateUserRequest
            {
                EmailAddress = this.Person.EmailAddress,
                FirstName = this.Person.FirstName,
                LastName = this.Person.EmailAddress,
                PhoneNumber = "555-123-1233",
                IpAddress = "10.1.0.1",
                LocalId = "LocalId",
                Fingerprint = Fingerprint
            };
        }

        protected AddKycRequest CreateKycRequest(SynapseUserOAuth oauth)
        {
            return new AddKycRequest
            {
                OAuth = oauth,
                Address1 = "1428 Elm Street",
                Address2 = "#4",
                City = "Brooklyn",
                State = "New York",
                PostalCode = "11215",
                CountryCode = "USA",
                DateOfBirth = DateTime.Parse("10/19/1979").Date,
                DocumentType = SynapseDocumentType.SSN,
                DocumentValue = this.Person.DocumentValue,
                Fingerprint = Fingerprint,
                FirstName = this.Person.FirstName,
                LastName = this.Person.LastName
            };
        }
    }

}
