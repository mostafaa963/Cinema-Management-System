using System.ComponentModel.DataAnnotations;

namespace Cinema_Management_System.Models
{
    public class Category
    {
        public int ID { get; set; }

        [MaxLength(20,ErrorMessage = "field Category Name must be a string or collection type with maximum length of '20'")]
        [Required,MinLength(5 ,ErrorMessage = "field Category Name must be a string or collection type with a minimum length of '5'")]     
        public string HomeName { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
