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
            Expedientes = new HashSet<Expedientes>();
            Personas = new HashSet<Personas>();
        }

        public int PacienteId { get; set; }
        public int PersonaId { get; set; }
        public string PacienteEmail { get; set; }

        public Personas Persona { get; set; }
        public ICollection<CirugiasPacientes> CirugiasPacientes { get; set; }
        public ICollection<ConsultasMedicas> ConsultasMedicas { get; set; }
        public ICollection<Expedientes> Expedientes { get; set; }
        public ICollection<Personas> Personas { get; set; }
    }
}
