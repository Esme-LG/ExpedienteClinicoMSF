using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Responsables
    {
        public Responsables()
        {
            Telefonos = new HashSet<Telefonos>();
        }

        public int ResponsableId { get; set; }
        public int ExpedienteId { get; set; }
        public string RespPrimerNombre { get; set; }
        public string RespSegundoNombre { get; set; }
        public string RespApellidoPaterno { get; set; }
        public string RespApellidoMaterno { get; set; }
        public string RespApellidoCasada { get; set; }
        public DateTime FechaNacimientoResp { get; set; }
        public string Relacion { get; set; }

        public Expedientes Expediente { get; set; }
        public ICollection<Telefonos> Telefonos { get; set; }
    }
}
