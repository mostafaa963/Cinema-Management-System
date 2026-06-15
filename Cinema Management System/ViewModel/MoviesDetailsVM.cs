namespace Cinema_Management_System.ViewModel
{
    public class MoviesDetailsVM
    {
        public string Name { get; set; } = string.Empty;
        public string MainImg { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        


    }
}
