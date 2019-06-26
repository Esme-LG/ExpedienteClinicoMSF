using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Personas
    {
        public Personas()
        {
            Familiares = new HashSet<Familiares>();
            Pacientes = new HashSet<Pacientes>();
            Responsables = new HashSet<Responsables>();
            UsuariosNavigation = new HashSet<Usuarios>();
        }
        
        public int PersonaId { get; set; }
        public int? UsuarioId { get; set; }
        public int? PacienteId { get; set; }
        public int? FamiliarId { get; set; }
        public int? ResponsableId { get; set; }
        [Required]
        public string PrimerNombre { get; set; }
        [Required]
        public string SegundoNombre { get; set; }
        [Required]
        public string ApellidoPaterno { get; set; }
        [Required]
        public string ApellidoMaterno { get; set; }
        public string ApellidoCasada { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }

        public Familiares Familiar { get; set; }
        public Pacientes Paciente { get; set; }
        public Responsables Responsable { get; set; }
        public Usuarios Usuarios { get; set; }
        public ICollection<Familiares> Familiares { get; set; }
        public ICollection<Pacientes> Pacientes { get; set; }
        public ICollection<Responsables> Responsables { get; set; }
        public ICollection<Usuarios> UsuariosNavigation { get; set; }
    }
}
