using System.Text.Json.Serialization;

namespace REVOPS.DevChallenge.Clients.Models
{
    // Esta classe mapeia o objeto raiz que vem da rota /organizations/{id}
    public class OrganizationResponse
    {
        [JsonPropertyName("organizationMembers")]
        public List<AgentDetail>? Members { get; set; }
    }

    public class AgentDetail
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("displayName")] 
        public string? DisplayName { get; set; }

        [JsonPropertyName("emailAddress")] 
        public string? Email { get; set; }
        
        
        public string NomeFinal => !string.IsNullOrEmpty(DisplayName) ? DisplayName : (Email ?? "Sem Nome");
    }
}