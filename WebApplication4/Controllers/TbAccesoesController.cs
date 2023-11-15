using System;
using System.Collections.Generic;
using System.Data;
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
    public class TbAccesoesController : Controller
    {
        private readonly ServicesDeskContext _context;

        public TbAccesoesController(ServicesDeskContext context)
        {
            _context = context;
        }

        // GET: TbAccesoes
        public async Task<IActionResult> Index()
        {
            var project_DesmodusDBContext = _context.TbAcceso.Include(t => t.IdPermisoNavigation).Include(t => t.IdUsuarioNavigation);
            return View(await project_DesmodusDBContext.ToListAsync());
        }
        // VALIDAR USUARIOS
       /* public TbAcceso ValidarUsuarios(string _correo, string _clave) 
        {
            var datosUsuario = _context.TbAccesos.Single(t => t.Correo == _correo && t.Clave == _clave);
            return datosUsuario;
        }*/

        // GET: TbAccesoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbAcceso == null)
            {
                return NotFound();
            }

            var tbAcceso = await _context.TbAcceso
                .Include(t => t.IdPermisoNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdAcceso == id);
            if (tbAcceso == null)
            {
                return NotFound();
            }

            return View(tbAcceso);
        }

        // GET: TbAccesoes/Create
        public IActionResult Create()
        {
            ViewData["IdPermiso"] = new SelectList(_context.TbPermiso, "IdPermiso", "Nombre");
            /*
                creamos una lista donde almacenamos el nombre completo de todos los usuarios para luego
                mostrarlo en el viewbag en la vista 
             */
            var listUsuarios = _context.TbUsuario.Select(s => new
            {
                IdUsuario = s.IdUsuario,
                NombreCompleto = string.Format("{0} {1} {2}", s.Nombre, s.Apellido1, s.Apellido2)//como son tres columnas con el format le decimos que haya un espacio entre cada uno
            });

            ViewData["IdUsuario"] = new SelectList(listUsuarios, "IdUsuario", "NombreCompleto");
            return View();
        }

        // POST: TbAccesoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAcceso,Correo,Clave,IdPermiso,IdUsuario")] TbAcceso tbAcceso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbAcceso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPermiso"] = new SelectList(_context.TbPermiso, "IdPermiso", "Nombre", tbAcceso.IdPermiso);
            var listUsuarios = _context.TbUsuario.Select(s => new
            {
                IdUsuario = s.IdUsuario,
                NombreCompleto = string.Format("{0} {1} {2}", s.Nombre, s.Apellido1, s.Apellido2)//como son tres columnas con el format le decimos que haya un espacio entre cada uno
            });

            ViewData["IdUsuario"] = new SelectList(listUsuarios, "IdUsuario", "NombreCompleto");
            return View();
            return View(tbAcceso);
        }

        // GET: TbAccesoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbAcceso == null)
            {
                return NotFound();
            }

            var tbAcceso = await _context.TbAcceso.FindAsync(id);
            if (tbAcceso == null)
            {
                return NotFound();
            }
            ViewData["IdPermiso"] = new SelectList(_context.TbPermiso, "IdPermiso", "Nombre", tbAcceso.IdPermiso);
            var listUsuarios = _context.TbUsuario.Select(s => new
            {
                IdUsuario = s.IdUsuario,
                NombreCompleto = string.Format("{0} {1} {2}", s.Nombre, s.Apellido1, s.Apellido2)//como son tres columnas con el format le decimos que haya un espacio entre cada uno
            });

            ViewData["IdUsuario"] = new SelectList(listUsuarios, "IdUsuario", "NombreCompleto");
            //ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "Nombre", tbAcceso.IdUsuario);
            return View(tbAcceso);
        }

        // POST: TbAccesoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAcceso,Correo,Clave,IdPermiso,IdUsuario")] TbAcceso tbAcceso)
        {
            if (id != tbAcceso.IdAcceso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbAcceso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbAccesoExists(tbAcceso.IdAcceso))
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

            var listUsuarios = _context.TbUsuario.Select(s => new
            {
                IdUsuario = s.IdUsuario,
                NombreCompleto = string.Format("{0} {1} {2}", s.Nombre, s.Apellido1, s.Apellido2)//como son tres columnas con el format le decimos que haya un espacio entre cada uno
            });

            ViewData["IdUsuario"] = new SelectList(listUsuarios, "IdUsuario", "NombreCompleto");
            ViewData["IdPermiso"] = new SelectList(_context.TbPermiso, "IdPermiso", "Nombre", tbAcceso.IdPermiso);
            //ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "Nombre", tbAcceso.IdUsuario);
            return View(tbAcceso);
        }

        // GET: TbAccesoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbAcceso == null)
            {
                return NotFound();
            }

            var tbAcceso = await _context.TbAcceso
                .Include(t => t.IdPermisoNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdAcceso == id);
            if (tbAcceso == null)
            {
                return NotFound();
            }

            return View(tbAcceso);
        }

        // POST: TbAccesoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbAcceso == null)
            {
                return Problem("Entity set 'Project_DesmodusDBContext.TbAccesos'  is null.");
            }
            var tbAcceso = await _context.TbAcceso.FindAsync(id);
            if (tbAcceso != null)
            {
                _context.TbAcceso.Remove(tbAcceso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbAccesoExists(int id)
        {
          return _context.TbAcceso.Any(e => e.IdAcceso == id);
        }
    }
}
