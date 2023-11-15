using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public partial class TbCategoria
    {
        public TbCategoria()
        {
            TbTicket = new HashSet<TbTicket>();
        }
        public int IdProblema { get; set; }
        public string? Problema { get; set; }

        public virtual ICollection<TbTicket> TbTicket { get; set; }
    }
}
