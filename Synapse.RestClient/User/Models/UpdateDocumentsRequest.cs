using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.User
{
    public class UpdateDocumentsRequest
    {
        [JsonProperty("documents")]
        public UpdateDocumentsRequestDocument[] Documents { get; set; }
    }

    public class UpdateDocumentsRequestDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("phone_number", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }

        [JsonProperty("ip", NullValueHandling = NullValueHandling.Ignore)]
        public string Ip { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("alias", NullValueHandling = NullValueHandling.Ignore)]
        public string Alias { get; set; }

        [JsonProperty("entity_type", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(SynapseEntityTypeEnumConverter))]
        public SynapseEntityType? EntityType { get; set; }

        [JsonProperty("entity_scope", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(SynapseEntityScopeEnumConverter))]
        public SynapseEntityScope? EntityScope { get; set; }

        [JsonProperty("day", NullValueHandling = NullValueHandling.Ignore)]
        public int? Day { get; set; }

        [JsonProperty("month", NullValueHandling = NullValueHandling.Ignore)]
        public int? Month { get; set; }

        [JsonProperty("year", NullValueHandling = NullValueHandling.Ignore)]
        public int? Year { get; set; }

        [JsonProperty("address_street", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressStreet { get; set; }

        [JsonProperty("address_city", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressCity { get; set; }

        [JsonProperty("address_subdivision", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressSubdivision { get; set; }

        [JsonProperty("address_postal_code", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressPostalCode { get; set; }

        [JsonProperty("address_country_code", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressCountryCode { get; set; }

        [JsonProperty("physical_docs", NullValueHandling = NullValueHandling.Ignore)]
        public UpdateDocumentsRequestPhysicalDocumentSubmission[] PhysicalDocs { get; set; }

        [JsonProperty("virtual_docs", NullValueHandling = NullValueHandling.Ignore)]
        public UpdateDocumentsRequestVirtualDocumentSubmission[] VirtualDocs { get; set; }

        [JsonProperty("social_docs", NullValueHandling = NullValueHandling.Ignore)]
        public UpdateDocumentsRequestSocialDocumentSubmission[] SocialDocs { get; set; }
    }

    public abstract class UpdateDocumentsRequestAbstractDocumentSubmission : AddDocumentsRequestAbstractDocumentSubmission 
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class UpdateDocumentsRequestPhysicalDocumentSubmission : UpdateDocumentsRequestAbstractDocumentSubmission
    {
        [JsonProperty("document_type")]
        [JsonConverter(typeof(SynapsePhysicalDocumentTypeEnumConverter))]
        public SynapsePhysicalDocumentType DocumentType { get; set; }
    }

    public class UpdateDocumentsRequestVirtualDocumentSubmission : UpdateDocumentsRequestAbstractDocumentSubmission
    {
        [JsonProperty("document_type", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(SynapseVirtualDocumentTypeEnumConverter))]
        public SynapseVirtualDocumentType? DocumentType { get; set; }

        [JsonProperty("document_value", NullValueHandling = NullValueHandling.Ignore)]
        public new string DocumentValue { get; set; }

        [JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
        public UpdateDocumentsRequestVirtualDocumentMeta Meta { get; set; }
    }

    public class UpdateDocumentsRequestVirtualDocumentMeta
    {
        [JsonProperty("question_set")]
        public UpdateDocumentsRequestVirtualDocumentQuestionSet QuestionSet { get; set; }
    }

    public class UpdateDocumentsRequestVirtualDocumentQuestionSet
    {
        [JsonProperty("answers")]
        public UpdateDocumentsRequestVirtualDocumentAnswer[] Answers { get; set; }
    }

    public class UpdateDocumentsRequestVirtualDocumentAnswer
    {
        [JsonProperty("question_id")]
        public int QuestionId { get; set; }

        [JsonProperty("answer_id")]
        public int AnswerId { get; set; }
    }

    public class UpdateDocumentsRequestSocialDocumentSubmission : UpdateDocumentsRequestAbstractDocumentSubmission
    {
        [JsonProperty("document_type")]
        [JsonConverter(typeof(SynapseSocialDocumentTypeEnumConverter))]
        public SynapseSocialDocumentType DocumentType { get; set; }
    }
}