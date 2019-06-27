using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpedienteClinicoMSF.Models
{
    public partial class SignosVitales
    {
        public SignosVitales()
        {
            ConsultasMedicas = new HashSet<ConsultasMedicas>();
        }

        public int SignoVitalId { get; set; }
        [Required]
        [Range(0,50,ErrorMessage ="Ingrese una temperatura entre 0 y 50 grados Centigrados")]
        public decimal Temperatura { get; set; }
        [Required]
        public short Pulso { get; set; }
        [Required]
        public short? FrecRespiratoria { get; set; }
        [Required]
        public string PresionArterial { get; set; }
        [Required]
        [Range(0,900,ErrorMessage ="Ingrese un peso entre  a 900 libras")]
        public decimal Peso { get; set; }
        [Required]
        [Range(0,5,ErrorMessage ="Ingrese una estatura entre 0 y 5 Metros")]
        public decimal Estatura { get; set; }

        public ICollection<ConsultasMedicas> ConsultasMedicas { get; set; }
    }
}
