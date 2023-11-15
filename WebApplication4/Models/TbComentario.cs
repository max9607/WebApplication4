using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public partial class TbComentario
    {
        [Key]
        public int IdComentario { get; set; }
        public string? Comentario { get; set; }
        public int? IdTicket { get; set; }
        public int? IdUsuario { get; set; }

        public virtual TbTicket? IdTicketNavigation { get; set; }
        public virtual TbUsuario? IdUsuarioNavigation { get; set; }
    }
}
