﻿using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class SignosVitales
    {
        public SignosVitales()
        {
            ConsultasMedicas = new HashSet<ConsultasMedicas>();
        }

        public int SignoVitalId { get; set; }
        public decimal Temperatura { get; set; }
        public short Pulso { get; set; }
        public short? FrecRespiratoria { get; set; }
        public string PresionArterial { get; set; }
        public decimal Peso { get; set; }
        public decimal Estatura { get; set; }

        public ICollection<ConsultasMedicas> ConsultasMedicas { get; set; }
    }
}
