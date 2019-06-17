using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Responsables
    {
        public Responsables()
        {
            Personas = new HashSet<Personas>();
            Telefonos = new HashSet<Telefonos>();
        }

        public int ResponsableId { get; set; }
        public int ExpedienteId { get; set; }
        public int PersonaId { get; set; }
        public string Relacion { get; set; }

        public Expedientes Expediente { get; set; }
        public Personas Persona { get; set; }
        public ICollection<Personas> Personas { get; set; }
        public ICollection<Telefonos> Telefonos { get; set; }
    }
}
