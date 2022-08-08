using AspNetCoreWebApplication.Data;
using AspNetCoreWebApplication.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public async Task<ActionResult> CreateAsync(Brand brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BrandsController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: BrandsController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BrandsController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
