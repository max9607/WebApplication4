using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class TbCategorium
    {
        public TbCategorium()
        {
            TbTickets = new HashSet<TbTicket>();
        }

        public int IdProblema { get; set; }
        public string? Problema { get; set; }

        public virtual ICollection<TbTicket> TbTickets { get; set; }
    }
}
