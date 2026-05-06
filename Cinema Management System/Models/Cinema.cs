using System.ComponentModel.DataAnnotations;

namespace Cinema_Management_System.Models
{
    public class Cinema
    {
        public int ID { get; set; }
        [Required ,Length(3,100)]
        public string CinemaName { get; set; } = string.Empty;
        [Required, Length(3, 100)]
        public string Location { get; set; } = string.Empty;

        public string Logo { get; set; } = string.Empty;
    }
}
