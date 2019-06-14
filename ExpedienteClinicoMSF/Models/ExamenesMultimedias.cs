using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class ExamenesMultimedias
    {
        public int TipoMultimediaId { get; set; }
        public int ExamenId { get; set; }

        public Examenes Examen { get; set; }
        public TiposMultimedia TipoMultimedia { get; set; }
    }
}
