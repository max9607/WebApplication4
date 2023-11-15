using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class TbTicket
    {
        public TbTicket()
        {
            TbComentario = new HashSet<TbComentario>();
            TbDerivado = new HashSet<TbDerivado>();
        }

        public int IdTicket { get; set; }
        public string? DespricionP { get; set; }
        public string? DescripionDetallada { get; set; }
        public string? Localizacion { get; set; }
        public byte[]? Adjunto { get; set; }
        public int? IdPrioridad { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdEstado { get; set; }
        public int? IdFecha { get; set; }
        public int? IdProblema { get; set; }

        public virtual TbEstadoTicket? IdEstadoNavigation { get; set; }
        public virtual TbFechaTicket? IdFechaNavigation { get; set; }
        public virtual TbPrioridadTicket? IdPrioridadNavigation { get; set; }
        public virtual TbCategoria? IdProblemaNavigation { get; set; }
        public virtual TbUsuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<TbComentario> TbComentario { get; set; }
        public virtual ICollection<TbDerivado> TbDerivado { get; set; }
    }
}
