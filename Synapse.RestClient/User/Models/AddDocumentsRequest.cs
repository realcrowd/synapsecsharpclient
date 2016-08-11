using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.User
{
    public class AddDocumentsRequest
    {
        public AddDocumentsRequestDocument[] Documents { get; set; }
    }

    public class AddDocumentsRequestDocument
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Ip { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        [JsonConverter(typeof(SynapseEntityTypeEnumConverter))]
        public SynapseEntityType EntityType { get; set; }

        [JsonConverter(typeof(SynapseEntityScopeEnumConverter))]
        public SynapseEntityScope EntityScope { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public string AddressStreet { get; set; }

        public string AddressCity { get; set; }

        public string AddressSubdivision { get; set; }

        public string AddressPostalCode { get; set; }

        public string AddressCountryCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AddDocumentsRequestPhysicalDocumentSubmission[] PhysicalDocs { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AddDocumentsRequestVirtualDocumentSubmission[] VirtualDocs { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AddDocumentsRequestSocialDocumentSubmission[] SocialDocs { get; set; }
    }

    public abstract class AddDocumentsRequestAbstractDocumentSubmission
    {
        public string DocumentValue { get; set; }
    }

    public class AddDocumentsRequestPhysicalDocumentSubmission : AddDocumentsRequestAbstractDocumentSubmission
    {
        [JsonConverter(typeof(SynapsePhysicalDocumentTypeEnumConverter))]
        public SynapsePhysicalDocumentType DocumentType { get; set; }
    }

    public class AddDocumentsRequestVirtualDocumentSubmission : AddDocumentsRequestAbstractDocumentSubmission
    {
        [JsonConverter(typeof(SynapseVirtualDocumentTypeEnumConverter))]
        public SynapseVirtualDocumentType DocumentType { get; set; }
    }

    public class AddDocumentsRequestSocialDocumentSubmission : AddDocumentsRequestAbstractDocumentSubmission
    {
        [JsonConverter(typeof(SynapseSocialDocumentTypeEnumConverter))]
        public SynapseSocialDocumentType DocumentType { get; set; }
    }
}
