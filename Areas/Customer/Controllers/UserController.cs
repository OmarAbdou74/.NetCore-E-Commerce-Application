using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _db;

        public UserController(UserManager<IdentityUser> userManager,ApplicationDbContext db)
        {
            _userManager = userManager;
            _db=db;
        }

        public IActionResult Index()
        {
            return View(_db.AppUsers.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppUser user)
        {
            if(ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    TempData["save"] = "User created successfully";
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
            }
            
            
            
            return View();
        }
        // Mke the edit an delete actiooooooooooooooooons and lockout


    }
}
