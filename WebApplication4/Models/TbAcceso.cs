using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class TbAcceso
    {
        public int IdAcceso { get; set; }
        public string? Correo { get; set; } = null!;
        public string? Clave { get; set; } = null!;
        public int? IdPermiso { get; set; }
        public int? IdUsuario { get; set; }
        public string? ClaveHash { get; set; }
        public bool? Estado { get; set; }

        public virtual TbPermiso? IdPermisoNavigation { get; set; }
        public virtual TbUsuario? IdUsuarioNavigation { get; set; }
    }
}
