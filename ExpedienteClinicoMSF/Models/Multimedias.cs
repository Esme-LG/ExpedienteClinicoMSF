using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Multimedias
    {
        public int MultimediaId { get; set; }
        public int TipoMultimediaId { get; set; }
        public int? ExamenPacienteId { get; set; }
        public string Archivo { get; set; }
        public string Formato { get; set; }

        public ExamenesPacientes ExamenPaciente { get; set; }
        public TiposMultimedia TipoMultimedia { get; set; }
    }
}
