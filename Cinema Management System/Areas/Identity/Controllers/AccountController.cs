using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Repositories.IRepositories;
using Cinema_Management_System.Utilities;
using Cinema_Management_System.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_Management_System.Areas.Identity.Controllers
{
    [Area(SD.IDENTITY_AREA)]
    public class AccountController : Controller
    {
       
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager,IEmailSender emailSender)
        {
            _userManager=userManager;
            _emailSender=emailSender;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid)
                return View(registerVM);
            var user = new ApplicationUser() {
                FirstName= registerVM.FirstName,
                LastName= registerVM.LastName,
                Email=registerVM.Email,
                UserName=registerVM.UserName,
                Address=registerVM.Address,
            };
            ViewBag.check = false;
           var  identityResult= await  _userManager.CreateAsync(user, registerVM.Password);
            if (!identityResult.Succeeded)
            {
                ViewBag.check = true;
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError(item.Code,item.Description);   
                }
               
                return View(registerVM);
            }
           var token=  await _userManager.GenerateEmailConfirmationTokenAsync(user);
           var link= Url.Action(nameof(Confirm), "Account", new
            {
                area = SD.IDENTITY_AREA,
                token,
                user.Id
            },Request.Scheme);

            await _emailSender.SendEmailAsync(registerVM.Email, "confirm Email",
                $"<h1>click<a href ='{link}'>Here</a></h1>");
            TempData["success_notification"] = "your Account is Add Check your Email";
            return RedirectToAction(nameof(Confirm));
        }
        public async Task<IActionResult> Confirm(string token, string  id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                TempData["error_notification"] = string.Join(",", result.Errors.Select(e=>e.Description));
            }

            TempData["success_notification"] = "confirm is successfully";
            return RedirectToAction(nameof(Login));
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            return View();
        }
    }
}
