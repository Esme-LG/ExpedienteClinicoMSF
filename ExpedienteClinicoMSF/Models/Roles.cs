using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Rol")]
        [Required]
        [MaxLength(20, ErrorMessage = "El nombre es demasiado largo, solo se permiten 20 caracteres")]
        public string Rol { get; set; }
        [Display(Name = "Descripcion del Rol")]
        [Required]
        [MaxLength(150, ErrorMessage = "Descripcion demasido larga")]
        public string DescripcionRol { get; set; }

        public ICollection<RolesMenus> RolesMenus { get; set; }
        public ICollection<RolesPermisos> RolesPermisos { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
