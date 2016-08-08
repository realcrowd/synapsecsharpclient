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
        [JsonProperty("documents")]
        public AddDocumentsRequestDocument[] Documents { get; set; }
    }

    public class AddDocumentsRequestDocument
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("entity_type")]
        [JsonConverter(typeof(SynapseEntityTypeEnumConverter))]
        public SynapseEntityType EntityType { get; set; }

        [JsonProperty("entity_scope")]
        [JsonConverter(typeof(SynapseEntityScopeEnumConverter))]
        public SynapseEntityScope EntityScope { get; set; }

        [JsonProperty("day")]
        public int Day { get; set; }

        [JsonProperty("month")]
        public int Month { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("address_street")]
        public string AddressStreet { get; set; }

        [JsonProperty("address_city")]
        public string AddressCity { get; set; }

        [JsonProperty("address_subdivision")]
        public string AddressSubdivision { get; set; }

        [JsonProperty("address_postal_code")]
        public string AddressPostalCode { get; set; }

        [JsonProperty("address_country_code")]
        public string AddressCountryCode { get; set; }

        [JsonProperty("physical_docs", NullValueHandling = NullValueHandling.Ignore)]
        public AddDocumentsRequestPhysicalDocumentSubmission[] PhysicalDocs { get; set; }

        [JsonProperty("virtual_docs", NullValueHandling = NullValueHandling.Ignore)]
        public AddDocumentsRequestVirtualDocumentSubmission[] VirtualDocs { get; set; }

        [JsonProperty("social_docs", NullValueHandling = NullValueHandling.Ignore)]
        public AddDocumentsRequestSocialDocumentSubmission[] SocialDocs { get; set; }
    }

    public abstract class AddDocumentsRequestAbstractDocumentSubmission
    {
        [JsonProperty("document_value")]
        public string DocumentValue { get; set; }
    }

    public class AddDocumentsRequestPhysicalDocumentSubmission : AddDocumentsRequestAbstractDocumentSubmission
    {
        [JsonProperty("document_type")]
        [JsonConverter(typeof(SynapsePhysicalDocumentTypeEnumConverter))]
        public SynapsePhysicalDocumentType DocumentType { get; set; }
    }

    public class AddDocumentsRequestVirtualDocumentSubmission : AddDocumentsRequestAbstractDocumentSubmission
    {
        [JsonProperty("document_type")]
        [JsonConverter(typeof(SynapseVirtualDocumentTypeEnumConverter))]
        public SynapseVirtualDocumentType DocumentType { get; set; }
    }

    public class AddDocumentsRequestSocialDocumentSubmission : AddDocumentsRequestAbstractDocumentSubmission
    {
        [JsonProperty("document_type")]
        [JsonConverter(typeof(SynapseSocialDocumentTypeEnumConverter))]
        public SynapseSocialDocumentType DocumentType { get; set; }
    }
}
