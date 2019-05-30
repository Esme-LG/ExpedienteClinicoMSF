using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Examenes
    {
        public Examenes()
        {
            ExamenesPacientes = new HashSet<ExamenesPacientes>();
            ExamenesResultados = new HashSet<ExamenesResultados>();
            PresentacionesExamenes = new HashSet<PresentacionesExamenes>();
        }

        public int ExamenId { get; set; }
        public int CategoriaId { get; set; }
        public string Examen { get; set; }
        public string DescripcionExamen { get; set; }

        public Categoria Categoria { get; set; }
        public ICollection<ExamenesPacientes> ExamenesPacientes { get; set; }
        public ICollection<ExamenesResultados> ExamenesResultados { get; set; }
        public ICollection<PresentacionesExamenes> PresentacionesExamenes { get; set; }
    }
}
