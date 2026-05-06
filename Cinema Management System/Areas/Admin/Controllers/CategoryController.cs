using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Models;
using Cinema_Management_System.Repositories;
using Cinema_Management_System.Repositories.IRepositories;
using Cinema_Management_System.Utilities;
using Cinema_Management_System.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Management_System.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _category;
        public CategoryController(IRepository<Category> category)
        {
            //_category = new Repository<Category>();
            _category = category;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            //var category = _db.Categories.AsEnumerable();
            var category =await _category.GetAsync();
            int totalPages = (int)Math.Ceiling(category.Count() / 5.0);
            category = category.Skip((page - 1) * 5).Take(5);
            return View(new CategoryWithRelatedVM
            {
                categories = category,
                CurrentPage = page,
                totalPages = totalPages
            });
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category,CancellationToken cancellation)
        {
            if (category == null)
                return NotFound();

            //if (!ModelState.IsValid)
            //    return View(category);

            //?Response.Cookies.Append("successfully", "Create a successfully");
            if (category is not null)
            {
               await _category.CreateAsync(category,cancellationToken: cancellation);
               await _category.CommitAsync(cancellationToken: cancellation);
            }
            TempData["info_notification"] = "Add Category a successfully";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id,CancellationToken cancellation)
        {
            var category = (await _category.GetAsync(cancellationToken: cancellation)).FirstOrDefault(e => e.ID == id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Category category, CancellationToken cancellation)
        {
            ViewBag.Category = category;

            //var _categoryUpdate = _db.Categories.FirstOrDefault(e => e.ID == category.ID);
            if (category == null)
                return NotFound();

            //if (!ModelState.IsValid)
            //    return View(category);

            if (category is not null)
            {
               _category.Update(category);
                await _category.CommitAsync(cancellationToken: cancellation);
            }
            TempData["info_notification"] = "Update Category a successfully";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteAsync(int id,CancellationToken cancellation)
        {
            var category = (await _category.GetAsync(cancellationToken: cancellation)).SingleOrDefault(e => e.ID == id);

            if (category is null) return NotFound();

            _category.Delete(category);
            TempData["info_notification"] = "Delete Category a successfully";
           await _category.CommitAsync(cancellationToken: cancellation);



            return RedirectToAction(nameof(Index));
        }
    }
}
