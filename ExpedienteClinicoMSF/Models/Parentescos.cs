using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Parentescos
    {
        public Parentescos()
        {
            Familiares = new HashSet<Familiares>();
        }

        public int ParentescoId { get; set; }
        public string Parentesco { get; set; }

        public ICollection<Familiares> Familiares { get; set; }
    }
}
