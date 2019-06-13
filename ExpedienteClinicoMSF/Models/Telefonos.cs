using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Telefonos
    {
        public int TelefonoId { get; set; }
        public int? PersonaId { get; set; }
        public int? UsuarioId { get; set; }
        public int? ExpedienteId { get; set; }
        public int? ResponsableId { get; set; }
        public string Numero { get; set; }

        public Expedientes Expediente { get; set; }
        public Responsables Responsable { get; set; }
        public Usuarios Usuarios { get; set; }
    }
}
