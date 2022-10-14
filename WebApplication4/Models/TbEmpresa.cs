using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class TbEmpresa
    {
        public TbEmpresa()
        {
            TbUsuarios = new HashSet<TbUsuario>();
        }

        public int IdEmpresa { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Nit { get; set; }
        public string? Telefono { get; set; }

        public virtual ICollection<TbUsuario> TbUsuarios { get; set; }
    }
}
