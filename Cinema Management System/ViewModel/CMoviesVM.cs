using Cinema_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Cinema_Management_System.ViewModel
{
    public class CMoviesVM
    {
        public IEnumerable<Cinema> Cinemas { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
        [Required, Length(3, 100)]
        public string Name { get; set; } = string.Empty;
        //public string MainImg { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
    }
}
