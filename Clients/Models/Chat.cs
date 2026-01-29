using System.Text.Json.Serialization;

namespace REVOPS.DevChallenge.Clients.Models
{
    public class Chat
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("createdAtUTC")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("organizationMember")]
        public ChatOrganizationMember? OrganizationMember { get; set; }

        [JsonPropertyName("lastOrganizationMember")]
        public ChatOrganizationMember? LastOrganizationMember { get; set; }

        [JsonPropertyName("contact")]
        public ChatContact? Contact { get; set; }

        [JsonPropertyName("tags")]
        public List<ChatTag>? Tags { get; set; }

        [JsonPropertyName("sector")]
        public ChatSector? Sector { get; set; }

        [JsonPropertyName("lastMessage")]
        public ChatLastMessage? LastMessage { get; set; }

    }

    public class ChatContact
    {
        
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        
        // --------------------------------------

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("emails")]
        public List<string>? Emails { get; set; }

        [JsonPropertyName("email")]
        public string? EmailSingular { get; set; }

        [JsonPropertyName("tags")]
        public List<ChatTag>? Tags { get; set; }
    }

    public class ChatTag
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class ChatSector
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
public class ChatOrganizationMember
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }
public class ChatLastMessage
    {
        [JsonPropertyName("createdAtUTC")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("messageType")]
        public string? MessageType { get; set; }

        [JsonPropertyName("source")]
        public string? Source { get; set; }
    }
    

}