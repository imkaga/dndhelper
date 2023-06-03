using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dndhelper.Models
{
    public class Campaign
    {
        public int Id { get; set; }

        [Required]
        [Remote(action: "IsCampaignNameUnique", controller: "Campaigns", ErrorMessage = "Campaign name cannot repeat.")]
        [DisplayName("Nazwa kampanii")]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }
        public int CampaignId { get; set; }
    }
}
