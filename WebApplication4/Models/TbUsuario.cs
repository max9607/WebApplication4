using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class TbUsuario
    {
        public TbUsuario()
        {
            TbAccesos = new HashSet<TbAcceso>();
            TbDerivados = new HashSet<TbDerivado>();
            TbTickets = new HashSet<TbTicket>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido1 { get; set; } = null!;
        public string? Apellido2 { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public int? IdEmpresa { get; set; }

        public virtual TbEmpresa? IdEmpresaNavigation { get; set; }
        public virtual ICollection<TbAcceso> TbAccesos { get; set; }
        public virtual ICollection<TbDerivado> TbDerivados { get; set; }
        public virtual ICollection<TbTicket> TbTickets { get; set; }
    }
}
