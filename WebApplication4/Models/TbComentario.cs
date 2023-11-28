using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class TbComentario
    {
        public int IdComentario { get; set; }
        public string? Comentario { get; set; }
        public int? IdTicket { get; set; }
        public int? IdUsuario { get; set; }

        public virtual TbTicket? IdTicketNavigation { get; set; }
        public virtual TbUsuario? IdUsuarioNavigation { get; set; }
    }
}
