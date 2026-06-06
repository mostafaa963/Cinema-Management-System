using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Utilities;
using Cinema_Management_System.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_Management_System.Areas.Identity.Controllers
{
    [Area(SD.IDENTITY_AREA)]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            //var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var profile = new ProfileViewModel
            {
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                UserId = user.Id,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
            //ProfileViewModel profile = user.Adapt<ProfileViewModel>();
            return View(profile);
        }
        public async Task<IActionResult> UpdateProfile(ProfileViewModel profileViewModel)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index), profileViewModel);
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            user.Id = profileViewModel.UserId;
            user.FirstName =profileViewModel.FullName;
            user.PhoneNumber = profileViewModel.PhoneNumber;
            user.Email = profileViewModel.Email;
            user.UserName = profileViewModel.Username;


            var Result = await _userManager.UpdateAsync(user);
            if (!Result.Succeeded)
            {
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);

                }
                return View(nameof(Index), profileViewModel);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
