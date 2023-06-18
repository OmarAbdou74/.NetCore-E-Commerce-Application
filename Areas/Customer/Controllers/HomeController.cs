using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Utility;

namespace OnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
                _db = db;
        }

        public IActionResult Index()
        {
           
            return View(_db.Products.Include(c => c.SpecialTag).Include(v => v.ProductTypes).ToList());
        }

        // Product Details with Get verb
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            var product=_db.Products.Include(c=>c.SpecialTag).Include(v=>v.ProductTypes).FirstOrDefault(c=>c.Id==id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // Post product details action method 
        [HttpPost]
        [ActionName("Details")]
        //         AddToCart
        public ActionResult ProductDetail(int? id)
        {
            List<Products> products=new List<Products>();  
            if (id == null)
                return NotFound();

            Products product = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (product == null)
                return NotFound();

            products = HttpContext.Session.Get<List<Products>>("products");
            if(products == null)
            {
                products = new List<Products>();
            } 
            products.Add(product);
            
            HttpContext.Session.Set("products", products);

            int count = 0;
            List<Products> om = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
                products = new List<Products>();
            count = products.Count();

            return View(product);

            
        }

        // Remove From Card post
        [HttpPost]
        public ActionResult Remove(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if(products!=null)
            {
                var product=products.FirstOrDefault(c=>c.Id==id);
                products.Remove(product);
                HttpContext.Session.Set("products", products);

            }    
            return RedirectToAction("Index");
        }

        // Remove From Card Get
        [HttpGet]
        [ActionName("Remove")]
        public ActionResult RemoveGet(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                products.Remove(product);
                HttpContext.Session.Set("products", products);

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Cart()
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
                products = new List<Products>();
            return View(products);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}