using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class ExamenesResultados
    {
        public int ExamenResultadoId { get; set; }
        public int ExamenId { get; set; }
        public string Resultado { get; set; }
        public string Medida { get; set; }
        public decimal ValorMin { get; set; }
        public decimal ValorMax { get; set; }

        public Examenes Examen { get; set; }
    }
}
