using AspNetCoreWebApplication.Data;
using AspNetCoreWebApplication.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppUsersController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public AppUsersController(DatabaseContext context)
        {
            _databaseContext = context;
        }

        // GET: AppUsersController
        public async Task<ActionResult> IndexAsync()
        {
            return View(await _databaseContext.AppUsers.ToListAsync());
        }

        // GET: AppUsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   await _databaseContext.AppUsers.AddAsync(appUser);
                   await _databaseContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştur");
                }

            }
            return View(appUser);
          
        }

        // GET: AppUsersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
           var kullanici= await _databaseContext.AppUsers.FindAsync(id);

            return View(kullanici);
        }

        // POST: AppUsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                        _databaseContext.Entry(appUser).State = EntityState.Modified;
                        await _databaseContext.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    
                   
                    
                }
                catch
                {
                    ModelState.AddModelError("", "Hata oluştur");
                }
            }
            return View(appUser);
           
        }

        // GET: AppUsersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var kullanici = await _databaseContext.AppUsers.FindAsync(id);

            return View(kullanici);
        }

        // POST: AppUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(AppUser appUser)
        {
           
                try
                {
                    _databaseContext.AppUsers.Remove(appUser);
                    //_databaseContext.Entry(appUser).State = EntityState.Deleted;
                    await _databaseContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    
                }
            
            return View(appUser);

        }
    }
}
