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
    public class TbFechaTicketsController : Controller
    {
        private readonly ServicesDeskContext _context;

        public TbFechaTicketsController(ServicesDeskContext context)
        {
            _context = context;
        }

        // GET: TbFechaTickets
        public async Task<IActionResult> Index()
        {
              return View(await _context.TbFechaTicket.ToListAsync());
        }

        // GET: TbFechaTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbFechaTicket == null)
            {
                return NotFound();
            }

            var tbFechaTicket = await _context.TbFechaTicket
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




        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool CancelarTicket(int id)
        {
            var a = _context.TbFechaTicket.Where(m => m.IdFecha == id).FirstOrDefault();
            if (a != null)
            {
                Console.WriteLine(id);
                _context.Remove(a);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return true;
            }
        }
        // GET: TbFechaTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbFechaTicket == null)
            {
                return NotFound();
            }

            var tbFechaTicket = await _context.TbFechaTicket.FindAsync(id);
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
            if (id == null || _context.TbFechaTicket == null)
            {
                return NotFound();
            }

            var tbFechaTicket = await _context.TbFechaTicket
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
            if (_context.TbFechaTicket == null)
            {
                return Problem("Entity set 'Project_DesmodusDBContext.TbFechaTickets'  is null.");
            }
            var tbFechaTicket = await _context.TbFechaTicket.FindAsync(id);
            if (tbFechaTicket != null)
            {
                _context.TbFechaTicket.Remove(tbFechaTicket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbFechaTicketExists(int id)
        {
          return _context.TbFechaTicket.Any(e => e.IdFecha == id);
        }

        //-----------------------------------------------------------------
        //Funcion que establece una fecha de abierto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public TbFechaTicket AbrirTicket(TbFechaTicket oticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oticket);
                _context.SaveChanges();
                return oticket;
            }
            return null;
        }

        //Funcion que actualiza el registro de abierto del ticket y establece la fecha cerrada
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<TbFechaTicket> CerrarTicket(TbFechaTicket oticket)
        {
            if (ModelState.IsValid)
            {
                _context.Update(oticket);
                await _context.SaveChangesAsync();
                return oticket;
            }
            return null;
        }

    }
}
