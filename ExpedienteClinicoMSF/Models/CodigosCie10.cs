using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class CodigosCie10
    {
        public CodigosCie10()
        {
            Diagnosticos = new HashSet<Diagnosticos>();
        }

        public int CodigoId { get; set; }
        public string Cie10 { get; set; }
        public string NomEnfermedad { get; set; }

        public ICollection<Diagnosticos> Diagnosticos { get; set; }
    }
}
