using Cinema_Management_System.Models;

namespace Cinema_Management_System
{
    public class CinemaWithRelatedVM
    {
        public IEnumerable<Cinema> Cinemas { get; set; }
        public int totalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
