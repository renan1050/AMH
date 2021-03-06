﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Model
{
    public class OrcamentoDM
    {
        [FormatedName("Código")]
        public int? orcCodigo { get; set; }

        [FormatedName("Data de criação")]
        public DateTime orcDataCriacao { get; set; }

        public int? pesCodigoC { get; set; }

        [FormatedName("Status")]
        public int? orcStatus { get; set; }
    }
}
