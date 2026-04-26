using Cinema_Management_System.Models;

namespace Cinema_Management_System
{
    public class UpdateMoviesVM
    {
        public Movie movie { get; set; }
        public IEnumerable<Cinema> Cinemas { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
    }
}
