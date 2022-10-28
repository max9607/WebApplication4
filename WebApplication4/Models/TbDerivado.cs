using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class TbDerivado
    {
        public int IdDerivado { get; set; }
        public int? IdTicket { get; set; }
        public int? IdUsuario { get; set; }

        public virtual TbTicket? IdTicketNavigation { get; set; }
        public virtual TbUsuario? IdUsuarioNavigation { get; set; }
    }
}
