using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Hospitales
    {
        public Hospitales()
        {
            Camillas = new HashSet<Camillas>();
            Cirugias = new HashSet<Cirugias>();
            Medicamentos = new HashSet<Medicamentos>();
            Medicos = new HashSet<Medicos>();
            Salas = new HashSet<Salas>();
            Usuarios = new HashSet<Usuarios>();
        }

        public int HospitalId { get; set; }
        public int DireccionId { get; set; }
        public string HospitalNombre { get; set; }
        public short DuracionPromConsulta { get; set; }

        public Direcciones Direccion { get; set; }
        public ICollection<Camillas> Camillas { get; set; }
        public ICollection<Cirugias> Cirugias { get; set; }
        public ICollection<Medicamentos> Medicamentos { get; set; }
        public ICollection<Medicos> Medicos { get; set; }
        public ICollection<Salas> Salas { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
