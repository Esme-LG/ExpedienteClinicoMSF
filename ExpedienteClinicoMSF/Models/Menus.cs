using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Display(Name = "Menú raíz")]
        public int? MenMenuId { get; set; }
        [Display(Name = "Opción")]
        public string Opcion { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Display(Name = "Menú raíz")]
        public Menus MenMenu { get; set; }
        public ICollection<Menus> InverseMenMenu { get; set; }
        public ICollection<RolesMenus> RolesMenus { get; set; }
    }
}
