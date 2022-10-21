using System.Security.Claims;
using WebApplication4.Models;
namespace WebApplication4.Logica_Ticket
{
    public class TcketLogica
    {
        private readonly Project_DesmodusDBContext _context;

        public TcketLogica(Project_DesmodusDBContext context)
        {
            _context = context;
        }

       
    }
}
