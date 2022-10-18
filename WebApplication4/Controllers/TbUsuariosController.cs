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
    [Authorize(Roles = "Administrador")]
    public class TbUsuariosController : Controller
    {
        private readonly Project_DesmodusDBContext _context;

        public TbUsuariosController(Project_DesmodusDBContext context)
        {
            _context = context;
        }

        // GET: TbUsuarios
        public async Task<IActionResult> Index()
        {
            var project_DesmodusDBContext = _context.TbUsuarios.Include(t => t.IdEmpresaNavigation);
            return View(await project_DesmodusDBContext.ToListAsync());
        }
    
       
        // GET: TbUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbUsuarios == null)
            {
                return NotFound();
            }

            var tbUsuario = await _context.TbUsuarios
                .Include(t => t.IdEmpresaNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (tbUsuario == null)
            {
                return NotFound();
            }

            return View(tbUsuario);
        }

        // GET: TbUsuarios/Create
        public IActionResult Create()
        {
            ViewData["IdEmpresa"] = new SelectList(_context.TbEmpresas, "IdEmpresa", "Nombre");
            return View();
        }

        // POST: TbUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["IdEmpresa"] = new SelectList(_context.TbEmpresas, "IdEmpresa", "Nombre", tbUsuario.IdEmpresa);
            return View(tbUsuario);
        }

        // GET: TbUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbUsuarios == null)
            {
                return NotFound();
            }

            var tbUsuario = await _context.TbUsuarios.FindAsync(id);
            if (tbUsuario == null)
            {
                return NotFound();
            }
            ViewData["IdEmpresa"] = new SelectList(_context.TbEmpresas, "IdEmpresa", "Nombre", tbUsuario.IdEmpresa);
            return View(tbUsuario);
        }

        // POST: TbUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombre,Apellido1,Apellido2,Telefono,Correo,IdEmpresa")] TbUsuario tbUsuario)
        {
            if (id != tbUsuario.IdUsuario)
            {
                return NotFound();
            }

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
            ViewData["IdEmpresa"] = new SelectList(_context.TbEmpresas, "IdEmpresa", "Nombre", tbUsuario.IdEmpresa);
            return View(tbUsuario);
        }

        // GET: TbUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbUsuarios == null)
            {
                return NotFound();
            }

            var tbUsuario = await _context.TbUsuarios
                .Include(t => t.IdEmpresaNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (tbUsuario == null)
            {
                return NotFound();
            }

            return View(tbUsuario);
        }

        // POST: TbUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbUsuarios == null)
            {
                return Problem("Entity set 'Project_DesmodusDBContext.TbUsuarios'  is null.");
            }
            var tbUsuario = await _context.TbUsuarios.FindAsync(id);
            if (tbUsuario != null)
            {
                _context.TbUsuarios.Remove(tbUsuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbUsuarioExists(int id)
        {
          return _context.TbUsuarios.Any(e => e.IdUsuario == id);
        }
    }
}
