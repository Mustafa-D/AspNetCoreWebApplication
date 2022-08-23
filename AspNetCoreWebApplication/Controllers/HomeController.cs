using AspNetCoreWebApplication.Data;
using AspNetCoreWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AspNetCoreWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _context;
        

        public HomeController(ILogger<HomeController> logger,DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            HomePageViewModel model = new HomePageViewModel();
            model.Sliders = await _context.Sliders.ToListAsync();
            model.Products = await _context.Products.ToListAsync();
            return View(model);
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