using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class TbPrioridadTicket
    {
        public TbPrioridadTicket()
        {
            TbTickets = new HashSet<TbTicket>();
        }

        public int IdPrioridad { get; set; }
        public string? Prioridad { get; set; }

        public virtual ICollection<TbTicket> TbTickets { get; set; }
    }
}
