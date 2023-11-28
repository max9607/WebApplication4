using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class TbUsuariosController : Controller
    {
        private readonly ServicesDeskContext _context;

        public TbUsuariosController(ServicesDeskContext context)
        {
            _context = context;
        }

        // GET: TbUsuarios
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index(string buscar)
        {
            
           var user = from m in _context.TbUsuario.Include(t=>t.IdEmpresaNavigation) select m;
            if (!string.IsNullOrEmpty(buscar))
            {
                user = user.Where(s => s.Nombre!.Contains(buscar)|| s.Apellido1!.Contains(buscar) || s.Apellido2!.Contains(buscar) || s.Correo!.Contains(buscar) || s.Telefono!.Contains(buscar)||s.IdEmpresaNavigation!.Nombre.Contains(buscar)
                ||(s.Nombre+" "+s.Apellido1+" "+s.Apellido2+" "+s.Correo).ToLower().Contains(buscar));
            }
            var project_DesmodusDBContext = _context.TbUsuario.Include(t => t.IdEmpresaNavigation);
            return View(await user.Where(x=>x.Estado==true).ToListAsync());
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index2(string buscar)
        {

            var user = from m in _context.TbUsuario.Include(t => t.IdEmpresaNavigation) select m;
            if (!string.IsNullOrEmpty(buscar))
            {
                user = user.Where(s => s.Nombre!.Contains(buscar) || s.Apellido1!.Contains(buscar) || s.Apellido2!.Contains(buscar) || s.Correo!.Contains(buscar) || s.Telefono!.Contains(buscar) || s.IdEmpresaNavigation!.Nombre.Contains(buscar)
                || (s.Nombre + " " + s.Apellido1 + " " + s.Apellido2 + " " + s.Correo).ToLower().Contains(buscar));
            }
            var project_DesmodusDBContext = _context.TbUsuario.Include(t => t.IdEmpresaNavigation);
            return View(await user.Where(x => x.Estado == false).ToListAsync());
        }

        // GET: TbUsuarios/Details/5
        [Authorize(Roles = "Administrador, Usuario, Técnico")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbUsuario == null)
            {
                return NotFound();
            }

            var tbUsuario = await _context.TbUsuario
                .Include(t => t.IdEmpresaNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (tbUsuario == null)
            {
                return NotFound();
            }

            return View(tbUsuario);
        }

        // GET: TbUsuarios/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            ViewData["IdEmpresa"] = new SelectList(_context.TbEmpresa.Where(x=>x.Estado==true), "IdEmpresa", "Nombre");
            return View();
        }

        // POST: TbUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Nombre,Apellido1,Apellido2,Telefono,Correo,IdEmpresa")] TbUsuario tbUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpresa"] = new SelectList(_context.TbEmpresa, "IdEmpresa", "Nombre", tbUsuario.IdEmpresa);
            return View(tbUsuario);
        }

        // GET: TbUsuarios/Edit/5
        [Authorize(Roles = "Administrador, Usuario, Técnico")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbUsuario == null)
            {
                return NotFound();
            }

            var tbUsuario = await _context.TbUsuario.FindAsync(id);
            if (tbUsuario == null)
            {
                return NotFound();
            }
            ViewData["IdEmpresa"] = new SelectList(_context.TbEmpresa.Where(x=>x.Estado==true), "IdEmpresa", "Nombre", tbUsuario.IdEmpresa);
            return View(tbUsuario);
        }

        // POST: TbUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador, Usuario, Técnico")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombre,Apellido1,Apellido2,Telefono,Correo,IdEmpresa,Estado")] TbUsuario tbUsuario, bool Estado)
        {
            if (id != tbUsuario.IdUsuario)
            {
                return NotFound();
            }
            // Asigna el valor del checkbox al modelo
            tbUsuario.Estado = Estado;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbUsuarioExists(tbUsuario.IdUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpresa"] = new SelectList(_context.TbEmpresa, "IdEmpresa", "Nombre", tbUsuario.IdEmpresa);
            return View(tbUsuario);
        }

        // GET: TbUsuarios/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbUsuario == null)
            {
                return NotFound();
            }

            var tbUsuario = await _context.TbUsuario
                .Include(t => t.IdEmpresaNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (tbUsuario == null)
            {
                return NotFound();
            }

            return View(tbUsuario);
        }

        // POST: TbUsuarios/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbUsuario == null)
            {
                return Problem("Entity set 'Project_DesmodusDBContext.TbUsuarios'  is null.");
            }

            var tbUsuario = await _context.TbUsuario.FindAsync(id);
            

            if (tbUsuario != null)
            {

                var tbAccesos = _context.TbAcceso.Where(i => i.IdUsuario == id);
                foreach (var item in tbAccesos)
                {
                    _context.TbAcceso.Remove(item);
                }
                
                _context.TbUsuario.Remove(tbUsuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Administrador")]
        private bool TbUsuarioExists(int id)
        {
          return _context.TbUsuario.Any(e => e.IdUsuario == id);
        }
        [HttpGet]
        public  IActionResult Test()
        {
            var name = HttpContext.Request.Query["term"].ToString();
            var name2 = _context.TbUsuario.Where(c => c.Nombre.Contains(name)|| c.Apellido1.Contains(name) || c.Apellido2.Contains(name) || c.Correo.Contains(name)).Select(c=>c.Nombre+" "+c.Apellido1+" "+c.Apellido2+" "+c.Correo).ToList();
           
            return Ok(name2);
        }
    }
}
