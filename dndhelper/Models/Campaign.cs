using System.ComponentModel.DataAnnotations;

namespace dndhelper.Models
{
    public class Campaign
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
