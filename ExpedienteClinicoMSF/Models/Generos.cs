using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Generos
    {
        public Generos()
        {
            Expedientes = new HashSet<Expedientes>();
            Familiares = new HashSet<Familiares>();
            Usuarios = new HashSet<Usuarios>();
        }

        public int GeneroId { get; set; }
        public string Genero { get; set; }
        public string Terminacion { get; set; }

        public ICollection<Expedientes> Expedientes { get; set; }
        public ICollection<Familiares> Familiares { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
