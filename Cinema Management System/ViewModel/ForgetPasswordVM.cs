using System.ComponentModel.DataAnnotations;

namespace Cinema_Management_System.ViewModel
{
    public class ForgetPasswordVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "EmailORUserName")]
        public string EmailORUserName { get; set; } = string.Empty;
    }
}
