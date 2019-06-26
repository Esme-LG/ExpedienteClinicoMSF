using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Permisos
    {
        public Permisos()
        {
            RolesPermisos = new HashSet<RolesPermisos>();
        }

        public int PermisoId { get; set; }
        [Required(ErrorMessage ="Este campo no puede estar vacio")]
        [MaxLength(50,ErrorMessage ="Permiso muy largo")]
        public string Permiso { get; set; }
        public bool EstadoPermiso { get; set; }

        public ICollection<RolesPermisos> RolesPermisos { get; set; }
    }
}
