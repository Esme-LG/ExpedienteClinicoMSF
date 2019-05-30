using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class PresentacionesExamenes
    {
        public int MultimediaId { get; set; }
        public int ExamenId { get; set; }

        public Examenes Examen { get; set; }
        public Multimedias Multimedia { get; set; }
    }
}
