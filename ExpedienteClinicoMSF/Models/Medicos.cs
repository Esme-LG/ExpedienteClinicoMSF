using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Medicos
    {
        public Medicos()
        {
            CirugiasPacientes = new HashSet<CirugiasPacientes>();
            ConsultasMedicas = new HashSet<ConsultasMedicas>();
        }

        public int MedicoId { get; set; }
        public int EspecialidadId { get; set; }
        public int HospitalId { get; set; }
        public string NumMedico { get; set; }

        public Especialidades Especialidad { get; set; }
        public Hospitales Hospital { get; set; }
        public ICollection<CirugiasPacientes> CirugiasPacientes { get; set; }
        public ICollection<ConsultasMedicas> ConsultasMedicas { get; set; }
    }
}
