﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Model
{
    public class PerfilDM
    {
        [FormatedName("Código")]
        public int? perCodigo { get; set; }

        [FormatedName("Nome")]
        public string perNome { get; set; }
    }
}
