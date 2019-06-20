using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Camillas
    {
        public Camillas()
        {
            Hospitalizaciones = new HashSet<Hospitalizaciones>();
        }

        public int CamillaId { get; set; }
        public int SalaId { get; set; }
        public int? HospitalId { get; set; }
        public string CorrelativoCamilla { get; set; }
        public bool EstadoCamilla { get; set; }

        public Hospitales Hospital { get; set; }
        public Salas Sala { get; set; }
        public ICollection<Hospitalizaciones> Hospitalizaciones { get; set; }
    }
}
