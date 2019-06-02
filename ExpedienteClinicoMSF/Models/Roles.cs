using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Roles
    {
        public Roles()
        {
            RolesMenus = new HashSet<RolesMenus>();
            RolesPermisos = new HashSet<RolesPermisos>();
            Usuarios = new HashSet<Usuarios>();
        }
        
        public int RolId { get; set; }
        public string Rol { get; set; }
        [Display(Name = "Descripcion")]
        public string DescripcionRol { get; set; }

        public ICollection<RolesMenus> RolesMenus { get; set; }
        public ICollection<RolesPermisos> RolesPermisos { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
