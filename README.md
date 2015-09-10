# Unofficial SynapsePay .NET Rest Client

Provides API interfaces & strongly-typed request/responses for SynapsePay's v3 API.

# Usage
## Simplest
```csharp
var api = new SynapseUserApiClient(new SynapseApiCredentials { ClientId="<clientid>", ClientSecret="<clientsecret>" }, "https://sandbox.synapsepay.com/api/v3/");
var resp = api.CreateUser(return new CreateUserRequest
            {
                EmailAddress = "olfred@sleepwell.biz",
                FirstName = "Freddy",
                LastName = "Krueger",
                PhoneNumber = "555-123-1233",
                IpAddress = "10.1.0.1",
                LocalId = "LocalId",
                Fingerprint = "<fingerprint>"
            });
Assert.IsTrue(resp.Success);
```

## If calling more than one resource type
```csharp
var creds = new SynapseApiCredentials
            {
                ClientId = ConfigurationManager.AppSettings["SynapseClientId"],
                ClientSecret = ConfigurationManager.AppSettings["SynapseClientSecret"]
            };
var url = ConfigurationManager.AppSettings["SynapseBaseUrl"];
var factory = new SynapseRestClientFactory(creds, url);
var userApi = factory.CreateUserApiClient();
var node = factory.CreateNodeApiClient();
//etc..
```
