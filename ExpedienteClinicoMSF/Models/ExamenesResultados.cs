using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpedienteClinicoMSF.Models
{
    public partial class ExamenesResultados
    {
        public int ExamenResultadoId { get; set; }
        [Display(Name = "Examen")]
        public int ExamenId { get; set; }
        public int? ExamenPacienteId { get; set; }
        public string Resultado { get; set; }
        public string Medida { get; set; }
        [Display(Name = "Valor mínimo")]
        public decimal? ValorMin { get; set; }
        [Display(Name = "Valor máximo")]
        public decimal? ValorMax { get; set; }
        public decimal? Valor { get; set; }

        public Examenes Examen { get; set; }
        public ExamenesPacientes ExamenPaciente { get; set; }
    }
}
