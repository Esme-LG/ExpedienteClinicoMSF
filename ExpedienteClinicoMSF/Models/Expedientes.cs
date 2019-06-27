using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Expedientes
    {
        public Expedientes()
        {
            Antecedentes = new HashSet<Antecedentes>();
            Familiares = new HashSet<Familiares>();
            Responsables = new HashSet<Responsables>();
            Telefonos = new HashSet<Telefonos>();
        }

        public int ExpedienteId { get; set; }
        public int PacienteId { get; set; }
        public int EstadoCivilId { get; set; }
        public int DireccionId { get; set; }
        public int GeneroId { get; set; }
        public string NumExpediente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool ExpEstado { get; set; }

        public Direcciones Direccion { get; set; }
        public EstadosCiviles EstadoCivil { get; set; }
        public Generos Genero { get; set; }
        public Pacientes Paciente { get; set; }
        public ICollection<Antecedentes> Antecedentes { get; set; }
        public ICollection<Familiares> Familiares { get; set; }
        public ICollection<Responsables> Responsables { get; set; }
        public ICollection<Telefonos> Telefonos { get; set; }
    }
}
