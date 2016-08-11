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
        public UpdateDocumentsRequestDocument[] Documents { get; set; }
    }

    public class UpdateDocumentsRequestDocument
    {
        public string Id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Ip { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Alias { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(SynapseEntityTypeEnumConverter))]
        public SynapseEntityType? EntityType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(SynapseEntityScopeEnumConverter))]
        public SynapseEntityScope? EntityScope { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Day { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Month { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Year { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string AddressStreet { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string AddressCity { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string AddressSubdivision { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string AddressPostalCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string AddressCountryCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UpdateDocumentsRequestPhysicalDocumentSubmission[] PhysicalDocs { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UpdateDocumentsRequestVirtualDocumentSubmission[] VirtualDocs { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UpdateDocumentsRequestSocialDocumentSubmission[] SocialDocs { get; set; }
    }

    public abstract class UpdateDocumentsRequestAbstractDocumentSubmission : AddDocumentsRequestAbstractDocumentSubmission 
    {
        public string Id { get; set; }
    }

    public class UpdateDocumentsRequestPhysicalDocumentSubmission : UpdateDocumentsRequestAbstractDocumentSubmission
    {
        [JsonConverter(typeof(SynapsePhysicalDocumentTypeEnumConverter))]
        public SynapsePhysicalDocumentType DocumentType { get; set; }
    }

    public class UpdateDocumentsRequestVirtualDocumentSubmission : UpdateDocumentsRequestAbstractDocumentSubmission
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(SynapseVirtualDocumentTypeEnumConverter))]
        public SynapseVirtualDocumentType? DocumentType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public new string DocumentValue { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UpdateDocumentsRequestVirtualDocumentMeta Meta { get; set; }
    }

    public class UpdateDocumentsRequestVirtualDocumentMeta
    {
        public UpdateDocumentsRequestVirtualDocumentQuestionSet QuestionSet { get; set; }
    }

    public class UpdateDocumentsRequestVirtualDocumentQuestionSet
    {
        public UpdateDocumentsRequestVirtualDocumentAnswer[] Answers { get; set; }
    }

    public class UpdateDocumentsRequestVirtualDocumentAnswer
    {
        public int QuestionId { get; set; }

        public int AnswerId { get; set; }
    }

    public class UpdateDocumentsRequestSocialDocumentSubmission : UpdateDocumentsRequestAbstractDocumentSubmission
    {
        [JsonConverter(typeof(SynapseSocialDocumentTypeEnumConverter))]
        public SynapseSocialDocumentType DocumentType { get; set; }
    }
}