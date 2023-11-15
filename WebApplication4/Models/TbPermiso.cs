using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class TbPermiso
    {
        public TbPermiso()
        {
            TbAcceso = new HashSet<TbAcceso>();
        }

        public int IdPermiso { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<TbAcceso> TbAcceso { get; set; }
    }
}
