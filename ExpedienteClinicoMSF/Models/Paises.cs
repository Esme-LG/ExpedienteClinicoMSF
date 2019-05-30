using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Paises
    {
        public Paises()
        {
            Direcciones = new HashSet<Direcciones>();
        }

        public int PaisId { get; set; }
        public string Pais { get; set; }

        public ICollection<Direcciones> Direcciones { get; set; }
    }
}
