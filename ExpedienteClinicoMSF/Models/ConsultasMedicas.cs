using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpedienteClinicoMSF.Models
{
    public partial class ConsultasMedicas
    {
        public ConsultasMedicas()
        {
            Diagnosticos = new HashSet<Diagnosticos>();
            ExamenesPacientes = new HashSet<ExamenesPacientes>();
            Tratamientos = new HashSet<Tratamientos>();
        }

        public int ConsultaId { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public int SignoVitalId { get; set; }
        public string TipoReserva { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaConsulta { get; set; }
        [Required]
        public string Sintomas { get; set; }

        public Medicos Medico { get; set; }
        public Pacientes Paciente { get; set; }
        public SignosVitales SignoVital { get; set; }
        public ICollection<Diagnosticos> Diagnosticos { get; set; }
        public ICollection<ExamenesPacientes> ExamenesPacientes { get; set; }
        public ICollection<Tratamientos> Tratamientos { get; set; }
    }
}
