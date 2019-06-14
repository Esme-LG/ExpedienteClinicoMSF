﻿using System;
using System.Collections.Generic;

namespace ExpedienteClinicoMSF.Models
{
    public partial class TiposMultimedia
    {
        public TiposMultimedia()
        {
            ExamenesMultimedias = new HashSet<ExamenesMultimedias>();
            Multimedias = new HashSet<Multimedias>();
        }

        public int TipoMultimediaId { get; set; }
        public string TipoMultimedia { get; set; }

        public ICollection<ExamenesMultimedias> ExamenesMultimedias { get; set; }
        public ICollection<Multimedias> Multimedias { get; set; }
    }
}
