using Cinema_Management_System.Models;

namespace Cinema_Management_System.ViewModel
{
    public class ActorWithRelatedVM
    {
        public IEnumerable<Actor> Actors { get; set; }
        public int totalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
