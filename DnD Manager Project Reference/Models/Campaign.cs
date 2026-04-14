using System.ComponentModel.DataAnnotations;

namespace DnDManager.Models
{
    public class Campaign
    {
        public int CampaignId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Campaign Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        // Navigation properties
        public List<Character> Characters { get; set; } = new List<Character>();
        public List<SessionLog> SessionLogs { get; set; } = new List<SessionLog>();
        public List<Combatant> Combatants { get; set; } = new List<Combatant>();
    }
}