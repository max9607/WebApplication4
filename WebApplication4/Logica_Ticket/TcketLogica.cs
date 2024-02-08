using System.Security.Claims;
using WebApplication4.Models;
namespace WebApplication4.Logica_Ticket
{
    public class TcketLogica
    {
        private readonly ServicesDeskContext _context;

        public TcketLogica(ServicesDeskContext context)
        {
            _context = context;
        }
        public TbTicket ValidarCerrado(int idE)
        {
            var DatosC = _context.TbTicket.Where(t => t.IdEstado == 4).FirstOrDefault();
            return DatosC;
        }



    }
}
