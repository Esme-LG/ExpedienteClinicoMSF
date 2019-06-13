using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Ubicaciones
    {
        public int RegionId { get; set; }
        public int DireccionId { get; set; }

        public Direcciones Direccion { get; set; }
        public Regiones Region { get; set; }
    }
}
