using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Regiones
    {
        public Regiones()
        {
            InverseRegRegion = new HashSet<Regiones>();
            Ubicaciones = new HashSet<Ubicaciones>();
        }

        public int RegionId { get; set; }
        public int? RegRegionId { get; set; }
        public string Region { get; set; }

        public Regiones RegRegion { get; set; }
        public ICollection<Regiones> InverseRegRegion { get; set; }
        public ICollection<Ubicaciones> Ubicaciones { get; set; }
    }
}
