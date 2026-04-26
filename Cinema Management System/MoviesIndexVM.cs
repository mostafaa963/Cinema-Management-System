using Cinema_Management_System.Models;

namespace Cinema_Management_System
{
    public class MoviesIndexVM
    {
        public IEnumerable<Movie> Movies { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
