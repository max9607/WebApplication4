using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication4.Models;

namespace WebApplication4.Logica_Login
{
    public class LoginLogica
    {

        private readonly ServicesDeskContext _context;

        public LoginLogica(ServicesDeskContext context)
        {
            _context = context;
        }
        public TbAcceso ValidarAcceso(string _correo, string _clave)
        {
            var DatosAcceso = _context.TbAcceso.Where(t => t.Correo == _correo && t.Clave == _clave && t.Estado==true).FirstOrDefault();
            return DatosAcceso;
        }
        public TbUsuario ValidarUsuario(int id)
        {
            var DatosUsuario = _context.TbUsuario.Where(t => t.IdUsuario == id).FirstOrDefault();
            return DatosUsuario;
        }
        public TbPermiso ValidarPermiso(int idP)
        {
            var DatosPermiso = _context.TbPermiso.Where(t => t.IdPermiso == idP).FirstOrDefault();
            return DatosPermiso;
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
