using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagController : Controller
    {
        private ApplicationDbContext _db;
        public SpecialTagController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.SpecialTags.ToList());
        }

        // Create  WithGet Verb
        public IActionResult Create()
        {
            return View();
        }

        // Create with Post verb
        [HttpPost]
        public async Task<IActionResult>Create(SpecialTag specialTag)
        {
            if(ModelState.IsValid)
            {
                _db.SpecialTags.Add(specialTag);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View();
        }

        // Edit with Get verb
        [HttpGet]
        public IActionResult Edit(int? id )
        {
            if (id == null)
                return NotFound();
            var specialTag = _db.SpecialTags.FirstOrDefault(x => x.Id == id);
            if (specialTag == null)
                return NotFound();

            return View(specialTag);
        }

        // Edit with Post verb
        [HttpPost]
        public async Task<IActionResult>Edit(SpecialTag specialTag)
        {
            if(ModelState.IsValid)
            {
                _db.SpecialTags.Update(specialTag);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(specialTag);
        }

        // Details with Get verb
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            var specialTage = _db.SpecialTags.Find(id);
            if (specialTage == null)
                return NotFound();

            return View(specialTage);
        }

        // Delete with Get Verb
        public IActionResult Delete (int? id)
        {
            if(id == null)
                return NotFound();
            var specialTag= _db.SpecialTags.Find(id);
            if(specialTag==null)
                return NotFound();
            return View(specialTag);
        }
        // Delete with Post Verb
        [HttpPost]
        public async Task<IActionResult>Delete(SpecialTag specialTag)
        {
            if(ModelState.IsValid)
            {
                _db.SpecialTags.Remove(specialTag);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(specialTag);
        }
    }


}
