using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Menus
    {
        public Menus()
        {
            InverseMenMenu = new HashSet<Menus>();
            RolesMenus = new HashSet<RolesMenus>();
        }

        public int MenuId { get; set; }
        public int? MenMenuId { get; set; }
        [Required]
        [MaxLength(25)]
        public string Opcion { get; set; }
        [MaxLength(100)]
        public string Direccion { get; set; }

        public Menus MenMenu { get; set; }
        public ICollection<Menus> InverseMenMenu { get; set; }
        public ICollection<RolesMenus> RolesMenus { get; set; }
    }
}
