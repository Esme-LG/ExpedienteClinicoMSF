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
        public string CorrelativoCamilla { get; set; }

        public ICollection<Hospitalizaciones> Hospitalizaciones { get; set; }
    }
}
