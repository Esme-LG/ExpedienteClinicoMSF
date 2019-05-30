using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Logs = new HashSet<Logs>();
            Telefonos = new HashSet<Telefonos>();
        }

        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public int? HospitalId { get; set; }
        public int EstadoCivilId { get; set; }
        public int DireccionId { get; set; }
        public int GeneroId { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoCasada { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Activo { get; set; }
        public bool? Bloqueado { get; set; }
        public short Intentos { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Direcciones Direccion { get; set; }
        public EstadosCiviles EstadoCivil { get; set; }
        public Generos Genero { get; set; }
        public Hospitales Hospital { get; set; }
        public Roles Rol { get; set; }
        public ICollection<Logs> Logs { get; set; }
        public ICollection<Telefonos> Telefonos { get; set; }
    }
}
