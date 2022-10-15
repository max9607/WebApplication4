using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using WebApplication4.Controllers;
using WebApplication4.Logica_Login;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Controllers
{
    public class LoginController : Controller
    {
        private readonly Project_DesmodusDBContext _context;

        public LoginController(Project_DesmodusDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(TbAcceso _tbAcceso)
        {
            LoginLogica _da_login = new LoginLogica(_context);
            //TbAccesoesController _da_login = new TbAccesoesController(context: Project_DesmodusDBContext);
            var usuario = _da_login.ValidarUsuarios(_tbAcceso.Correo, _tbAcceso.Clave);

            if(usuario != null)
            {
                return RedirectToAction("Index","TbUsuarios");
            }
            else
            {
                return View();
            }
           
        }
    }
}
