using Cinema_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Cinema_Management_System.ViewModel
{
    public class CreateActorViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
        
        public Actor Actor { get; set; }
       
    }
}
