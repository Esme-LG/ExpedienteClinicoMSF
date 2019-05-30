using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Diagnosticos
    {
        public int DiagnosticoId { get; set; }
        public int CodigoId { get; set; }
        public int ConsultaId { get; set; }
        public string Diagnostico { get; set; }
        public string Comentario { get; set; }

        public CodigosCie10 Codigo { get; set; }
        public ConsultasMedicas Consulta { get; set; }
    }
}
