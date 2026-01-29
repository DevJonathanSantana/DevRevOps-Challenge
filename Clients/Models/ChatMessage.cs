using System.Text.Json.Serialization;

namespace REVOPS.DevChallenge.Clients.Models
{
    public class ChatMessageResponse
    {
        [JsonPropertyName("items")]
        public List<ChatMessageItem>? Items { get; set; }
    }

    public class ChatMessageItem
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("messageType")]
        public string? MessageType { get; set; } 

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("createdAtUTC")]
        public DateTimeOffset CreatedAt { get; set; }

        
        
        [JsonPropertyName("source")]
        public string? Source { get; set; } 
    }
}