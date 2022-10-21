using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Dynamic;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class TbTicketsController : Controller
    {
        private readonly Project_DesmodusDBContext _context;

        public static int Actual { get; set; }

        public TbTicketsController(Project_DesmodusDBContext context)
        {
            _context = context;
        }

        // GET: TbTickets
        public async Task<IActionResult> Index()
        {
            var project_DesmodusDBContext = _context.TbTickets.Include(t => t.IdEstadoNavigation).Include(t => t.IdFechaNavigation).Include(t => t.IdPrioridadNavigation).Include(t => t.IdProblemaNavigation).Include(t => t.IdUsuarioNavigation);
            return View(await project_DesmodusDBContext.ToListAsync());
        }

        // GET: TbTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbTickets == null)
            {
                return NotFound();
            }

            var tbTicket = await _context.TbTickets
                .Include(t => t.IdEstadoNavigation)
                .Include(t => t.IdFechaNavigation)
                .Include(t => t.IdPrioridadNavigation)
                .Include(t => t.IdProblemaNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdTicket == id);
            if (tbTicket == null)
            {
                return NotFound();
            }

            return View(tbTicket);
        }
        //GET TbTickets/Volver
        public IActionResult Volver()
        {
            TbFechaTicketsController _con_ft = new TbFechaTicketsController(_context);
             _con_ft.CancelarTicket(Actual);
            return RedirectToAction("Index", "Home");
        }
        // GET: TbTickets/Create
        public IActionResult Create()
        {
            // var a = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
            TbFechaTicketsController _con_ft = new TbFechaTicketsController(_context);
            TbFechaTicket f = new TbFechaTicket();
            f.FechaCreado = DateTime.Now;
            var fechaT = _con_ft.AbrirTicket(f);
            Actual = fechaT.IdFecha;

            ViewData["IdEstado"] = new SelectList(_context.TbEstadoTickets, "IdEstado", "EstadoTicket");
            ViewData["IdFecha"] = fechaT.IdFecha;
            ViewData["IdPrioridad"] = new SelectList(_context.TbPrioridadTickets, "IdPrioridad", "Prioridad");
            ViewData["IdProblema"] = new SelectList(_context.TbCategoria, "IdProblema", "Problema");
            // ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "Nombre");
            var a = User.FindFirst("IdUsuario");
            var usuario = _context.TbUsuarios.Single(i => i.IdUsuario == int.Parse(a.Value));
            ViewData["IdUsuario"] = usuario.IdUsuario;
            //Console.WriteLine(usuario.Nombre);
            return View();
        }

        // POST: TbTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTicket,DespricionP,DescripionDetallada,Localizacion,Adjunto,IdPrioridad,IdUsuario,IdEstado,IdFecha,IdProblema")] TbTicket tbTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstado"] = new SelectList(_context.TbEstadoTickets, "IdEstado", "IdEstado", tbTicket.IdEstado);
            ViewData["IdFecha"] = new SelectList(_context.TbFechaTickets, "IdFecha", "IdFecha", tbTicket.IdFecha);
            ViewData["IdPrioridad"] = new SelectList(_context.TbPrioridadTickets, "IdPrioridad", "IdPrioridad", tbTicket.IdPrioridad);
            ViewData["IdProblema"] = new SelectList(_context.TbCategoria, "IdProblema", "IdProblema", tbTicket.IdProblema);
            ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario", tbTicket.IdUsuario);
            return View(tbTicket);
        }

        // GET: TbTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbTickets == null)
            {
                return NotFound();
            }

            var tbTicket = await _context.TbTickets.FindAsync(id);
            if (tbTicket == null)
            {
                return NotFound();
            }
            ViewData["IdEstado"] = new SelectList(_context.TbEstadoTickets, "IdEstado", "IdEstado", tbTicket.IdEstado);
            ViewData["IdFecha"] = new SelectList(_context.TbFechaTickets, "IdFecha", "IdFecha", tbTicket.IdFecha);
            ViewData["IdPrioridad"] = new SelectList(_context.TbPrioridadTickets, "IdPrioridad", "IdPrioridad", tbTicket.IdPrioridad);
            ViewData["IdProblema"] = new SelectList(_context.TbCategoria, "IdProblema", "IdProblema", tbTicket.IdProblema);
            ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario");
            return View(tbTicket);
        }

        // POST: TbTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTicket,DespricionP,DescripionDetallada,Localizacion,Adjunto,IdPrioridad,IdUsuario,IdEstado,IdFecha,IdProblema")] TbTicket tbTicket)
        {
            if (id != tbTicket.IdTicket)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbTicketExists(tbTicket.IdTicket))
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
            ViewData["IdEstado"] = new SelectList(_context.TbEstadoTickets, "IdEstado", "IdEstado", tbTicket.IdEstado);
            ViewData["IdFecha"] = new SelectList(_context.TbFechaTickets, "IdFecha", "IdFecha", tbTicket.IdFecha);
            ViewData["IdPrioridad"] = new SelectList(_context.TbPrioridadTickets, "IdPrioridad", "IdPrioridad", tbTicket.IdPrioridad);
            ViewData["IdProblema"] = new SelectList(_context.TbCategoria, "IdProblema", "IdProblema", tbTicket.IdProblema);
            ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario", tbTicket.IdUsuario);
            return View(tbTicket);
        }

        // GET: TbTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbTickets == null)
            {
                return NotFound();
            }

            var tbTicket = await _context.TbTickets
                .Include(t => t.IdEstadoNavigation)
                .Include(t => t.IdFechaNavigation)
                .Include(t => t.IdPrioridadNavigation)
                .Include(t => t.IdProblemaNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdTicket == id);
            if (tbTicket == null)
            {
                return NotFound();
            }

            return View(tbTicket);
        }

        // POST: TbTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbTickets == null)
            {
                return Problem("Entity set 'Project_DesmodusDBContext.TbTickets'  is null.");
            }
            var tbTicket = await _context.TbTickets.FindAsync(id);
            if (tbTicket != null)
            {
                _context.TbTickets.Remove(tbTicket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbTicketExists(int id)
        {
            return _context.TbTickets.Any(e => e.IdTicket == id);
        }


    }
}
