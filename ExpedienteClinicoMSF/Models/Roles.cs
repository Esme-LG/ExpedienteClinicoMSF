using System;
using System.Collections.Generic;

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
        public string DescripcionRol { get; set; }

        public ICollection<RolesMenus> RolesMenus { get; set; }
        public ICollection<RolesPermisos> RolesPermisos { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
