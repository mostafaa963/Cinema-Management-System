namespace Cinema_Management_System.Models
{
    public class ProductPromotion
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Discount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ValidTo { get; set; } = DateTime.Now.AddDays(16);
        public bool Status { get; set; } = true;
        public int Usage { get; set; } = 100;

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
