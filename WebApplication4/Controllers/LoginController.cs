using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using WebApplication4.Controllers;
using WebApplication4.Logica_Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
namespace WebApplication4.Controllers
{
    public class LoginController : Controller
    {
        private readonly ServicesDeskContext _context;


        public LoginController(ServicesDeskContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(TbAcceso _tbAcceso)
        {
            LoginLogica _da_login = new LoginLogica(_context);
            var Acceso_usuario = _da_login.ValidarAcceso(_tbAcceso.Correo, _tbAcceso.Clave);

            if (Acceso_usuario != null)
            {
                var Datos_usuario = _da_login.ValidarUsuario((int)Acceso_usuario.IdUsuario);
                var Permiso_usuario = _da_login.ValidarPermiso((int)Acceso_usuario.IdPermiso);
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Datos_usuario.Nombre),
            new Claim("Correo", Acceso_usuario.Correo),
            new Claim(ClaimTypes.Role, Permiso_usuario.Nombre),
            new Claim("IdUsuario", Datos_usuario.IdUsuario.ToString())
        };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                if (Permiso_usuario.Nombre == "Usuario")
                {
                    return RedirectToAction("UserTickets", "TbTickets");
                }
                if (Permiso_usuario.Nombre == "Técnico")
                {
                    return RedirectToAction("Index", "TbTickets");
                }
                if (Permiso_usuario.Nombre == "Administrador")
                {
                    return RedirectToAction("Index", "Home");
                }
                return await Salir(); // Esta parte parece un error, deberías revisarla
            }
            else
            {
                // Si el inicio de sesión falla, establece un mensaje de error y devuelve la vista
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
                return View();
            }
        }

        /*public async Task LogoutAction()
        {
            // SomeOtherPage is where we redirect to after signout
            await MyCustomSignOut("Index");
        }
        public async Task MyCustomSignOut(string redirectUri)
        {
            // inject the HttpContextAccessor to get "context"
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var prop = new AuthenticationProperties()
            {
                RedirectUri = redirectUri
            };
            // after signout this will redirect to your provided target
            await HttpContext.SignOutAsync("Login", prop);
        }*/
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
