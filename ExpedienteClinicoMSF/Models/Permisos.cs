using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
