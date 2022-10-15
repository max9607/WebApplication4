using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication4.Models;

namespace WebApplication4.Logica_Login
{
    public class LoginLogica
    {

        private readonly Project_DesmodusDBContext _context;

        public LoginLogica(Project_DesmodusDBContext context)
        {
            _context = context;
        }
        public TbAcceso ValidarUsuarios(string _correo, string _clave)
        {
         
            var datosUsuario = _context.TbAccesos.Where(t => t.Correo == _correo && t.Clave == _clave).FirstOrDefault();
            return datosUsuario;
        }

        /* public TbAcceso ValidarUsuarios(string _correo, string _clave)
         {
             return _context.TbAccesos.Include(t => t.Correo == _correo && t.Clave == _clave).FirstOrDefaultAsync();
         }
         public async Task<IActionResult> Index(string _correo, string _clave)
         {
             var courses = _context.TbAccesos
                 .Include(t => t.Correo == _correo && t.Clave == _clave)
                 .AsNoTracking();
             return View(await courses.ToListAsync());
         }*/
    }
}
