using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        // Constructor Dependency Injection 
        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
                
        }
        // Index for Showing All Product Types
        public IActionResult Index()
        {
            //var data = _db.ProductTypes.ToList();
            return View(_db.ProductTypes.ToList());
        }

        // Create Action Method for GET verb
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create Action Method for Post verb
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if(ModelState.IsValid)
            {
                _db.ProductTypes.Add(productTypes);
                 await _db.SaveChangesAsync();
                TempData["save"] = "Product type has been saved successfully";
                return RedirectToAction("Index");
            }
            return  View(productTypes);
        }

        

        // Edit Action Method for Get verb
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            
           var productType= _db.ProductTypes.Find(id);

            if (productType == null)
                return NotFound();

           return View(productType);
        }

        // Edit Action Method for Post verb
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View(productTypes);
        }

        // Details Action Method for Get verb
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var productType = _db.ProductTypes.Find(id);

            if (productType == null)
                return NotFound();

            return View(productType);
        }

        // Details Action Method for Post verb
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
           
                return RedirectToAction("Index");

        }

        // Delete Action Method for Get verb
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var productType = _db.ProductTypes.Find(id);

            if (productType == null)
                return NotFound();

            return View(productType);
        }

        // Delete Action Method for Post verb
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id,string  ok)
        {
            if(id==null)
                return NotFound();
            
            var productType= _db.ProductTypes.Find(id);
                _db.ProductTypes.Remove(productType);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            

            
        }
    }
}
