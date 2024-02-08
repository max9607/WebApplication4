using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class TbUsuario
    {
        public TbUsuario()
        {
            TbAcceso = new HashSet<TbAcceso>();
            TbComentario = new HashSet<TbComentario>();
            TbDerivado = new HashSet<TbDerivado>();
            TbTicket = new HashSet<TbTicket>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido1 { get; set; } = null!;
        public string? Apellido2 { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public int? IdEmpresa { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public virtual TbEmpresa? IdEmpresaNavigation { get; set; }
        public virtual ICollection<TbAcceso> TbAcceso { get; set; }
        public virtual ICollection<TbComentario> TbComentario { get; set; }
        public virtual ICollection<TbDerivado> TbDerivado { get; set; }
        public virtual ICollection<TbTicket> TbTicket { get; set; }
    }
}
