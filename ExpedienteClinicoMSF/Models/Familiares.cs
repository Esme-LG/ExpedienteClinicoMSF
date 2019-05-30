using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Familiares
    {
        public Familiares()
        {
            Antecedentes = new HashSet<Antecedentes>();
        }

        public int FamiliarId { get; set; }
        public int ParentescoId { get; set; }
        public int DireccionId { get; set; }
        public int GeneroId { get; set; }
        public string FamPrimerNombre { get; set; }
        public string FamSegundoNombre { get; set; }
        public string FamApellidoPaterno { get; set; }
        public string FamApellidoMaterno { get; set; }
        public string FamApellidoCasada { get; set; }
        public DateTime FamFechaNacimiento { get; set; }
        public DateTime? FechaMuerte { get; set; }
        public string CausaMuerte { get; set; }

        public Direcciones Direccion { get; set; }
        public Generos Genero { get; set; }
        public Parentescos Parentesco { get; set; }
        public ICollection<Antecedentes> Antecedentes { get; set; }
    }
}
