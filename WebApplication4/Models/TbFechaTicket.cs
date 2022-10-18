﻿using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class TbFechaTicket
    {
        public TbFechaTicket()
        {
            TbTickets = new HashSet<TbTicket>();
        }

        public int IdFecha { get; set; }
        public DateTime? FechaCreado { get; set; }
        public DateTime? FechaCerrado { get; set; }

        public virtual ICollection<TbTicket> TbTickets { get; set; }
    }
}
