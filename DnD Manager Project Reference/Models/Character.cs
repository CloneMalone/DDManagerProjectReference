using System.ComponentModel.DataAnnotations;

namespace DnDManager.Models
{
    public class Character
    {
        public int CharacterId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Class { get; set; }

        [StringLength(50)]
        public string? Race { get; set; }

        [Range(1, 20)]
        public int Level { get; set; } = 1;

        [Required]
        [Range(1, 999)]
        [Display(Name = "Max HP")]
        public int MaxHP { get; set; }

        [Required]
        [Range(0, 999)]
        [Display(Name = "Current HP")]
        public int CurrentHP { get; set; }

        [Range(0, 30)]
        [Display(Name = "Armor Class")]
        public int AC { get; set; }

        // Foreign key
        public int CampaignId { get; set; }
        public Campaign? Campaign { get; set; }
    }
}