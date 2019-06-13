using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Salas
    {
        public Salas()
        {
            Camillas = new HashSet<Camillas>();
            Hospitalizaciones = new HashSet<Hospitalizaciones>();
        }

        public int SalaId { get; set; }
        public short NumeroSala { get; set; }
        public string NombreSala { get; set; }
        public bool EstadoSala { get; set; }

        public ICollection<Camillas> Camillas { get; set; }
        public ICollection<Hospitalizaciones> Hospitalizaciones { get; set; }
    }
}
