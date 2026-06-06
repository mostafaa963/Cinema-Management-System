using Cinema_Management_System.DataAccess;

namespace Cinema_Management_System.Models
{
    public class PromotionUserUsage
    {
        public int Id { get; set; }
        public string ApplicationuserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int productPromotionId { get; set; }
        public ProductPromotion productPromotion { get; set; }
        public string Code { get; set; }

        public DateTime UsedAt { get; set; }= DateTime.UtcNow;
    }
}
