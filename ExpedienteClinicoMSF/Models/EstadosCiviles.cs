using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class EstadosCiviles
    {
        public EstadosCiviles()
        {
            Expedientes = new HashSet<Expedientes>();
            Usuarios = new HashSet<Usuarios>();
        }

        public int EstadoCivilId { get; set; }
        public string EstadoCivil { get; set; }

        public ICollection<Expedientes> Expedientes { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
