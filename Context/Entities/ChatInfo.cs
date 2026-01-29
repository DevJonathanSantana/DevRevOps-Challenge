using System.ComponentModel.DataAnnotations;

namespace REVOPS.DevChallenge.Context.Entities
{
    public class ChatInfo
    {
        [Key]
        public int Id { get; set; } 
        public string? ChatId { get; set; } 
        public bool IsAnyAttendantAssigned { get; set; }
        public string? ContactName { get; set; }
        public string? ContactEmail { get; set; }
        public DateTime CreatedAt { get; set; } 
        public string? Tags { get; set; }
        public string? Sector { get; set; }
        public string? AgentName { get; set; }

        public DateTime LastActivityAt { get; set; }
}

    }

    
