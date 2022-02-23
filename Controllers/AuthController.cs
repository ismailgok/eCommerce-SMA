using Ders30.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ders30.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (model.Email == "cem@gmail.com" && model.Password == "123")
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("KullaniciAdi")))
                {
                    HttpContext.Session.SetString("KullaniciAdi", "Cem");
                    HttpContext.Session.SetInt32("Rol", 1); // 1: Admin

                    // Cookie
                    if (model.RememberMe)
                    {
                        var options = new CookieOptions()
                        {
                            Expires = DateTime.Now.AddMinutes(30),
                            Domain = "localhost",
                            Secure = true,
                            HttpOnly = true
                        };

                        Response.Cookies.Append("KullaniciAdi", Helper.Base64Encode("Cem"), options);
                        Response.Cookies.Append("Rol", Helper.Base64Encode("1"), options);
                    }
                }

                return Redirect("/");
            }
            else
            {
                ViewBag.Message = "Kullanıcı girişi hatalı";
                return View(model);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("KullaniciAdi");
            HttpContext.Session.Remove("Rol");

            Response.Cookies.Delete("KullaniciAdi");
            Response.Cookies.Delete("Rol");

            return Redirect("/");
        }
    }
}
