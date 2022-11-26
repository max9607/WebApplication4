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
using System.Data;
using SelectPdf;
using WebApplication4.Models.ViewModels;
using System.Diagnostics.Metrics;
using WebApplication4.Logica;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class TbTicketsController : Controller
    {
        private readonly Project_DesmodusDBContext _context;

        public static int Actual { get; set; }
        public static byte[] EditImagen { get; set; }

        private static TbTicket EditTicket { get; set; }
        public TbTicketsController(Project_DesmodusDBContext context)
        {
            _context = context;
        }

        // GET: TbTickets
        [Authorize(Roles = "Administrador,Técnico")]
        public async Task<IActionResult> Index()
        {


            if (User.IsInRole("Técnico"))
            {
                /*
                 En 'iduser' se toma el id del usuario que accedió EJ: 1003 
                'lista_derivados obtiene todos los tickets del usuario de como una lista de entidades EJ: {1003,2002}{1003,2006}'
                'id_drivados' obtiene todos los id's de los tickets de 'lista_derivados' {2002,2006}
                'lista_tickets' obtiene las entidades las cuales sus id's estén dentro de la lista 'id_derivados'
                 */
                var iduser = User.FindFirst("IdUsuario"); //1003
                var lista_derivados = _context.TbDerivados.Include(t => t.IdTicketNavigation).Include(t => t.IdUsuarioNavigation).Where(i => i.IdUsuario == int.Parse(iduser.Value)).ToList(); //{1003,2002}{1003,2006}
                var id_derivados = lista_derivados.Select(x => x.IdTicket).ToList();//2002,2006,2008
                var lista_tickets = _context.TbTickets.Include(t => t.IdEstadoNavigation)
                                                      .Include(t => t.IdFechaNavigation)
                                                      .Include(t => t.IdPrioridadNavigation)
                                                      .Include(t => t.IdProblemaNavigation)
                                                      .Include(t => t.IdUsuarioNavigation).Where(i => id_derivados.Contains(i.IdTicket));
                return View(await lista_tickets.ToListAsync());
            }
            else
            {
                var project_DesmodusDBContext = _context.TbTickets.Include(t => t.IdEstadoNavigation).Include(t => t.IdFechaNavigation).Include(t => t.IdPrioridadNavigation).Include(t => t.IdProblemaNavigation).Include(t => t.IdUsuarioNavigation);
                return View(await project_DesmodusDBContext.ToListAsync());
            }
        }
        //GET: Tickets del usuario logueado

        public async Task<IActionResult> UserTickets()
        {
            var a = User.FindFirst("IdUsuario");    //obtenemos el claim IdUsuario y lo almacenamos en una variable a
            var userid = int.Parse(a.Value);        //guardamos el valor de a convertido a entero
            if (_context.TbTickets == null)
            {
                return NotFound();
            }
            var tbTicket = await _context.TbTickets
                .Include(t => t.IdEstadoNavigation)
                .Include(t => t.IdFechaNavigation)
                .Include(t => t.IdPrioridadNavigation)
                .Include(t => t.IdProblemaNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .Where(m => m.IdUsuario == userid).ToListAsync();
            if (tbTicket == null)
            {
                return NotFound();
            }

            return View(tbTicket);
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
            var listTecnicos = _context.TbAccesos.Where(i => i.IdPermiso == 3 || i.IdPermisoNavigation.Nombre == "Técnico").Select(s => new
            {
                IdUsuario = s.IdUsuario,
                NombreCompleto = string.Format("{0} {1} {2}", s.IdUsuarioNavigation.Nombre, s.IdUsuarioNavigation.Apellido1, s.IdUsuarioNavigation.Apellido2)
            });
            ViewData["Derivar"] = new SelectList(listTecnicos, "IdUsuario", "NombreCompleto");
            return View(tbTicket);
        }
        //GET TbTickets/Volver
        public IActionResult Volver()
        {
            TbFechaTicketsController _con_ft = new TbFechaTicketsController(_context);
            _con_ft.CancelarTicket(Actual);
            var roles = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            if (roles.FirstOrDefault() == "Administrador")
                return RedirectToAction(nameof(Index));
            if (roles.FirstOrDefault() == "Usuario")
                return RedirectToAction(nameof(UserTickets));
            else
                return RedirectToAction("Index", "Login");
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

            var estado = _context.TbEstadoTickets.Single(f => f.EstadoTicket == "nuevo");
            //ViewData["IdEstado"] = new SelectList(_context.TbEstadoTickets, "IdEstado", "EstadoTicket");
            ViewData["IdFecha"] = fechaT.IdFecha;
            ViewData["IdPrioridad"] = new SelectList(_context.TbPrioridadTickets, "IdPrioridad", "Prioridad");
            ViewData["IdProblema"] = new SelectList(_context.TbCategoria, "IdProblema", "Problema");
            // ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "Nombre");
            var a = User.FindFirst("IdUsuario");
            var usuario = _context.TbUsuarios.Single(i => i.IdUsuario == int.Parse(a.Value));
            ViewData["IdEstado"] = estado.IdEstado;
            ViewData["IdUsuario"] = usuario.IdUsuario;
            //Console.WriteLine(usuario.Nombre);
            return View();
        }

        // POST: TbTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTicket,DespricionP,DescripionDetallada,Localizacion,IdPrioridad,IdUsuario,IdEstado,IdFecha,IdProblema")] TbTicket tbTicket)
        {

            if (ModelState.IsValid)
            {

                foreach (var file in Request.Form.Files)
                {
                    //Leer la imagen
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    tbTicket.Adjunto = ms.ToArray();

                    ms.Close();
                    ms.Dispose();


                }
                /*mailLogica omail = new mailLogica();

                var admins = _context.TbAccesos.Where(i => i.IdPermisoNavigation.Nombre == "Administrador").ToList();
                var idadmins = admins.Select(i => i.IdUsuario).ToList();
                var correosadmins = _context.TbUsuarios.Where(i => idadmins.Contains(i.IdUsuario));
                var listacorreos = correosadmins.Select(i => i.Correo).ToList();
                
                var correoUsuario = _context.TbUsuarios.First(i => i.IdUsuario == tbTicket.IdUsuario);
                

                await omail.SendEmailAsync(listacorreos,tbTicket.DespricionP,correoUsuario.Correo);*/

                _context.Add(tbTicket);
                await _context.SaveChangesAsync();
                if (User.IsInRole("Administrador"))
                {
                    return RedirectToAction(nameof(Index));
                }
                if (User.IsInRole("Usuario"))
                {
                    return RedirectToAction(nameof(UserTickets));
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

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

            if (tbTicket.Adjunto != null)
            {
                EditImagen = tbTicket.Adjunto;
            }


            if (tbTicket == null)
            {
                return NotFound();
            }
            //ViewData["IdEstado"] = new SelectList(_context.TbEstadoTickets, "IdEstado", "EstadoTicket", tbTicket.IdEstado);
            //ViewData["IdFecha"] = new SelectList(_context.TbFechaTickets, "IdFecha", "FechaCreado", tbTicket.IdFecha);

            ViewData["Estado"] = _context.TbEstadoTickets.Single(i => i.IdEstado == tbTicket.IdEstado).EstadoTicket; //hice esto mas arriba pero aqui en una sola linea xdxd me da paja cambiarlo en el create
            ViewData["Fecha"] = _context.TbFechaTickets.Single(i => i.IdFecha == tbTicket.IdFecha).FechaCreado;
            ViewData["IdPrioridad"] = new SelectList(_context.TbPrioridadTickets, "IdPrioridad", "Prioridad", tbTicket.IdPrioridad);
            ViewData["IdProblema"] = new SelectList(_context.TbCategoria, "IdProblema", "Problema", tbTicket.IdProblema);
            //ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "Nombre");
            EditTicket = tbTicket; //Guardamos de forma temporal el ticket a editar
            return View(tbTicket);
        }

        // POST: TbTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTicket,DespricionP,DescripionDetallada,IdPrioridad,IdProblema")] TbTicket tbTicket)
        {
            if (id != tbTicket.IdTicket)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (tbTicket.Adjunto != null)
                {
                    //Console.WriteLine("Tiene la IMAGEEEEN");
                }
                try
                {
                    if (Request.Form.Files.Count() > 0)
                    {
                        foreach (var file in Request.Form.Files)
                        {
                            //Leer la imagen

                            MemoryStream ms = new MemoryStream();
                            file.CopyTo(ms);
                            tbTicket.Adjunto = ms.ToArray();

                            ms.Close();
                            ms.Dispose();


                        }
                    }
                    else
                    {
                        tbTicket.Adjunto = EditImagen;
                    }
                    //Aqui va la informacion que no se podrá editar, la tomamos de EditTicket debe estar des-bindeado de los parametros de la funcion
                    tbTicket.Localizacion = EditTicket.Localizacion;
                    tbTicket.IdUsuario = EditTicket.IdUsuario;
                    tbTicket.IdEstado = EditTicket.IdEstado;
                    tbTicket.IdFecha = EditTicket.IdFecha;
                    //----------------------------------------------------------------------
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
                if (User.IsInRole("Administrador") || User.IsInRole("Técnico"))
                {
                    return RedirectToAction(nameof(Index));
                }
                if (User.IsInRole("Usuario"))
                {
                    return RedirectToAction(nameof(UserTickets));
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            //ViewData["IdEstado"] = new SelectList(_context.TbEstadoTickets, "IdEstado", "IdEstado", tbTicket.IdEstado);
            //ViewData["IdFecha"] = new SelectList(_context.TbFechaTickets, "IdFecha", "IdFecha", tbTicket.IdFecha);
            ViewData["IdPrioridad"] = new SelectList(_context.TbPrioridadTickets, "IdPrioridad", "IdPrioridad", tbTicket.IdPrioridad);
            ViewData["IdProblema"] = new SelectList(_context.TbCategoria, "IdProblema", "IdProblema", tbTicket.IdProblema);
            //ViewData["IdUsuario"] = new SelectList(_context.TbUsuarios, "IdUsuario", "IdUsuario", tbTicket.IdUsuario);
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


        //----------------------------------------------------------------------------------

        //GET: Obtener imaganes
        public ActionResult Obtener(int id)
        {
            var img = _context.TbTickets.Where(i => i.IdTicket == id).FirstOrDefault();
            return File(img.Adjunto, "image/jpeg");
        }
        //GET: Obtiene los tickets cerrados

        public async Task<IActionResult> BuscarTicket(string buscar)
        {
            var user = from m in _context.TbTickets.Include(t => t.IdEstadoNavigation).Include(t => t.IdFechaNavigation).Include(t => t.IdPrioridadNavigation).Include(t => t.IdProblemaNavigation).Include(t => t.IdUsuarioNavigation) select m;
            if (!string.IsNullOrEmpty(buscar))
            {
                user = user.Where(s => s.IdTicket.ToString().Contains(buscar) || s.DespricionP!.Contains(buscar) || s.DescripionDetallada!.Contains(buscar) || s.IdUsuarioNavigation!.Nombre.Contains(buscar)
                || (s.IdTicket + " " + s.DespricionP + " " + s.DescripionDetallada + " " + s.IdUsuarioNavigation.Nombre).ToLower().Contains(buscar));
            }
            var project_DesmodusDBContext = _context.TbTickets.Include(t => t.IdEstadoNavigation).Include(t => t.IdFechaNavigation).Include(t => t.IdPrioridadNavigation).Include(t => t.IdProblemaNavigation).Include(t => t.IdUsuarioNavigation);
            return View(await user.ToListAsync());
        }

        [HttpGet]
        public IActionResult Test()
        {
            var name = HttpContext.Request.Query["term"].ToString();
            var name2 = _context.TbTickets.Where(c => c.IdTicket.ToString().Contains(name) || c.DespricionP!.Contains(name) || c.IdUsuarioNavigation!.Nombre.Contains(name)).Select(c => c.IdTicket + " " + c.DespricionP + " " + c.DescripionDetallada + " " + c.IdUsuarioNavigation.Nombre).ToList();

            return Ok(name2);
        }
        //Funcion que se activa al derivar el ticker cambia el estado a abierto
        public async Task<bool> AbrirTicketAsync(int IdTicket)
        {
            var tbTicket = _context.TbTickets.Single(i => i.IdTicket == IdTicket);
            if (tbTicket == null)
            {
                return false;
            }
            else
            {
                /*
                    - 1 Nuevo
                    - 2 Abierto
                    - 3 Pendiente
                    - 4 Cerrado
                 */
                tbTicket.IdEstado = 2;//pasa a estar abierto

                _context.Update(tbTicket);
                await _context.SaveChangesAsync();//guardamos

                return true;
            }

        }
        //Funcion que cambia el estado del ticket a pendiente
        public async Task<bool> AceptarTicketAsync(int IdTicket)
        {
            var tbTicket = _context.TbTickets.Single(i => i.IdTicket == IdTicket);
            if (tbTicket == null)
            {
                return false;
            }
            else
            {
                /*
                    - 1 Nuevo
                    - 2 Abierto
                    - 3 Pendiente
                    - 4 Cerrado
                 */
                tbTicket.IdEstado = 3;//pasa a estar abierto

                _context.Update(tbTicket);
                await _context.SaveChangesAsync();//guardamos

                return true;
            }

        }

        [Authorize(Roles = "Administrador,Técnico")]
        public async Task<IActionResult> GeneradorPdf(string html)
        {
            html = html.Replace("StrTag", "<").Replace("EndTag", ">");

            HtmlToPdf oHtmlToPdf = new HtmlToPdf();
            PdfDocument oPdfDocument = oHtmlToPdf.ConvertHtmlString(html);
            byte[] pdf = oPdfDocument.Save();
            oPdfDocument.Close();

            return File(
                pdf,
                "application/pdf",
                "CerradosReporte.pdf"
                );

        }

        [Authorize(Roles = "Administrador,Técnico")]
        public async Task<IActionResult> Cerrados()
        {
            var project_DesmodusDBContext = _context.TbTickets.Include(t => t.IdEstadoNavigation).Include(t => t.IdFechaNavigation).Include(t => t.IdPrioridadNavigation).Include(t => t.IdProblemaNavigation).Include(t => t.IdUsuarioNavigation);
            return View(await project_DesmodusDBContext.ToListAsync());

        }

        public IActionResult ResumenTickets()
        {
            
            //este coso devuelve una lista de los ticktes con un estado asignado.
            List<VMTickets> list = (from TbTicket in _context.TbTickets where TbTicket.IdEstado==1 ||TbTicket.IdEstado==2 || TbTicket.IdEstado == 3 || TbTicket.IdEstado == 4 
                                   group TbTicket by TbTicket.IdEstadoNavigation.EstadoTicket  into grupo
                                   select new VMTickets
                                   {
                                       DespricionP = grupo.Key,
                                       IdEstado = grupo.Count(),
                                   }).ToList();


            return StatusCode(StatusCodes.Status200OK, list);

        }

        public IActionResult ResumeTicketsTecnicos() {
            //este coso devuelve una lista de los ticktes asiganos a cada tecnico.
            List<VMAsignados> list = (from TbDerivado in _context.TbDerivados 
                                      group TbDerivado by TbDerivado.IdUsuarioNavigation.Nombre into grupo    
                                      select new VMAsignados
                                      {
                                          NombreUsusario = grupo.Key,
                                          IdTicket = grupo.Count(),
                                      }).ToList();


            return StatusCode(StatusCodes.Status200OK, list);

        }
        public IActionResult contadorDetecnico()
        {
            List<VMContadorUsuarios> list = (from TbAcceso in _context.TbAccesos where TbAcceso.IdPermisoNavigation.Nombre == "Técnico"
                                             group TbAcceso by TbAcceso.IdUsuario into grupo
                                             select new VMContadorUsuarios
                                             {
                                                 IdPermiso = grupo.Key,
                                                 IdUsuario = grupo.Count(),

                                             }).ToList();

            return StatusCode(StatusCodes.Status200OK, list);

        }
        public IActionResult contadorDeUser()
        {
            List<VMContadorUsuarios> list = (from TbAcceso in _context.TbAccesos
                                             where TbAcceso.IdPermisoNavigation.Nombre == "Usuario"
                                             group TbAcceso by TbAcceso.IdUsuario into grupo
                                             select new VMContadorUsuarios
                                             {
                                                 IdPermiso = grupo.Key,
                                                 IdUsuario = grupo.Count(),

                                             }).ToList();

            return StatusCode(StatusCodes.Status200OK, list);

        }

        public IActionResult contadorDeAdmin()
        {
            List<VMContadorUsuarios> list = (from TbAcceso in _context.TbAccesos where TbAcceso.IdPermisoNavigation.Nombre == "Administrador"
                                             group TbAcceso by TbAcceso.IdUsuario into grupo
                                             select new VMContadorUsuarios
                                             {
                                                 IdPermiso = grupo.Key,
                                                 IdUsuario = grupo.Count(),

                                             }).ToList();

            return StatusCode(StatusCodes.Status200OK, list);

        }

       


        public async Task<RedirectToActionResult> CerrarTicket(int IdTicket, string Comentario)
        {
            var tbTicket = _context.TbTickets.Single(i => i.IdTicket == IdTicket);
            var tbCliente = _context.TbUsuarios.Single(i => i.IdUsuario == tbTicket.IdUsuario);
            var tbFecha = _context.TbFechaTickets.Single(i => i.IdFecha == tbTicket.IdFecha);
            var tbDerivado = _context.TbDerivados.Single(i => i.IdTicket == tbTicket.IdTicket);
            var tbReceptor = _context.TbUsuarios.Single(i => i.IdUsuario == tbDerivado.IdUsuario);

            TbTicketsCerradoesController oTCerrados = new TbTicketsCerradoesController(_context);
            TbFechaTicketsController oFecha = new TbFechaTicketsController(_context);
            TbDerivadoesController oDerivados = new TbDerivadoesController(_context);

            tbFecha.FechaCerrado = DateTime.Now;
            tbFecha =  await oFecha.CerrarTicket(tbFecha);

            string Cliente = tbCliente.Nombre + " " + tbCliente.Apellido1 + " " + tbCliente.Apellido2;
            string Receptor = tbReceptor.Nombre + " " + tbReceptor.Apellido1 + " " + tbReceptor.Apellido2;

            await oTCerrados.CrearCerrado(IdTicket, Cliente, Comentario, tbTicket.DespricionP, (DateTime)tbFecha.FechaCreado, (DateTime)tbFecha.FechaCerrado, Receptor);

            tbTicket.IdEstado = 4;//pasa a estar cerrado
            _context.Update(tbTicket);
            await _context.SaveChangesAsync();
            
            //await oDerivados.EliminarDerivado(IdTicket);

            return RedirectToAction("Index", "TbTickets");
        }

        public void probarcorreo()
        {
            mailLogica oMail = new mailLogica();
            oMail.emailpruebas();
        }
    }
}
