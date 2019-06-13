using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class Hospitalizaciones
    {
        public int HospitalizacionId { get; set; }
        public int CirugiaPacienteId { get; set; }
        public int SalaId { get; set; }
        public int CamillaId { get; set; }
        public short Sala { get; set; }
        public short NumeroCamilla { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaAltaAprox { get; set; }
        public DateTime? FechaAlta { get; set; }

        public Camillas Camilla { get; set; }
        public CirugiasPacientes CirugiaPaciente { get; set; }
        public Salas SalaNavigation { get; set; }
    }
}
