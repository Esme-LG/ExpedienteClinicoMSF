using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class ExamenesPacientes
    {
        public ExamenesPacientes()
        {
            ExamenesResultados = new HashSet<ExamenesResultados>();
            Multimedias = new HashSet<Multimedias>();
        }

        public int ExamenPacienteId { get; set; }
        public int ExamenId { get; set; }
        public int ConsultaId { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public DateTime? FechaLectura { get; set; }
        public string Lectura { get; set; }

        public ConsultasMedicas Consulta { get; set; }
        public Examenes Examen { get; set; }
        public ICollection<ExamenesResultados> ExamenesResultados { get; set; }
        public ICollection<Multimedias> Multimedias { get; set; }
    }
}
