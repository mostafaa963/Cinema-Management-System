using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations;

namespace Cinema_Management_System.ViewModel
{
    public class LoginVM
    {
        public int id { get; set; }
        
        [Microsoft.Build.Framework.Required]
        public string?  EmailORUserName  { get; set; }=string.Empty;
        [DataType(DataType.Password)]
        [Microsoft.Build.Framework.Required]
        public string  Password  { get; set; }=string.Empty;
        public bool  RememberMe  { get; set; }



    }
}
