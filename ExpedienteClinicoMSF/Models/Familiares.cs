using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Familiares
    {
        public Familiares()
        {
            Antecedentes = new HashSet<Antecedentes>();
            Personas = new HashSet<Personas>();
        }

        public int FamiliarId { get; set; }
        public int ParentescoId { get; set; }
        public int DireccionId { get; set; }
        public int GeneroId { get; set; }
        public int PersonaId { get; set; }
        public int? ExpedienteId { get; set; }
        public DateTime? FechaMuerte { get; set; }
        public string CausaMuerte { get; set; }

        public Direcciones Direccion { get; set; }
        public Expedientes Expediente { get; set; }
        public Generos Genero { get; set; }
        public Parentescos Parentesco { get; set; }
        public Personas Persona { get; set; }
        public ICollection<Antecedentes> Antecedentes { get; set; }
        public ICollection<Personas> Personas { get; set; }
    }
}
