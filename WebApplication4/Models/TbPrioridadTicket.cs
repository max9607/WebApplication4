using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class TbPrioridadTicket
    {
        public TbPrioridadTicket()
        {
            TbTicket = new HashSet<TbTicket>();
        }

        public int IdPrioridad { get; set; }
        public string? Prioridad { get; set; }
        public int? TiempoRespuesta { get; set; }

        public virtual ICollection<TbTicket> TbTicket { get; set; }
    }
}
