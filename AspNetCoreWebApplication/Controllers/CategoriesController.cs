using AspNetCoreWebApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApplication.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _context;
        public CategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync(int? id)
        {
            if(id == null)return NotFound();
            if (id != null)
            {
                return View(await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(p => p.Id == id));
            }
            return View();
           
        }
    }
}
