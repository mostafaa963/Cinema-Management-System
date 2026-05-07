using System.ComponentModel.DataAnnotations;

namespace Cinema_Management_System.ViewModel
{
    public class RegisterVM
    {
        public int Id { get; set; }
        [Required]
        public string  FirstName { get; set; }=string.Empty;
        [Required]
        public string  LastName { get; set; }= string.Empty;
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }= string.Empty;
        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string  ConfirmPassword { get; set; }= string.Empty;
        public string?  Address { get; set; }
       }
}
