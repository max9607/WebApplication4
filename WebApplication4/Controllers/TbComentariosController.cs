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
    public class TbComentariosController : Controller
    {
        private readonly ServicesDeskContext _context;

        public TbComentariosController(ServicesDeskContext context)
        {
            _context = context;
        }

        // GET: TbComentarios
        public async Task<IActionResult> Index()
        {
            var servicesDeskContext = _context.TbComentario.Include(t => t.IdTicketNavigation).Include(t => t.IdUsuarioNavigation);
            return View(await servicesDeskContext.ToListAsync());
        }

        // GET: TbComentarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbComentario == null)
            {
                return NotFound();
            }

            var tbComentario = await _context.TbComentario
                .Include(t => t.IdTicketNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdComentario == id);
            if (tbComentario == null)
            {
                return NotFound();
            }

            return View(tbComentario);
        }

        // GET: TbComentarios/Create
        public IActionResult Create()
        {
            var a = User.FindFirst("IdUsuario");
            var usuario = _context.TbUsuario.Single(i => i.IdUsuario == int.Parse(a.Value));
            ViewData["IdTicket"] = new SelectList(_context.TbTicket, "IdTicket", "IdTicket");
            ViewData["IdUsuario"] = usuario.IdUsuario;
            return View();
        }

        // POST: TbComentarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdComentario,Comentario,IdTicket,IdUsuario")] TbComentario tbComentario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbComentario);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                ViewData["IdTicket"] = new SelectList(_context.TbTicket, "IdTicket", "IdTicket", tbComentario.IdTicket);
                ViewData["IdUsuario"] = new SelectList(_context.TbUsuario, "IdUsuario", "IdUsuario", tbComentario.IdUsuario);
                return RedirectToAction("Details", "TbTickets", new { id = tbComentario.IdTicket });
            }
            ViewData["IdTicket"] = new SelectList(_context.TbTicket, "IdTicket", "IdTicket", tbComentario.IdTicket);
            ViewData["IdUsuario"] = new SelectList(_context.TbUsuario, "IdUsuario", "IdUsuario", tbComentario.IdUsuario);
            return RedirectToAction("Details", "TbTickets", new { id = tbComentario.IdTicket });
        }
    
        // GET: TbComentarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbComentario == null)
            {
                return NotFound();
            }

            var tbComentario = await _context.TbComentario.FindAsync(id);
            if (tbComentario == null)
            {
                return NotFound();
            }
            ViewData["IdTicket"] = new SelectList(_context.TbTicket, "IdTicket", "IdTicket", tbComentario.IdTicket);
            ViewData["IdUsuario"] = new SelectList(_context.TbUsuario, "IdUsuario", "IdUsuario", tbComentario.IdUsuario);
            return View(tbComentario);
        }

        // POST: TbComentarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdComentario,Comentario,IdTicket,IdUsuario")] TbComentario tbComentario)
        {
            if (id != tbComentario.IdComentario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbComentario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbComentarioExists(tbComentario.IdComentario))
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
            ViewData["IdTicket"] = new SelectList(_context.TbTicket, "IdTicket", "IdTicket", tbComentario.IdTicket);
            ViewData["IdUsuario"] = new SelectList(_context.TbUsuario, "IdUsuario", "IdUsuario", tbComentario.IdUsuario);
            return View(tbComentario);
        }

        // GET: TbComentarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbComentario == null)
            {
                return NotFound();
            }

            var tbComentario = await _context.TbComentario
                .Include(t => t.IdTicketNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdComentario == id);
            if (tbComentario == null)
            {
                return NotFound();
            }

            return View(tbComentario);
        }

        // POST: TbComentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbComentario == null)
            {
                return Problem("Entity set 'ServicesDeskContext.TbComentario'  is null.");
            }
            var tbComentario = await _context.TbComentario.FindAsync(id);
            if (tbComentario != null)
            {
                _context.TbComentario.Remove(tbComentario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbComentarioExists(int id)
        {
          return (_context.TbComentario?.Any(e => e.IdComentario == id)).GetValueOrDefault();
        }
    }
}
