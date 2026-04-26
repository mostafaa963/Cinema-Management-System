namespace Cinema_Management_System.Models
{
    public class MoviesSubimage
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public Movie? Movie { get; set; }
        public string SubImage { get; set; } = string.Empty;
    }
}
