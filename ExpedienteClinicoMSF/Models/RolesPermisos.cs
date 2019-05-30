using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class RolesPermisos
    {
        public int PermisoId { get; set; }
        public int RolId { get; set; }

        public Permisos Permiso { get; set; }
        public Roles Rol { get; set; }
    }
}
