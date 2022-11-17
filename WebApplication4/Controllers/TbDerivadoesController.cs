using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public bool TbDerivadoExists(int id)
        {
          return _context.TbDerivados.Any(e => e.IdDerivado == id);
        }

        //-----------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Derivar(int Vuser, int Vticket)
        {
            TbTicketsController oTicket = new TbTicketsController(_context);
            TbDerivado tbDerivado = new TbDerivado();
            /*
                Para que se tome el valor de un input de la vista este debe etner el atributo {name=""}
                el valor de ese atributo debe ser igual al de los parametros de la funcion en este caso
                'Vuser' y 'Vticket'
             */
            if (ExisteDerivado(Vticket))
            {
                var DerivadoAnterior = _context.TbDerivados.FirstOrDefault(i => i.IdTicket == Vticket);
                _context.TbDerivados.Remove(DerivadoAnterior);
                await _context.SaveChangesAsync();

            }
            tbDerivado.IdTicket = Vticket;
            tbDerivado.IdUsuario = Vuser;


            if (ModelState.IsValid)
            {
                if (oTicket.AbrirTicketAsync(Vticket).Result)
                {
                    _context.Add(tbDerivado);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "TbTickets");
                }

            }
            return RedirectToAction("Index", "TbTickets");
        }

        public bool ExisteDerivado(int id)
        {
            return _context.TbDerivados.Any(e => e.IdTicket == id);
        }

        public string NombreDerivado(int idT)
        {
            /*
             * Primero se obtiene el objeto buscado por el ticket el cual se quiere saber a quien está derivado
             * luego buscamos el objeto usuario con el id del resultado anterior
             * 
             * se devuelve una cadena concatenada con el nombre completo
             */
            var oDerivado = _context.TbDerivados.FirstOrDefault(e => e.IdTicket == idT);
            var oUsuario = _context.TbUsuarios.FirstOrDefault(e => e.IdUsuario == oDerivado.IdUsuario);

            return (oUsuario.Nombre + " " + oUsuario.Apellido1 + " " + oUsuario.Apellido2);
        }
        //Aceptar Ticket, este puede estar derivado o no, se lo llama desde ticket/details
        [HttpPost]
        [Route("{idTicket:int}/{idUsuario:int}")]
        public async Task<RedirectToActionResult> AceptarAsync(int idTicket, int idUsuario)
        {
            TbTicketsController oTicket = new TbTicketsController(_context);

            if (!ExisteDerivado(idTicket))
            {
                //Derivar(idUsuario, idTicket);

                TbDerivado tbDerivado = new TbDerivado();

                tbDerivado.IdTicket = idTicket;
                tbDerivado.IdUsuario = idUsuario;

                //Console.WriteLine(Vuser + "--------------" + Vticket);
                if (ModelState.IsValid)
                {
                    if (oTicket.AbrirTicketAsync(idTicket).Result)
                    {
                        _context.Add(tbDerivado);
                        await _context.SaveChangesAsync();
                        
                    }

                }

                oTicket.AceptarTicketAsync(idTicket);
                return RedirectToAction("Index", "TbTickets");
            }
            else
            {
                var DerivadoAnterior = _context.TbDerivados.Single(i => i.IdTicket == idTicket);

                if (DerivadoAnterior.IdUsuario != idUsuario)
                {
                    _context.TbDerivados.Remove(DerivadoAnterior);
                    await _context.SaveChangesAsync();

                    TbDerivado tbDerivado = new TbDerivado();

                    tbDerivado.IdTicket = idTicket;
                    tbDerivado.IdUsuario = idUsuario;

                    //Console.WriteLine(Vuser + "--------------" + Vticket);
                    if (ModelState.IsValid)
                    {
                        if (oTicket.AbrirTicketAsync(idTicket).Result)
                        {
                            _context.Add(tbDerivado);
                            await _context.SaveChangesAsync();

                        }

                    }
                }
                oTicket.AceptarTicketAsync(idTicket);

                return RedirectToAction("Index", "TbTickets");
            }

        }

        //Eliminar derivados a cerrar
        public async Task EliminarDerivado(int idTicket)
        {
            var tbDerivado = _context.TbDerivados.FirstOrDefault(i => i.IdTicket == idTicket);
            if(tbDerivado != null)
            {
                _context.TbDerivados.Remove(tbDerivado);
                await _context.SaveChangesAsync();
            }

        }

    }
}
