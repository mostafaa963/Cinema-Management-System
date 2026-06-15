using Cinema_Management_System.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema_Management_System.Models
{
    [NotMapped]
    public class UserPromotionUsage
    {
        public int Id { get; set; }
        public string ApplicationuserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int UserPromotionId { get; set; }
        public UserPromotion UserPromotion { get; set; }
        public string Code { get; set; }

        public DateTime UsedAt { get; set; } = DateTime.UtcNow;
    }
}
