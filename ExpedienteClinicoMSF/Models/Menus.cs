using System;
using System.Collections.Generic;

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
        public string Opcion { get; set; }
        public string Direccion { get; set; }

        public Menus MenMenu { get; set; }
        public ICollection<Menus> InverseMenMenu { get; set; }
        public ICollection<RolesMenus> RolesMenus { get; set; }
    }
}
