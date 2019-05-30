using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Cirugias
    {
        public Cirugias()
        {
            CirugiasPacientes = new HashSet<CirugiasPacientes>();
        }

        public int CirugiaId { get; set; }
        public int EspecialidadId { get; set; }
        public string Cirugia { get; set; }

        public Especialidades Especialidad { get; set; }
        public ICollection<CirugiasPacientes> CirugiasPacientes { get; set; }
    }
}
