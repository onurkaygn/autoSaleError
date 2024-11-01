using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;
using System.Security.Claims;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IService<Kullanici> _service;
        private readonly IService<Rol> _serviceRol;
        public LoginController(IService<Kullanici> service, IService<Rol> serviceRol)
        {
            _service = service;
            _serviceRol = serviceRol;
        }

        #region Index
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            try
            {
                var account = _service.Get(k => k.Email == email && k.Sifre == password && k.AktifMi == true);

                if (account == null) 
                { 
                TempData["Mesaj"] = "Giriş Başarısız!";

                }
                else
                {
                    var rol = _serviceRol.Get(r => r.Id == account.RolId);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, account.Adi),
                       
                    };
                    if (rol is not null)
                    {
                        claims.Add(new Claim("Role", rol.Adi));
                    }
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/Admin");  
                }
            }
            catch (Exception)
            {
                TempData["Mesaj"] = "Error Occured!";
           
            }
            return View();
        }
        #endregion

        #region Logout

        public async Task<IActionResult> Logout() 
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin/Login");
        }

        #endregion
    }
}
