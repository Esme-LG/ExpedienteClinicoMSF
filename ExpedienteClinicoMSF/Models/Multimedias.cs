using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Multimedias
    {
        public Multimedias()
        {
            PresentacionesExamenes = new HashSet<PresentacionesExamenes>();
        }

        public int MultimediaId { get; set; }
        public int TipoMultimediaId { get; set; }
        public string Archivo { get; set; }
        public string Formato { get; set; }

        public TiposMultimedia TipoMultimedia { get; set; }
        public ICollection<PresentacionesExamenes> PresentacionesExamenes { get; set; }
    }
}
