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

        public UserResponseClient Client { get; set; }

        [Obsolete]
        public UserResponseDocStatus DocStatus { get; set; }

        public UserResponseDocuments[] Documents { get; set; }

        public UserResponseExtra Extra { get; set; }

        public bool IsHidden { get; set; }

        public string[] LegalNames { get; set; }

        public UserResponseLogin[] Logins { get; set; }

        [JsonConverter(typeof(SynapsePermissionEnumConverter))]
        public SynapsePermission Permission { get; set; }

        public string[] PhoneNumbers { get; set; }

        public string[] Photos { get; set; }

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
        public UserResponseLinksSelf Self { get; set; }
    }

    public class UserResponseLinksSelf
    {
        public string Href { get; set; }
    }

    public class UserResponseClient
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class UserResponseDocStatus
    {
        public string PhysicalDoc { get; set; }

        public string VirtualDoc { get; set; }
    }

    public class UserResponseDocuments
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PermissionScope { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UserResponsePhysicalDocument[] PhysicalDocs { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UserResponseVirtualDocument[] VirtualDocs { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UserResponseSocialDocument[] SocialDocs { get; set; }
    }

    public abstract class UserResponseAbstractDocument
    {
        public string Id { get; set; }

        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset LastUpdated { get; set; }

        public string Status { get; set; }
    }

    public class UserResponsePhysicalDocument : UserResponseAbstractDocument
    {
        [JsonConverter(typeof(SynapsePhysicalDocumentTypeEnumConverter))]
        public SynapsePhysicalDocumentType DocumentType { get; set; }
    }

    public class UserResponseVirtualDocument : UserResponseAbstractDocument
    {
        [JsonConverter(typeof(SynapseVirtualDocumentTypeEnumConverter))]
        public SynapseVirtualDocumentType DocumentType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UserResponseVirtualDocumentMeta Meta { get; set; }
    }

    public class UserResponseVirtualDocumentMeta
    {
        public UserResponseVirtualDocumentQuestionSet QuestionSet;
    }

    public class UserResponseVirtualDocumentQuestionSet
    {
        public string Id { get; set; }

        public UserResponseVirtualDocumentQuestion[] Questions { get; set; }
    }

    public class UserResponseVirtualDocumentQuestion
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public UserResponseVirtualDocumentAnswer[] Answers { get; set; }
    }

    public class UserResponseVirtualDocumentAnswer
    {
        public int Id { get; set; }

        public string Answer { get; set; }
    }

    public class UserResponseSocialDocument : UserResponseAbstractDocument
    {
        [JsonConverter(typeof(SynapseSocialDocumentTypeEnumConverter))]
        public SynapseSocialDocumentType DocumentType { get; set; }
    }

    public class UserResponseExtra
    {
        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset DateJoined { get; set; }

        public bool ExtraSecurity { get; set; }

        public bool IsBusiness { get; set; }

        public string SuppId { get; set; }

        public int CipTag { get; set; }
    }

    public class UserResponseLogin
    {
        public string Email { get; set; }

        public string Scope { get; set; }
    }
}