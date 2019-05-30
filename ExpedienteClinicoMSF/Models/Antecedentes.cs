using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Antecedentes
    {
        public int AntecedenteId { get; set; }
        public int? ExpedienteId { get; set; }
        public int? FamiliarId { get; set; }
        public string Enfermedad { get; set; }
        public DateTime? FechaDiagnostico { get; set; }
        public string EstadoEnfermedad { get; set; }

        public Expedientes Expediente { get; set; }
        public Familiares Familiar { get; set; }
    }
}
