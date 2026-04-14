using System.ComponentModel.DataAnnotations;

namespace DnDManager.Models
{
    public class SessionLog
    {
        public int SessionLogId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Session Date")]
        public DateTime SessionDate { get; set; }

        public string? Summary { get; set; }

        public string? Notes { get; set; }

        // Foreign key
        public int CampaignId { get; set; }
        public Campaign? Campaign { get; set; }
    }
}