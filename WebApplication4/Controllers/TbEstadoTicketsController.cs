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
    public class TbEstadoTicketsController : Controller
    {
        private readonly Project_DesmodusDBContext _context;

        public TbEstadoTicketsController(Project_DesmodusDBContext context)
        {
            _context = context;
        }

        // GET: TbEstadoTickets
        public async Task<IActionResult> Index()
        {
              return View(await _context.TbEstadoTickets.ToListAsync());
        }

        // GET: TbEstadoTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbEstadoTickets == null)
            {
                return NotFound();
            }

            var tbEstadoTicket = await _context.TbEstadoTickets
                .FirstOrDefaultAsync(m => m.IdEstado == id);
            if (tbEstadoTicket == null)
            {
                return NotFound();
            }

            return View(tbEstadoTicket);
        }

        // GET: TbEstadoTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbEstadoTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstado,EstadoTicket")] TbEstadoTicket tbEstadoTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbEstadoTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbEstadoTicket);
        }

        // GET: TbEstadoTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbEstadoTickets == null)
            {
                return NotFound();
            }

            var tbEstadoTicket = await _context.TbEstadoTickets.FindAsync(id);
            if (tbEstadoTicket == null)
            {
                return NotFound();
            }
            return View(tbEstadoTicket);
        }

        // POST: TbEstadoTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstado,EstadoTicket")] TbEstadoTicket tbEstadoTicket)
        {
            if (id != tbEstadoTicket.IdEstado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbEstadoTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbEstadoTicketExists(tbEstadoTicket.IdEstado))
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
            return View(tbEstadoTicket);
        }

        // GET: TbEstadoTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbEstadoTickets == null)
            {
                return NotFound();
            }

            var tbEstadoTicket = await _context.TbEstadoTickets
                .FirstOrDefaultAsync(m => m.IdEstado == id);
            if (tbEstadoTicket == null)
            {
                return NotFound();
            }

            return View(tbEstadoTicket);
        }

        // POST: TbEstadoTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbEstadoTickets == null)
            {
                return Problem("Entity set 'Project_DesmodusDBContext.TbEstadoTickets'  is null.");
            }
            var tbEstadoTicket = await _context.TbEstadoTickets.FindAsync(id);
            if (tbEstadoTicket != null)
            {
                _context.TbEstadoTickets.Remove(tbEstadoTicket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbEstadoTicketExists(int id)
        {
          return _context.TbEstadoTickets.Any(e => e.IdEstado == id);
        }
    }
}
