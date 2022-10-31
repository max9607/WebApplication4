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
    public class TbDerivadoesController : Controller
    {
        private readonly Project_DesmodusDBContext _context;

        public TbDerivadoesController(Project_DesmodusDBContext context)
        {
            _context = context;
        }

        // GET: TbDerivadoes
        public async Task<IActionResult> Index()
        {
            var project_DesmodusDBContext = _context.TbDerivados.Include(t => t.IdTicketNavigation).Include(t => t.IdUsuarioNavigation);
            return View(await project_DesmodusDBContext.ToListAsync());
        }

        // GET: TbDerivadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbDerivados == null)
            {
                return NotFound();
            }

            var tbDerivado = await _context.TbDerivados
                .Include(t => t.IdTicketNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdDerivado == id);
            if (tbDerivado == null)
            {
                return NotFound();
            }

            return View(tbDerivado);
        }

        // GET: TbDerivadoes/Create
        public IActionResult Create()
        {
            ViewData["IdTicket"] = new SelectList(_context.TbTickets, "IdTicket", "IdTicket");
            ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: TbDerivadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDerivado,IdTicket,IdUsuario")] TbDerivado tbDerivado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbDerivado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTicket"] = new SelectList(_context.TbTickets, "IdTicket", "IdTicket", tbDerivado.IdTicket);
            ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario", tbDerivado.IdUsuario);
            return View(tbDerivado);
        }

        // GET: TbDerivadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbDerivados == null)
            {
                return NotFound();
            }

            var tbDerivado = await _context.TbDerivados.FindAsync(id);
            if (tbDerivado == null)
            {
                return NotFound();
            }
            ViewData["IdTicket"] = new SelectList(_context.TbTickets, "IdTicket", "IdTicket", tbDerivado.IdTicket);
            ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario", tbDerivado.IdUsuario);
            return View(tbDerivado);
        }

        // POST: TbDerivadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDerivado,IdTicket,IdUsuario")] TbDerivado tbDerivado)
        {
            if (id != tbDerivado.IdDerivado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbDerivado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbDerivadoExists(tbDerivado.IdDerivado))
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
            ViewData["IdTicket"] = new SelectList(_context.TbTickets, "IdTicket", "IdTicket", tbDerivado.IdTicket);
            ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario", tbDerivado.IdUsuario);
            return View(tbDerivado);
        }

        // GET: TbDerivadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbDerivados == null)
            {
                return NotFound();
            }

            var tbDerivado = await _context.TbDerivados
                .Include(t => t.IdTicketNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdDerivado == id);
            if (tbDerivado == null)
            {
                return NotFound();
            }

            return View(tbDerivado);
        }

        // POST: TbDerivadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbDerivados == null)
            {
                return Problem("Entity set 'Project_DesmodusDBContext.TbDerivados'  is null.");
            }
            var tbDerivado = await _context.TbDerivados.FindAsync(id);
            if (tbDerivado != null)
            {
                _context.TbDerivados.Remove(tbDerivado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbDerivadoExists(int id)
        {
          return _context.TbDerivados.Any(e => e.IdDerivado == id);
        }

        //-----------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Derivar(int Vuser, int Vticket)
        {
            TbDerivado tbDerivado = new TbDerivado();
            /*
                Para que se tome el valor de un input de la vista este debe etner el atributo {name=""}
                el valor de ese atributo debe ser igual al de los parametros de la funcion en este caso
                'Vuser' y 'Vticket'
             */
            tbDerivado.IdTicket = Vticket;
            tbDerivado.IdUsuario = Vuser;

            //Console.WriteLine(Vuser + "--------------" + Vticket);
            if (ModelState.IsValid)
            {
                _context.Add(tbDerivado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "TbTickets");
        }
    }
}
