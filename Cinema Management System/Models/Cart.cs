using Cinema_Management_System.DataAccess;

namespace Cinema_Management_System.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string ApplicationuserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int Quantity { get; set; }

        public double ProductPrice { get; set; }
        public double TotalPrice { get; set; }

    }
}
