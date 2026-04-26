namespace Cinema_Management_System
{
    public class UpdateInsetMoviesVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //public string MainImg { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        //public Category Home { get; set; } = null!;
        //public List<Actor>? Actors { get; set; }
        //public Cinema Cinema { get; set; } = null!;

        public int CategoryId { get; set; }
        public int CinemaID
        {
            get; set;
        }
    }
}
