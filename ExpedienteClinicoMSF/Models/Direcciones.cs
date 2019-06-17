using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Direcciones
    {
        public Direcciones()
        {
            Expedientes = new HashSet<Expedientes>();
            Familiares = new HashSet<Familiares>();
            Hospitales = new HashSet<Hospitales>();
            Ubicaciones = new HashSet<Ubicaciones>();
            Usuarios = new HashSet<Usuarios>();
        }

        public int DireccionId { get; set; }
        public int PaisId { get; set; }
        public string Ciudad { get; set; }
        public string Calle { get; set; }
        public short NumeroCasa { get; set; }

        public Paises Pais { get; set; }
        public ICollection<Expedientes> Expedientes { get; set; }
        public ICollection<Familiares> Familiares { get; set; }
        public ICollection<Hospitales> Hospitales { get; set; }
        public ICollection<Ubicaciones> Ubicaciones { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
