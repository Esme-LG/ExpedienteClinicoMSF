using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Hospitales
    {
        public Hospitales()
        {
            Medicos = new HashSet<Medicos>();
            Usuarios = new HashSet<Usuarios>();
        }

        public int HospitalId { get; set; }
        public int DireccionId { get; set; }
        public string HospitalNombre { get; set; }
        public short DuracionPromConsulta { get; set; }

        public Direcciones Direccion { get; set; }
        public ICollection<Medicos> Medicos { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
