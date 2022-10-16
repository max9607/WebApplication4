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
    public class TbPermisoesController : Controller
    {
        private readonly Project_DesmodusDBContext _context;

        public TbPermisoesController(Project_DesmodusDBContext context)
        {
            _context = context;
        }

        // GET: TbPermisoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.TbPermisos.ToListAsync());
        }

        // GET: TbPermisoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbPermisos == null)
            {
                return NotFound();
            }

            var tbPermiso = await _context.TbPermisos
                .FirstOrDefaultAsync(m => m.IdPermiso == id);
            if (tbPermiso == null)
            {
                return NotFound();
            }

            return View(tbPermiso);
        }

        // GET: TbPermisoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbPermisoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPermiso,Nombre")] TbPermiso tbPermiso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbPermiso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbPermiso);
        }

        // GET: TbPermisoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbPermisos == null)
            {
                return NotFound();
            }

            var tbPermiso = await _context.TbPermisos.FindAsync(id);
            if (tbPermiso == null)
            {
                return NotFound();
            }
            return View(tbPermiso);
        }

        // POST: TbPermisoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPermiso,Nombre")] TbPermiso tbPermiso)
        {
            if (id != tbPermiso.IdPermiso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbPermiso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbPermisoExists(tbPermiso.IdPermiso))
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
            return View(tbPermiso);
        }

        // GET: TbPermisoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbPermisos == null)
            {
                return NotFound();
            }

            var tbPermiso = await _context.TbPermisos
                .FirstOrDefaultAsync(m => m.IdPermiso == id);
            if (tbPermiso == null)
            {
                return NotFound();
            }

            return View(tbPermiso);
        }

        // POST: TbPermisoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbPermisos == null)
            {
                return Problem("Entity set 'Project_DesmodusDBContext.TbPermisos'  is null.");
            }
            var tbPermiso = await _context.TbPermisos.FindAsync(id);
            if (tbPermiso != null)
            {
                _context.TbPermisos.Remove(tbPermiso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbPermisoExists(int id)
        {
          return _context.TbPermisos.Any(e => e.IdPermiso == id);
        }
    }
}
