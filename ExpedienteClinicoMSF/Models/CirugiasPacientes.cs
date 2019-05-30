using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class CirugiasPacientes
    {
        public int CirugiaPacienteId { get; set; }
        public int CirugiaId { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public DateTime FechaCirugia { get; set; }

        public Cirugias Cirugia { get; set; }
        public Medicos Medico { get; set; }
        public Pacientes Paciente { get; set; }
    }
}
