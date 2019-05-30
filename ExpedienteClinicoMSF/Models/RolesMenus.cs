using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class RolesMenus
    {
        public int RolId { get; set; }
        public int MenuId { get; set; }

        public Menus Menu { get; set; }
        public Roles Rol { get; set; }
    }
}
