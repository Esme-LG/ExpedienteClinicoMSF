using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Especialidades
    {
        public Especialidades()
        {
            Cirugias = new HashSet<Cirugias>();
            Medicos = new HashSet<Medicos>();
        }

        public int EspecialidadId { get; set; }
        public string Especialidad { get; set; }
        public string DescripcionEsp { get; set; }
        public bool? EsQuirurgica { get; set; }
        public bool? EsMedica { get; set; }

        public ICollection<Cirugias> Cirugias { get; set; }
        public ICollection<Medicos> Medicos { get; set; }
    }
}
