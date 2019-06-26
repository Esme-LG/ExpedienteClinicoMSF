using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Logs = new HashSet<Logs>();
            Personas = new HashSet<Personas>();
            Telefonos = new HashSet<Telefonos>();
        }

        public int PersonaId { get; set; }
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public int HospitalId { get; set; }
        public int EstadoCivilId { get; set; }
        public int DireccionId { get; set; }
        public int GeneroId { get; set; }
        [MaxLength(50,ErrorMessage ="El correo es muy largo")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Este campo no puede estar vacio")]
        [MaxLength (25)]
        public string Pass { get; set; }
        public bool Activo { get; set; }
        public bool? Bloqueado { get; set; }
        public short Intentos { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Direcciones Direccion { get; set; }
        public EstadosCiviles EstadoCivil { get; set; }
        public Generos Genero { get; set; }
        public Hospitales Hospital { get; set; }
        public Personas Persona { get; set; }
        public Roles Rol { get; set; }
        public ICollection<Logs> Logs { get; set; }
        public ICollection<Personas> Personas { get; set; }
        public ICollection<Telefonos> Telefonos { get; set; }


    }
}
