namespace Cinema_Management_System.Models
{
    public class Actor
    {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Nationality { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public int MovieID { get; set; }
        public Movie? Movies { get; set; }
    }
}
