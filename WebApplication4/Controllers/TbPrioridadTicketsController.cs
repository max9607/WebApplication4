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
    public class TbPrioridadTicketsController : Controller
    {
        private readonly Project_DesmodusDBContext _context;

        public TbPrioridadTicketsController(Project_DesmodusDBContext context)
        {
            _context = context;
        }

        // GET: TbPrioridadTickets
        public async Task<IActionResult> Index()
        {
              return View(await _context.TbPrioridadTickets.ToListAsync());
        }

        // GET: TbPrioridadTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbPrioridadTickets == null)
            {
                return NotFound();
            }

            var tbPrioridadTicket = await _context.TbPrioridadTickets
                .FirstOrDefaultAsync(m => m.IdPrioridad == id);
            if (tbPrioridadTicket == null)
            {
                return NotFound();
            }

            return View(tbPrioridadTicket);
        }

        // GET: TbPrioridadTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbPrioridadTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPrioridad,Prioridad")] TbPrioridadTicket tbPrioridadTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbPrioridadTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbPrioridadTicket);
        }

        // GET: TbPrioridadTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbPrioridadTickets == null)
            {
                return NotFound();
            }

            var tbPrioridadTicket = await _context.TbPrioridadTickets.FindAsync(id);
            if (tbPrioridadTicket == null)
            {
                return NotFound();
            }
            return View(tbPrioridadTicket);
        }

        // POST: TbPrioridadTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPrioridad,Prioridad")] TbPrioridadTicket tbPrioridadTicket)
        {
            if (id != tbPrioridadTicket.IdPrioridad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbPrioridadTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbPrioridadTicketExists(tbPrioridadTicket.IdPrioridad))
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
            return View(tbPrioridadTicket);
        }

        // GET: TbPrioridadTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbPrioridadTickets == null)
            {
                return NotFound();
            }

            var tbPrioridadTicket = await _context.TbPrioridadTickets
                .FirstOrDefaultAsync(m => m.IdPrioridad == id);
            if (tbPrioridadTicket == null)
            {
                return NotFound();
            }

            return View(tbPrioridadTicket);
        }

        // POST: TbPrioridadTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbPrioridadTickets == null)
            {
                return Problem("Entity set 'Project_DesmodusDBContext.TbPrioridadTickets'  is null.");
            }
            var tbPrioridadTicket = await _context.TbPrioridadTickets.FindAsync(id);
            if (tbPrioridadTicket != null)
            {
                _context.TbPrioridadTickets.Remove(tbPrioridadTicket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbPrioridadTicketExists(int id)
        {
          return _context.TbPrioridadTickets.Any(e => e.IdPrioridad == id);
        }
    }
}
