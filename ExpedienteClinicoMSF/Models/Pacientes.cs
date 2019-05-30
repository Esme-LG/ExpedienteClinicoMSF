using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Pacientes
    {
        public Pacientes()
        {
            CirugiasPacientes = new HashSet<CirugiasPacientes>();
            ConsultasMedicas = new HashSet<ConsultasMedicas>();
        }

        public int PacienteId { get; set; }
        public int ExpedienteId { get; set; }

        public ICollection<CirugiasPacientes> CirugiasPacientes { get; set; }
        public ICollection<ConsultasMedicas> ConsultasMedicas { get; set; }
    }
}
