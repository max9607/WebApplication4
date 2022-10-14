using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class TbAccesoesController : Controller
    {
        private readonly Project_DesmodusDBContext _context;

        public TbAccesoesController(Project_DesmodusDBContext context)
        {
            _context = context;
        }

        // GET: TbAccesoes
        public async Task<IActionResult> Index()
        {
            var project_DesmodusDBContext = _context.TbAccesos.Include(t => t.IdPermisoNavigation).Include(t => t.IdUsuarioNavigation);
            return View(await project_DesmodusDBContext.ToListAsync());
        }

        // GET: TbAccesoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbAccesos == null)
            {
                return NotFound();
            }

            var tbAcceso = await _context.TbAccesos
                .Include(t => t.IdPermisoNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdAcceso == id);
            if (tbAcceso == null)
            {
                return NotFound();
            }

            return View(tbAcceso);
        }

        // GET: TbAccesoes/Create
        public IActionResult Create()
        {
            ViewData["IdPermiso"] = new SelectList(_context.TbPermisos, "IdPermiso", "IdPermiso");
            ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: TbAccesoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAcceso,Correo,Clave,IdPermiso,IdUsuario")] TbAcceso tbAcceso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbAcceso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPermiso"] = new SelectList(_context.TbPermisos, "IdPermiso", "IdPermiso", tbAcceso.IdPermiso);
            ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario", tbAcceso.IdUsuario);
            return View(tbAcceso);
        }

        // GET: TbAccesoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbAccesos == null)
            {
                return NotFound();
            }

            var tbAcceso = await _context.TbAccesos.FindAsync(id);
            if (tbAcceso == null)
            {
                return NotFound();
            }
            ViewData["IdPermiso"] = new SelectList(_context.TbPermisos, "IdPermiso", "IdPermiso", tbAcceso.IdPermiso);
            ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario", tbAcceso.IdUsuario);
            return View(tbAcceso);
        }

        // POST: TbAccesoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAcceso,Correo,Clave,IdPermiso,IdUsuario")] TbAcceso tbAcceso)
        {
            if (id != tbAcceso.IdAcceso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbAcceso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbAccesoExists(tbAcceso.IdAcceso))
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
            ViewData["IdPermiso"] = new SelectList(_context.TbPermisos, "IdPermiso", "IdPermiso", tbAcceso.IdPermiso);
            ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario", tbAcceso.IdUsuario);
            return View(tbAcceso);
        }

        // GET: TbAccesoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbAccesos == null)
            {
                return NotFound();
            }

            var tbAcceso = await _context.TbAccesos
                .Include(t => t.IdPermisoNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdAcceso == id);
            if (tbAcceso == null)
            {
                return NotFound();
            }

            return View(tbAcceso);
        }

        // POST: TbAccesoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbAccesos == null)
            {
                return Problem("Entity set 'Project_DesmodusDBContext.TbAccesos'  is null.");
            }
            var tbAcceso = await _context.TbAccesos.FindAsync(id);
            if (tbAcceso != null)
            {
                _context.TbAccesos.Remove(tbAcceso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbAccesoExists(int id)
        {
          return _context.TbAccesos.Any(e => e.IdAcceso == id);
        }
    }
}
