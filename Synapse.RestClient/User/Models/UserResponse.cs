using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.User
{
    public class UserResponse
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_links")]
        public UserResponseLinks Links { get; set; }

        [JsonProperty("client")]
        public UserResponseClient Client { get; set; }

        [Obsolete]
        [JsonProperty("doc_status")]
        public UserResponseDocStatus DocStatus { get; set; }

        [JsonProperty("documents")]
        public UserResponseDocuments[] Documents { get; set; }

        [JsonProperty("extra")]
        public UserResponseExtra Extra { get; set; }

        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("legal_names")]
        public string[] LegalNames { get; set; }

        [JsonProperty("logins")]
        public UserResponseLogin[] Logins { get; set; }

        [JsonProperty("permission")]
        [JsonConverter(typeof(SynapsePermissionEnumConverter))]
        public SynapsePermission Permission { get; set; }

        [JsonProperty("phone_numbers")]
        public string[] PhoneNumbers { get; set; }

        [JsonProperty("photos")]
        public string[] Photos { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        public bool HasKBAQuestions
        {
            get {
                return this.Documents != null
                    && this.Documents.Any(d => d.VirtualDocs != null 
                        && d.VirtualDocs.Any(vd => vd.Meta?.QuestionSet != null));
            }
        }
    }

    public class UserResponseLinks
    {
        [JsonProperty("self")]
        public UserResponseLinksSelf Self { get; set; }
    }

    public class UserResponseLinksSelf
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class UserResponseClient
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class UserResponseDocStatus
    {
        [JsonProperty("physical_doc")]
        public string PhysicalDoc { get; set; }

        [JsonProperty("virtual_doc")]
        public string VirtualDoc { get; set; }
    }

    public class UserResponseDocuments
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("permission_scope")]
        public string PermissionScope { get; set; }

        [JsonProperty("physical_docs", NullValueHandling = NullValueHandling.Ignore)]
        public UserResponsePhysicalDocument[] PhysicalDocs { get; set; }

        [JsonProperty("virtual_docs", NullValueHandling = NullValueHandling.Ignore)]
        public UserResponseVirtualDocument[] VirtualDocs { get; set; }

        [JsonProperty("social_docs", NullValueHandling = NullValueHandling.Ignore)]
        public UserResponseSocialDocument[] SocialDocs { get; set; }
    }

    public abstract class UserResponseAbstractDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("last_updated")]
        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset LastUpdated { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class UserResponsePhysicalDocument : UserResponseAbstractDocument
    {
        [JsonProperty("document_type")]
        [JsonConverter(typeof(SynapsePhysicalDocumentTypeEnumConverter))]
        public SynapsePhysicalDocumentType DocumentType { get; set; }
    }

    public class UserResponseVirtualDocument : UserResponseAbstractDocument
    {
        [JsonProperty("document_type")]
        [JsonConverter(typeof(SynapseVirtualDocumentTypeEnumConverter))]
        public SynapseVirtualDocumentType DocumentType { get; set; }

        [JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
        public UserResponseVirtualDocumentMeta Meta { get; set; }
    }

    public class UserResponseVirtualDocumentMeta
    {
        [JsonProperty("question_set")]
        public UserResponseVirtualDocumentQuestionSet QuestionSet;
    }

    public class UserResponseVirtualDocumentQuestionSet
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("questions")]
        public UserResponseVirtualDocumentQuestion[] Questions { get; set; }
    }

    public class UserResponseVirtualDocumentQuestion
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("answers")]
        public UserResponseVirtualDocumentAnswer[] Answers { get; set; }
    }

    public class UserResponseVirtualDocumentAnswer
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("answer")]
        public string Answer { get; set; }
    }

    public class UserResponseSocialDocument : UserResponseAbstractDocument
    {
        [JsonProperty("document_type")]
        [JsonConverter(typeof(SynapseSocialDocumentTypeEnumConverter))]
        public SynapseSocialDocumentType DocumentType { get; set; }
    }

    public class UserResponseExtra
    {
        [JsonProperty("date_joined")]
        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset DateJoined { get; set; }

        [JsonProperty("extra_security")]
        public bool ExtraSecurity { get; set; }

        [JsonProperty("is_business")]
        public bool IsBusiness { get; set; }

        [JsonProperty("supp_id")]
        public string SuppId { get; set; }

        [JsonProperty("cip_tag")]
        public int CipTag { get; set; }
    }

    public class UserResponseLogin
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}