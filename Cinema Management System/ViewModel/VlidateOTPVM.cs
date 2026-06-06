using System.ComponentModel.DataAnnotations;

namespace Cinema_Management_System.ViewModel
{
    public class VlidateOTPVM
    {
        public int Id { get; set; }
        [Required]
        public string OTP { get; set; } = string.Empty;
    }
}
