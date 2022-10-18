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
    public class TbFechaTicketsController : Controller
    {
        private readonly Project_DesmodusDBContext _context;

        public TbFechaTicketsController(Project_DesmodusDBContext context)
        {
            _context = context;
        }

        // GET: TbFechaTickets
        public async Task<IActionResult> Index()
        {
              return View(await _context.TbFechaTickets.ToListAsync());
        }

        // GET: TbFechaTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbFechaTickets == null)
            {
                return NotFound();
            }

            var tbFechaTicket = await _context.TbFechaTickets
                .FirstOrDefaultAsync(m => m.IdFecha == id);
            if (tbFechaTicket == null)
            {
                return NotFound();
            }

            return View(tbFechaTicket);
        }

        // GET: TbFechaTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbFechaTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFecha,FechaCreado,FechaCerrado")] TbFechaTicket tbFechaTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbFechaTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbFechaTicket);
        }

        // GET: TbFechaTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbFechaTickets == null)
            {
                return NotFound();
            }

            var tbFechaTicket = await _context.TbFechaTickets.FindAsync(id);
            if (tbFechaTicket == null)
            {
                return NotFound();
            }
            return View(tbFechaTicket);
        }

        // POST: TbFechaTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFecha,FechaCreado,FechaCerrado")] TbFechaTicket tbFechaTicket)
        {
            if (id != tbFechaTicket.IdFecha)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbFechaTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbFechaTicketExists(tbFechaTicket.IdFecha))
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
            return View(tbFechaTicket);
        }

        // GET: TbFechaTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbFechaTickets == null)
            {
                return NotFound();
            }

            var tbFechaTicket = await _context.TbFechaTickets
                .FirstOrDefaultAsync(m => m.IdFecha == id);
            if (tbFechaTicket == null)
            {
                return NotFound();
            }

            return View(tbFechaTicket);
        }

        // POST: TbFechaTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbFechaTickets == null)
            {
                return Problem("Entity set 'Project_DesmodusDBContext.TbFechaTickets'  is null.");
            }
            var tbFechaTicket = await _context.TbFechaTickets.FindAsync(id);
            if (tbFechaTicket != null)
            {
                _context.TbFechaTickets.Remove(tbFechaTicket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbFechaTicketExists(int id)
        {
          return _context.TbFechaTickets.Any(e => e.IdFecha == id);
        }
    }
}
