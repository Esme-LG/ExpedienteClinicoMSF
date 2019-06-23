using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Logs
    {
        public int PersonaId { get; set; }
        public int UsuarioId { get; set; }
        public int LogId { get; set; }
        public string Accion { get; set; }
        public string Entidad { get; set; }
        public DateTime FechaAccion { get; set; }
        public string Campo { get; set; }
        public string ValorOriginal { get; set; }
        public string ValorNuevo { get; set; }

        public Usuarios Usuarios { get; set; }
    }
}
