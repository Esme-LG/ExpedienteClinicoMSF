using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Examen")]
        public int ExamenId { get; set; }
        [Display(Name = "Consulta")]
        public int ConsultaId { get; set; }
        [Display(Name = "Fecha de realización")]
        public DateTime FechaRealizacion { get; set; }
        [Display(Name = "Fecha de lectura")]
        public DateTime? FechaLectura { get; set; }
        public string Lectura { get; set; }

        public ConsultasMedicas Consulta { get; set; }
        public Examenes Examen { get; set; }
        public ICollection<ExamenesResultados> ExamenesResultados { get; set; }
        public ICollection<Multimedias> Multimedias { get; set; }
    }
}
