using AspNetCoreWebApplication.Data;
using AspNetCoreWebApplication.Entities;
using AspNetCoreWebApplication.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApplication.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _contex;
        public CategoriesController(DatabaseContext contex)
        {
            _contex = contex;
        }

        // GET: CategoriesController
        public ActionResult Index()
        {
            return View(_contex.Categories.ToList());
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Category category, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string filename = await FileHelper.FileLoaderAsync(Image);
                    category.Image = filename;
                    await _contex.Categories.AddAsync(category);
                    _contex.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "hata oluştur");
                }

            }
            return View(category);

        }

        // GET: CategoriesController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            //var kayit = _contex.Categories.FirstOrDefault(c => c.Id == id);
            //if (kayit == null) return NotFound();
            return View(await _contex.Categories.FindAsync(id));
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Category category, IFormFile Image, bool resmiSil)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (resmiSil)
                    {
                        FileHelper.FileRemover(category.Image);
                        category.Image = string.Empty;
                    }

                    if (Image != null) category.Image = await FileHelper.FileLoaderAsync(Image);
                    _contex.Categories.Update(category);
                    await _contex.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));



                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştur");
                }
            }
            return View(category);
        }

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {

            return View(await _contex.Categories.FindAsync(id));
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(Category category)
        {
            try
            {
                _contex.Categories.Remove(category);
                await _contex.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata oluştu");
            }
            return View(category);
        }
    }
}
