using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Permisos
    {
        public Permisos()
        {
            RolesPermisos = new HashSet<RolesPermisos>();
        }

        public int PermisoId { get; set; }
        public string Permiso { get; set; }
        public bool EstadoPermiso { get; set; }

        public ICollection<RolesPermisos> RolesPermisos { get; set; }
    }
}
