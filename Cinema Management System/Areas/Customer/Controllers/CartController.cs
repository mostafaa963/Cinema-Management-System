using AspNetCoreGeneratedDocument;
using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Models;
using Cinema_Management_System.Repositories;
using Cinema_Management_System.Repositories.IRepositories;
using Cinema_Management_System.Service.AccountService;
using Cinema_Management_System.Utilities;
using Cinema_Management_System.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;

namespace Cinema_Management_System.Areas.Customer.Controllers
{
    [Area(SD.CUSTOMER_AREA)]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IRepository<Cart> _cart;
        private readonly IRepository<Movie> _movies;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountService _accountService;

        public CartController(IRepository<Cart> cart, IRepository<Movie> movies,
            UserManager<ApplicationUser> userManager,
            IAccountService accountService)
        {
            _movies = movies;
            _cart = cart;
            _userManager = userManager;
            _accountService = accountService;
        }


        [HttpPost]
        public async Task<IActionResult> AddTocCart(AddToCartVM addToCartVM)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
                return Challenge();
            var movies = await _movies.GetOneAsync(e => e.Id == addToCartVM.productId);
            if (movies == null)
                return NotFound();
            var cart = await _cart.GetOneAsync(e => e.MovieId == addToCartVM.productId
                                             && e.ApplicationuserId == user.Id);
            if (cart is null)
            {
                //Cart newCart;
                Cart newCart = new Cart
                {
                    ApplicationuserId = user.Id,
                    ProductPrice = (double)movies.Price,
                    Quantity = addToCartVM.Quantity,
                    MovieId = movies.Id,

                };
                newCart.TotalPrice = newCart.Quantity * newCart.ProductPrice;
                await _cart.CreateAsync(newCart);
            }
            else
            {
                cart.Quantity += addToCartVM.Quantity;
                cart.TotalPrice = cart.Quantity * cart.ProductPrice;
            }
            await _cart.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound();
            var Cartitem = await _cart.GetAsync(e => e.ApplicationuserId == user.Id
                , [e => e.Movie]);

            if (Cartitem is null) return NotFound();

            return View(Cartitem.ToList());
        }

        public async Task<IActionResult> IncrementAsync()
        {
            var user = await _userManager.GetUserAsync(User);
             

            return View();
        }
        public async Task<IActionResult> RemoveFromCart(int cartId)
        {
            var user=await _userManager.GetUserAsync(User);
                if(user == null) return Challenge();

            var cartitem = await _cart.GetOneAsync(e => e.Id == cartId && e.ApplicationuserId == user.Id);
            if (cartitem is null) return NotFound();
            _cart.Delete(cartitem);
            await _cart.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Pay()
        {
            return View();
        }
        public IActionResult Remove()
        {
            return View();
        }


    }
}
