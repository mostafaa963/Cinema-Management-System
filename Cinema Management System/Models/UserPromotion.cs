using Cinema_Management_System.DataAccess;

namespace Cinema_Management_System.Models
{
    public class UserPromotion
    {
        public int Id { get; set; }
        public string  Code { get; set; }
        public  double Discount { get; set; }

        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime ValidTo { get; set; } = DateTime.Now.AddDays(12);

        public string ApplicationuserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

    }
}
