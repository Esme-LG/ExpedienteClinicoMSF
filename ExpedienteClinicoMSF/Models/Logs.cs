using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Logs
    {
        public int UsuarioId { get; set; }
        public int LogId { get; set; }
        public string Accion { get; set; }
        public string Entidad { get; set; }
        public DateTime FechaAccion { get; set; }

        public Usuarios Usuario { get; set; }
    }
}
