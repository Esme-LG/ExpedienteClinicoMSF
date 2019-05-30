using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Examenes = new HashSet<Examenes>();
        }

        public int CategoriaId { get; set; }
        public string Categoria1 { get; set; }
        public string CategoriaDescripcion { get; set; }

        public ICollection<Examenes> Examenes { get; set; }
    }
}
