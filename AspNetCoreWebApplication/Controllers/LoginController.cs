using AspNetCoreWebApplication.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AspNetCoreWebApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly DatabaseContext _contex;

        public LoginController(DatabaseContext contex)
        {
            _contex = contex;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string email, string password)
        {
            try
            {
                var kullanici = await _contex.AppUsers.FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.IsAdmin && u.IsActive);
                if (kullanici == null) TempData["Mesaj"] = "Giriş Başarısız";
                else
                {
                    var haklar = new List<Claim>()
                    {
                    new Claim(ClaimTypes.Email, kullanici.Email)
                    };
                    var kullaniciKimligi = new ClaimsIdentity(haklar, "Login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(kullaniciKimligi);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (kullanici.IsAdmin) return Redirect("/Admin");
                    else return Redirect("/Home");
                }
            }
            catch (Exception)
            {

                TempData["Mesaj"] = "Giriş Başarısız";
            }
            return View();
        }
        [Route("Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
