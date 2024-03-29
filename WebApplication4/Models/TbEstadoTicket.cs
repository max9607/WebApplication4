﻿using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class TbEstadoTicket
    {
        public TbEstadoTicket()
        {
            TbTicket = new HashSet<TbTicket>();
        }

        public int IdEstado { get; set; }
        public string? EstadoTicket { get; set; }

        public virtual ICollection<TbTicket> TbTicket { get; set; }
    }
}
