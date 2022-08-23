using AspNetCoreWebApplication.Data;
using AspNetCoreWebApplication.Entities;
using AspNetCoreWebApplication.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApplication.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _context;
        public ProductsController(DatabaseContext context)
        {
            _context = context;
        }
    
        // GET: ProductsController
        public async Task<ActionResult> IndexAsync()
        {
            return View(await  _context.Products.Include(c=>c.Category).Include(b=>b.Brand).ToListAsync());
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.CategoryId = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            ViewBag.BrandId = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Product product,IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null) product.Image = await FileHelper.FileLoaderAsync(Image);
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            return View(product);
            
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            ViewBag.CategoryId = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");

            ViewBag.BrandId = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name");

            return View( await _context.Products.FindAsync(id));
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Product product,IFormFile? Image, bool resmiSil)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (resmiSil)
                    {
                        FileHelper.FileRemover(product.Image);
                        product.Image = string.Empty;

                    }
                    if (Image != null)product.Image= await FileHelper.FileLoaderAsync(Image);
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();

                  
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştur");

                }

            }
            return View(product);
          
       
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {

            return View( _context.Products.Include(c=>c.Category).Include(b=>b.Brand).FirstOrDefault(p=>p.Id==id));
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(Product product)
        {
            try
            {
               _context.Products.Remove(product);
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştur"); 
            }
            return View(product);
        }
    }
}
