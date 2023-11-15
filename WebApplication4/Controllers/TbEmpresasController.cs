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
    public class TbEmpresasController : Controller
    {
        private readonly ServicesDeskContext _context;

        public TbEmpresasController(ServicesDeskContext context)
        {
            _context = context;
        }

        // GET: TbEmpresas
        public async Task<IActionResult> Index()
        {
              return View(await _context.TbEmpresa.ToListAsync());
        }

        // GET: TbEmpresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbEmpresa == null)
            {
                return NotFound();
            }

            var tbEmpresa = await _context.TbEmpresa
                .FirstOrDefaultAsync(m => m.IdEmpresa == id);
            if (tbEmpresa == null)
            {
                return NotFound();
            }

            return View(tbEmpresa);
        }

        // GET: TbEmpresas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbEmpresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpresa,Nombre,Nit,Telefono")] TbEmpresa tbEmpresa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbEmpresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbEmpresa);
        }

        // GET: TbEmpresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbEmpresa == null)
            {
                return NotFound();
            }

            var tbEmpresa = await _context.TbEmpresa.FindAsync(id);
            if (tbEmpresa == null)
            {
                return NotFound();
            }
            return View(tbEmpresa);
        }

        // POST: TbEmpresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpresa,Nombre,Nit,Telefono")] TbEmpresa tbEmpresa)
        {
            if (id != tbEmpresa.IdEmpresa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbEmpresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbEmpresaExists(tbEmpresa.IdEmpresa))
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
            return View(tbEmpresa);
        }

        // GET: TbEmpresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbEmpresa == null)
            {
                return NotFound();
            }

            var tbEmpresa = await _context.TbEmpresa
                .FirstOrDefaultAsync(m => m.IdEmpresa == id);
            if (tbEmpresa == null)
            {
                return NotFound();
            }

            return View(tbEmpresa);
        }

        // POST: TbEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbEmpresa == null)
            {
                return Problem("Entity set 'Project_DesmodusDBContext.TbEmpresas'  is null.");
            }
            var tbEmpresa = await _context.TbEmpresa.FindAsync(id);
            if (tbEmpresa != null)
            {
                _context.TbEmpresa.Remove(tbEmpresa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbEmpresaExists(int id)
        {
          return _context.TbEmpresa.Any(e => e.IdEmpresa == id);
        }
    }
}
