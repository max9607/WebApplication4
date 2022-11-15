using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class TbTicketsCerradoesController : Controller
    {
        private readonly Project_DesmodusDBContext _context;

        public TbTicketsCerradoesController(Project_DesmodusDBContext context)
        {
            _context = context;
        }

        // GET: TbTicketsCerradoes
        public async Task<IActionResult> Index(DateTime? FechaInicio, DateTime? FechaFinal)
        {
            var date = from m in _context.TbTicketsCerrados select m;
            Console.WriteLine("FechaInicio "+FechaInicio.ToString());
            Console.WriteLine("FechaFinal  "+FechaFinal.ToString());
            
            if (FechaInicio != null && FechaFinal != null)
            {
                date = date.Where(e => e.FechaCreado >= FechaInicio && e.FechaCerrado <= FechaFinal);
 
                return View(await date.ToListAsync());
            }
            else
            {
                var data = _context.TbTicketsCerrados;
                return View(await data.ToListAsync());
            }
        }

        // GET: TbTicketsCerradoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbTicketsCerrados == null)
            {
                return NotFound();
            }

            var tbTicketsCerrado = await _context.TbTicketsCerrados
                .FirstOrDefaultAsync(m => m.IdCerrados == id);
            if (tbTicketsCerrado == null)
            {
                return NotFound();
            }

            return View(tbTicketsCerrado);
        }

        // GET: TbTicketsCerradoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbTicketsCerradoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCerrados,IdTicket,Cliente,Comentario,DespricionP,FechaCreado,FechaCerrado,Receptor")] TbTicketsCerrado tbTicketsCerrado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbTicketsCerrado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbTicketsCerrado);
        }

        // GET: TbTicketsCerradoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbTicketsCerrados == null)
            {
                return NotFound();
            }

            var tbTicketsCerrado = await _context.TbTicketsCerrados.FindAsync(id);
            if (tbTicketsCerrado == null)
            {
                return NotFound();
            }
            return View(tbTicketsCerrado);
        }

        // POST: TbTicketsCerradoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCerrados,IdTicket,Cliente,Comentario,DespricionP,FechaCreado,FechaCerrado,Receptor")] TbTicketsCerrado tbTicketsCerrado)
        {
            if (id != tbTicketsCerrado.IdCerrados)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbTicketsCerrado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbTicketsCerradoExists(tbTicketsCerrado.IdCerrados))
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
            return View(tbTicketsCerrado);
        }

        // GET: TbTicketsCerradoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbTicketsCerrados == null)
            {
                return NotFound();
            }

            var tbTicketsCerrado = await _context.TbTicketsCerrados
                .FirstOrDefaultAsync(m => m.IdCerrados == id);
            if (tbTicketsCerrado == null)
            {
                return NotFound();
            }

            return View(tbTicketsCerrado);
        }

        // POST: TbTicketsCerradoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbTicketsCerrados == null)
            {
                return Problem("Entity set 'Project_DesmodusDBContext.TbTicketsCerrados'  is null.");
            }
            var tbTicketsCerrado = await _context.TbTicketsCerrados.FindAsync(id);
            if (tbTicketsCerrado != null)
            {
                _context.TbTicketsCerrados.Remove(tbTicketsCerrado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbTicketsCerradoExists(int id)
        {
          return _context.TbTicketsCerrados.Any(e => e.IdCerrados == id);
        }

        //----------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<bool> CrearCerrado(int IdTicket, string Cliente,string Comentario,string DespricionP,DateTime FechaCreado, DateTime FechaCerrado,string Receptor)
        {
            //[Bind("IdCerrados,IdTicket,Cliente,Comentario,DespricionP,FechaCreado,FechaCerrado,Receptor")] TbTicketsCerrado tbTicketsCerrado

            TbTicketsCerrado tbTicketsCerrado = new TbTicketsCerrado();

            tbTicketsCerrado.IdTicket = IdTicket;
            tbTicketsCerrado.Cliente = Cliente;
            tbTicketsCerrado.Comentario = Comentario;
            tbTicketsCerrado.DespricionP = DespricionP;
            tbTicketsCerrado.FechaCreado = FechaCreado;
            tbTicketsCerrado.FechaCerrado = FechaCerrado;
            tbTicketsCerrado.Receptor = Receptor;

            if (ModelState.IsValid)
            {
                _context.Add(tbTicketsCerrado);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

       /* public async Task<IActionResult> BuscarPorMes()
        {
            DateTime FechaInicio = DateTime.Today.AddDays(-45);
            DateTime FechaFinal = DateTime.Now;

            var data = _context.TbTicketsCerrados.Where()

            return View();
        }*/
    }
}
