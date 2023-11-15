using System;
using System.Collections.Generic;
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
    public class TbCategoriumsController : Controller
    {
        private readonly ServicesDeskContext _context;

        public TbCategoriumsController(ServicesDeskContext context)
        {
            _context = context;
        }

        // GET: TbCategoriums
        public async Task<IActionResult> Index()
        {
              return View(await _context.TbCategoria.ToListAsync());
        }

        // GET: TbCategoriums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbCategoria == null)
            {
                return NotFound();
            }

            var tbCategorium = await _context.TbCategoria
                .FirstOrDefaultAsync(m => m.IdProblema == id);
            if (tbCategorium == null)
            {
                return NotFound();
            }

            return View(tbCategorium);
        }

        // GET: TbCategoriums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbCategoriums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProblema,Problema")] TbCategoria tbCategorium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbCategorium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbCategorium);
        }

        // GET: TbCategoriums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbCategoria == null)
            {
                return NotFound();
            }

            var tbCategorium = await _context.TbCategoria.FindAsync(id);
            if (tbCategorium == null)
            {
                return NotFound();
            }
            return View(tbCategorium);
        }

        // POST: TbCategoriums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProblema,Problema")] TbCategoria tbCategorium)
        {
            if (id != tbCategorium.IdProblema)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbCategorium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbCategoriumExists(tbCategorium.IdProblema))
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
            return View(tbCategorium);
        }

        // GET: TbCategoriums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbCategoria == null)
            {
                return NotFound();
            }

            var tbCategorium = await _context.TbCategoria
                .FirstOrDefaultAsync(m => m.IdProblema == id);
            if (tbCategorium == null)
            {
                return NotFound();
            }

            return View(tbCategorium);
        }

        // POST: TbCategoriums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbCategoria == null)
            {
                return Problem("Entity set 'Project_DesmodusDBContext.TbCategoria'  is null.");
            }
            var tbCategorium = await _context.TbCategoria.FindAsync(id);
            if (tbCategorium != null)
            {
                _context.TbCategoria.Remove(tbCategorium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbCategoriumExists(int id)
        {
          return _context.TbCategoria.Any(e => e.IdProblema == id);
        }
    }
}
