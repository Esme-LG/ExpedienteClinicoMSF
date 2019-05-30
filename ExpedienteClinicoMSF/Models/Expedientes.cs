using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Expedientes
    {
        public Expedientes()
        {
            Antecedentes = new HashSet<Antecedentes>();
            Responsables = new HashSet<Responsables>();
            Telefonos = new HashSet<Telefonos>();
        }

        public int ExpedienteId { get; set; }
        public int EstadoCivilId { get; set; }
        public int DireccionId { get; set; }
        public int GeneroId { get; set; }
        public string NumExpediente { get; set; }
        public string ExpPrimerNombre { get; set; }
        public string ExpSegundoNombre { get; set; }
        public string ExpApellidoPaterno { get; set; }
        public string ExpApellidoMaterno { get; set; }
        public string ExpApellidoCasada { get; set; }
        public DateTime ExpFechaNacimiento { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ExpEmail { get; set; }

        public EstadosCiviles EstadoCivil { get; set; }
        public Generos Genero { get; set; }
        public ICollection<Antecedentes> Antecedentes { get; set; }
        public ICollection<Responsables> Responsables { get; set; }
        public ICollection<Telefonos> Telefonos { get; set; }
    }
}
