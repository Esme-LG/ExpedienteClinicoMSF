using System;
using System.Collections.Generic;

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
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoCasada { get; set; }
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
