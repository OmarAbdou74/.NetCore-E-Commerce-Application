using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Models.ViewModels;

namespace OnlineShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private  ApplicationDbContext _db;
		private IHostEnvironment _he;
		public ProductController(ApplicationDbContext db, IHostEnvironment he)
		{
			_db = db;
			_he = he;
		}
		public IActionResult Index()
		{
			return View(_db.Products.Include(c=>c.ProductTypes).Include(f=>f.SpecialTag).ToList());
		}

		// Index with Post Verb
		[HttpPost]
        public IActionResult Index(decimal? lowAmount,decimal? largeAmount)
		{
			var products = _db.Products.Include(c=>c.ProductTypes).Include(x=>x.SpecialTag).Where(z=>z.Price>=lowAmount&&z.Price<=largeAmount).ToList();
			if (lowAmount == null || largeAmount == null)
				products = _db.Products.Include(c => c.ProductTypes).Include(x => x.SpecialTag).ToList();

            return View(products);
		}

        //Create with Get Verb
        [HttpGet]
		public IActionResult Create()
		{
			ViewData["ProductTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
			ViewData["SpecialTagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
			return View();
		}
		//Create with Post Verb
		[HttpPost]
		public async Task<IActionResult> Create(ProductViewModel product)
		{
			Products products = new Products();
			products.Name = product.Name;
			products.Price = product.Price;
			products.ProductColor = product.ProductColor;
			products.IsAvailable = product.IsAvailable;
			products.ProductTypeId = product.ProductTypeId;
			products.SpecialTagId = product.SpecialTagId;

			if (ModelState.IsValid)
			{
				if (product.Image != null)
				{
					//var name = Path.Combine(_he.ContentRootPath + "/Images",Path.GetFileName(product.Image.FileName));
					//await product.Image.CopyToAsync(new FileStream(name,FileMode.Create));
					//products.Image = "Images/" + product.Image.FileName;

					var searchProduct=_db.Products.FirstOrDefault(x => x.Name == product.Name);
					if(searchProduct != null)
					{
						ViewBag.message = "this product is already exist";
                        ViewData["ProductTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                        ViewData["SpecialTagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
                        return View(product);
					}

					 
                    string fileName = Path.GetFileName(product.Image.FileName);
					string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", fileName);
					var stream = new FileStream(uploadfilepath, FileMode.Create);
					product.Image.CopyToAsync(stream);
					products.Image = "Images/" + product.Image.FileName;
				}
				if (product.Image == null)
				
					products.Image = "no-image-found-360x260.png";
				
					_db.Products.Add(products);
					await _db.SaveChangesAsync();
					return RedirectToAction("Index");
				}
				return View(products);

			}
		
		
        //Edit with Get Verb
		//time 1:50:00
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewData["ProductTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["SpecialTagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
			if (id == null)
				return NotFound();
			var product = _db.Products.Include(c=>c.SpecialTag).Include(f=>f.ProductTypes).FirstOrDefault(x => x.Id == id);
			if(product==null)
				return NotFound();

            return View(product);
        }
        //Edit with Post Verb
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel product)
        {
            Products products = new Products();
			products.Id=product.Id;
            products.Name = product.Name;
            products.Price = product.Price;
            products.ProductColor = product.ProductColor;
            products.IsAvailable = product.IsAvailable;
            products.ProductTypeId = product.ProductTypeId;
            products.SpecialTagId = product.SpecialTagId;

            if (ModelState.IsValid)
            {
                if (product.Image != null)
                {
                    //var name = Path.Combine(_he.ContentRootPath + "/Images",Path.GetFileName(product.Image.FileName));
                    //await product.Image.CopyToAsync(new FileStream(name,FileMode.Create));
                    //products.Image = "Images/" + product.Image.FileName;

                    string fileName = product.Image.FileName;
                    fileName = Path.GetFileName(fileName);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", fileName);
                    var stream = new FileStream(uploadfilepath, FileMode.Create);
                    product.Image.CopyToAsync(stream);
                    products.Image = "Images/" + product.Image.FileName;
                }
                if (product.Image == null)

                    products.Image = "no-image-found-360x260.png";

                _db.Products.Update(products);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(products);

        }

        //Details with GET Verb
        [HttpGet]
		public IActionResult Details(int? id)
		{
			if (id == null)
				return NotFound();

			var products=_db.Products.Include(c => c.ProductTypes).Include(f => f.SpecialTag).FirstOrDefault(x => x.Id == id);

			if(products==null)
				{ return NotFound(); }

			return View(products);
		}
        //Delete with GET Verb
		public IActionResult Delete(int? id)
		{
            ViewData["ProductTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["SpecialTagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
            if (id == null)
				return NotFound();
			var product = _db.Products.Include(x => x.ProductTypes).Include(c => c.SpecialTag).FirstOrDefault(z=>z.Id==id);
			if (product == null)
				return NotFound();
			
			return View(product);
		}

		//Delete with Post Verb
		[HttpPost]
		[ActionName("Delete")]
		
        public async Task<IActionResult> DeleteConfirm(int?id)
		{
			if (id == null)
				return NotFound();
			var product = _db.Products.Find(id);
			if (product == null)
				return NotFound();
			_db.Products.Remove(product);
			 await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}





    }
}
