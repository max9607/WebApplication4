using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class TbTicketsCerrados
    {
        public int IdCerrados { get; set; }
        public int? IdTicket { get; set; }
        public string? Cliente { get; set; }
        public string? Comentario { get; set; }
        public string? DespricionP { get; set; }
        public DateTime? FechaCreado { get; set; }
        public DateTime? FechaCerrado { get; set; }
        public string? Receptor { get; set; }
    }
}
