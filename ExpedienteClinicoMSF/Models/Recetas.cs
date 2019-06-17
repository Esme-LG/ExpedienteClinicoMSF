using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Recetas
    {
        public int MedicamentosId { get; set; }
        public int TratamientoId { get; set; }
        public short Cantidad { get; set; }

        public Medicamentos Medicamentos { get; set; }
        public Tratamientos Tratamiento { get; set; }
    }
}
