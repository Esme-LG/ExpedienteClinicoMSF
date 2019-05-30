using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Tratamientos
    {
        public Tratamientos()
        {
            Recetas = new HashSet<Recetas>();
        }

        public int TratamientoId { get; set; }
        public int ConsultaId { get; set; }
        public string Dosis { get; set; }
        public string Frecuencia { get; set; }
        public string Durante { get; set; }

        public ConsultasMedicas Consulta { get; set; }
        public ICollection<Recetas> Recetas { get; set; }
    }
}
