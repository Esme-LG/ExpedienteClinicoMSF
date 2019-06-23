using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Medicamentos
    {
        public Medicamentos()
        {
            Recetas = new HashSet<Recetas>();
        }

        public int MedicamentosId { get; set; }
        public int? HospitalId { get; set; }
        public string Medicamento { get; set; }
        public int Stock { get; set; }

        public Hospitales Hospital { get; set; }
        public ICollection<Recetas> Recetas { get; set; }
    }
}
