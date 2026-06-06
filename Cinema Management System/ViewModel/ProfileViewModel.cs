using System.ComponentModel.DataAnnotations;

namespace Cinema_Management_System.ViewModel
{
    public class ProfileViewModel
    {
        public string UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
