using AspNetCoreWebApplication.Data;
using AspNetCoreWebApplication.Entities;
using AspNetCoreWebApplication.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApplication.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class BrandsController : Controller
    {
       
        private readonly DatabaseContext _contex;
        public BrandsController(DatabaseContext context)
        {
            _contex = context;
        }
        // GET: BrandsController1
        public async Task<ActionResult> IndexAsync()
        {
            return View(await _contex.Brands.ToListAsync()); 
        }

        // GET: BrandsController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrandsController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandsController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Brand brand,IFormFile logo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    brand.Logo= await  FileHelper.FileLoaderAsync(logo);
                    _contex.Entry(brand).State = EntityState.Added;
                    //_contex.Brands.Add(brand);
                   await _contex.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    
                }
            }
            return View(brand);

        }

        // GET: BrandsController1/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var marka= await _contex.Brands.FindAsync(id);
            if (marka == null) return NotFound();
            return View(marka);
        }

        // POST: BrandsController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Brand brand,IFormFile? logo,bool resmiSil)
        {
            if (ModelState.IsValid)
            {


                try
                {
                    if (resmiSil)
                    {
                        FileHelper.FileRemover(brand.Logo);
                        brand.Logo = string.Empty;
                    }
                    if (logo != null) brand.Logo = await FileHelper.FileLoaderAsync(logo);
                    //_contex.Entry(brand).State = EntityState.Modified;
                     _contex.Brands.Update(brand);
                    await _contex.SaveChangesAsync();                  

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                   ModelState.AddModelError("", "Hata oluşutur");
                }
            }
            return View(brand);
            
        }

        // GET: BrandsController1/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            
            return View(await _contex.Brands.FindAsync(id));
        }

        // POST: BrandsController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(Brand brand)
        {
            try
            {
                 _contex.Brands.Remove(brand);
                await _contex.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu");
            }
            return View(brand);
        }
    }
}
