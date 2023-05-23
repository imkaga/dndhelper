using System.ComponentModel.DataAnnotations;

namespace dndhelper.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Strength { get; set; }

        public int Dexterity { get; set; }

        public int Intelligence { get; set; }

        public int Luck { get; set; }

        [Required]
        public int CampaignId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
