using AspNetCoreWebApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _context;
        public ProductsController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync(int? id)
        {
          
            return View(await _context.Products.ToListAsync());
        }
        public async Task<IActionResult> DetailAsync(int? id)
        {

            return View(await _context.Products.Include(b=>b.Brand).Include(c=>c.Category).FirstOrDefaultAsync(p=>p.Id==id));
        }

    }
}
