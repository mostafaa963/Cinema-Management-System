using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;


namespace Cinema_Management_System.ViewModel
{
    public class LoginVM
    {
        //public int id { get; set; }

        //[Microsoft.Build.Framework.Required]
        [System.ComponentModel.DataAnnotations.Required]
        public string?  EmailORUserName  { get; set; }=string.Empty;
        //[DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.Password)]
        public string  Password  { get; set; }=string.Empty;
        public bool RememberMe { get; set; } = false;



    }
}
