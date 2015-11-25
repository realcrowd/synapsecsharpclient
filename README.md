# Unofficial SynapsePay .NET Rest Client

Provides API interfaces & strongly-typed request/responses for SynapsePay's v3 API.

# Usage
## Simplest
```csharp
var api = new SynapseUserApiClient(new SynapseApiCredentials { 
            ClientId="<clientid>", 
            ClientSecret="<clientsecret>"
}, "https://sandbox.synapsepay.com/api/v3/");
var resp = await api.CreateUserAsync(new CreateUserRequest
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

## Webhook HMAC Verification
Helper function implementing https://discuss.synapsepay.com/t/hmac-for-web-hooks/17
```csharp
HMACSignatureValidator.IsValid(signature, secret, oid, date);
```
This can be used in an ActionFilter to authenticate your requests
```csharp
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateSynapseHMACAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var secret = ConfigurationManager.AppSettings["SynapseClientSecret"];
            var headers = actionContext.ControllerContext.Request.Headers;
            var values = (IEnumerable<string>)null;
            if (headers.TryGetValues("X-Synapse-Signature", out values))
            {
                var signature = values.First();
                var request = actionContext.ControllerContext.Request;
                dynamic data = actionContext.ActionArguments["request"];
                var oid = Convert.ToString(data._id["$oid"]);
                var date = Convert.ToString(data.recent_status.date["$date"]);

                if (HMACValidator.IsValid(signature, secret, oid, date))
                {
                    base.OnActionExecuting(actionContext);
                    return;
                } 
            }
            throw new HttpResponseException(System.Net.HttpStatusCode.Forbidden);
            
        }


    }
```
