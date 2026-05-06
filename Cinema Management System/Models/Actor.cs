using System.ComponentModel.DataAnnotations;

namespace Cinema_Management_System.Models
{
    public class Actor
    {
        public int ID { get; set; }
        [Required,MinLength(10)]
        [MaxLength(25)]
        public string Name { get; set; } = string.Empty;
        [Required, MinLength(10)]
        [MaxLength(25)]
        public string Nationality { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;

        public int MovieID { get; set; }
        public Movie? Movies { get; set; }
    }
}
