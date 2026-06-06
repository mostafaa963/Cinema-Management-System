using Cinema_Management_System.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_Management_System.Areas.Customer.Controllers
{
    [Area(SD.CUSTOMER_AREA)]
    //[Authorize (Roles = $"{RoleNames.CUSTOMER}")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
