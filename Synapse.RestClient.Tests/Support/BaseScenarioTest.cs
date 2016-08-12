using Synapse.RestClient.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient
{
    using User;
    using Node;
    public abstract class BaseScenarioTest : BaseTest
    {
        protected Person Person { get; set; }

        public override void Init()
        {
            base.Init();
            this.Person = this.CreatePerson(SynapseTestDocumentValues.PassValidationNoVerificationRequired);
        }

        protected virtual Person CreatePerson(string ssn)
        {
            return Person.CreateRandom(ssn);
        }

        protected SynapseApiUserCredentials CreateSynapseApiUser()
        {
            return new SynapseApiUserCredentials()
            {
                Fingerprint = Fingerprint,
                IpAddressOfUser = IpAddress
            };
        }

        protected CreateUserRequest CreateUserRequest()
        {
            return new CreateUserRequest
            {
                Logins = new CreateUserRequestLogin[]
                {
                    new CreateUserRequestLogin {
                        Email = this.Person.EmailAddress,
                        ReadOnly = true
                    }
                },
                PhoneNumbers = new string[] { "555-123-1233" },
                LegalNames = new string[] { this.Person.FirstName },
                Extra = new CreateUserRequestExtra
                {
                    SuppId = "LocalId",
                    CipTag = 1
                },
            };
        }

        protected AddDocumentsRequest CreateAddDocumentsRequest()
        {
            return new AddDocumentsRequest()
            {
                Documents = new AddDocumentsRequestDocument[]
                {
                    new AddDocumentsRequestDocument
                    {
                        Email = this.Person.EmailAddress,
                        PhoneNumber = "555-123-1233",
                        Ip = IpAddress,
                        Name = string.Format("{0} {0}", this.Person.FirstName, this.Person.LastName),
                        Alias = this.Person.FirstName,
                        EntityType = SynapseEntityType.Male,
                        EntityScope = SynapseEntityScope.ArtsAndEntertainment,
                        Day = DateTime.UtcNow.Day,
                        Month = DateTime.UtcNow.Month,
                        Year = DateTime.UtcNow.Year,
                        AddressStreet = "1428 Elm Street #4",
                        AddressCity = "Brooklyn",
                        AddressSubdivision = "NY",
                        AddressPostalCode = "11215",
                        AddressCountryCode = "US",
                        VirtualDocs = new AddDocumentsRequestVirtualDocumentSubmission[]
                        {
                            new AddDocumentsRequestVirtualDocumentSubmission()
                            {
                                DocumentType = SynapseVirtualDocumentType.SSN,
                                DocumentValue = this.Person.DocumentValue
                            }
                        },
                        PhysicalDocs = new AddDocumentsRequestPhysicalDocumentSubmission[]
                        {
                            new AddDocumentsRequestPhysicalDocumentSubmission()
                            {
                                DocumentType = SynapsePhysicalDocumentType.SubscriptionAgreement,
                                DocumentValue = GetTextResource("Base64Attachment.txt")
                                //DocumentValue = "data:text/csv;base64,SUQsTmFtZSxUb3RhbCAoaW4gJCksRmVlIChpbiAkKSxOb3RlLFRyYW5zYWN0aW9uIFR5cGUsRGF0ZSxTdGF0dXMNCjUxMTksW0RlbW9dIEJlbHogRW50ZXJwcmlzZXMsLTAuMTAsMC4wMCwsQmFuayBBY2NvdW50LDE0MzMxNjMwNTEsU2V0dGxlZA0KNTExOCxbRGVtb10gQmVseiBFbnRlcnByaXNlcywtMS4wMCwwLjAwLCxCYW5rIEFjY291bnQsMTQzMzE2MjkxOSxTZXR0bGVkDQo1MTE3LFtEZW1vXSBCZWx6IEVudGVycHJpc2VzLC0xLjAwLDAuMDAsLEJhbmsgQWNjb3VudCwxNDMzMTYyODI4LFNldHRsZWQNCjUxMTYsW0RlbW9dIEJlbHogRW50ZXJwcmlzZXMsLTEuMDAsMC4wMCwsQmFuayBBY2NvdW50LDE0MzMxNjI2MzQsU2V0dGxlZA0KNTExNSxbRGVtb10gQmVseiBFbnRlcnByaXNlcywtMS4wMCwwLjAwLCxCYW5rIEFjY291bnQsMTQzMzE2MjQ5OCxTZXR0bGVkDQo0ODk1LFtEZW1vXSBMRURJQyBBY2NvdW50LC03LjAwLDAuMDAsLEJhbmsgQWNjb3VudCwxNDMyMjUwNTYyLFNldHRsZWQNCjQ4MTIsS2FyZW4gUGF1bCwtMC4xMCwwLjAwLCxCYW5rIEFjY291bnQsMTQzMTk5NDAzNixTZXR0bGVkDQo0NzgwLFNhbmthZXQgUGF0aGFrLC0wLjEwLDAuMDAsLEJhbmsgQWNjb3VudCwxNDMxODQ5NDgxLFNldHRsZWQNCjQzMTUsU2Fua2FldCBQYXRoYWssLTAuMTAsMC4wMCwsQmFuayBBY2NvdW50LDE0Mjk3NzU5MzcsU2V0dGxlZA0KNDMxNCxTYW5rYWV0IFBhdGhhaywtMC4xMCwwLjAwLCxCYW5rIEFjY291bnQsMTQyOTc3NTQzNCxTZXR0bGVkDQo0MzEzLFNhbmthZXQgUGF0aGFrLC0wLjEwLDAuMDAsLEJhbmsgQWNjb3VudCwxNDI5Nzc1MzY0LFNldHRsZWQNCjQzMTIsU2Fua2FldCBQYXRoYWssLTAuMTAsMC4wMCwsQmFuayBBY2NvdW50LDE0Mjk3NzUyNTAsU2V0dGxlZA0KNDMxMSxTYW5rYWV0IFBhdGhhaywtMC4xMCwwLjAwLCxCYW5rIEFjY291bnQsMTQyOTc3NTAxMyxTZXR0bGVkDQo0MjM1LFtEZW1vXSBCZWx6IEVudGVycHJpc2VzLC0wLjEwLDAuMDAsLEJhbmsgQWNjb3VudCwxNDI5MzMxODA2LFNldHRsZWQNCjQxMzYsU2Fua2FldCBQYXRoYWssLTAuMTAsMC4wMCwsQmFuayBBY2NvdW50LDE0Mjg4OTA4NjMsU2V0dGxlZA0KNDAzMCxTYW5rYWV0IFBhdGhhaywtMC4xMCwwLjAwLCxCYW5rIEFjY291bnQsMTQyODIxNTM5NixTZXR0bGVkDQo0MDE0LFtEZW1vXSBCZWx6IEVudGVycHJpc2VzLC0wLjEwLDAuMDAsLEJhbmsgQWNjb3VudCwxNDI4MTI1MzgwLENhbmNsZWQNCjM4MzIsU2Fua2FldCBQYXRoYWssLTAuMTAsMC4wMCwsQmFuayBBY2NvdW50LDE0MjcxMDc0NzAsU2V0dGxlZA0KMzgyNixTYW5rYWV0IFBhdGhhaywtMC4xMCwwLjAwLCxCYW5rIEFjY291bnQsMTQyNzAzNTM5MixTZXR0bGVkDQozODI1LFNhbmthZXQgUGF0aGFrLC0wLjEwLDAuMDAsLEJhbmsgQWNjb3VudCwxNDI3MDMyOTM3LFNldHRsZWQNCg=="
                            }
                        }
                    }
                }
            };
        }

        protected AddACHNodeRequest CreateAddACHNodeRequest()
        {
            return new AddACHNodeRequest()
            {
                Info = new AddACHNodeRequestInfo()
                {
                    Nickname = "Freddy's Chase Checking",
                    //NameOnAccount = "Freddy Krueger Jr.",
                    AccountNum = "12345678",
                    RoutingNum = "021000021", //Chase NYC
                    Type = SynapseACHNodeType.Personal,
                    Class = SynapseACHNodeClass.Checking,
                },
                Extra = new AddACHNodeRequestExtra()
                {
                    SuppId = "1234"
                }
            };
        }
    }

}
