using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Model
{
    public class PessoaxTipoDM
    {
        [FormatedName("Código")]
        public int? genCodigo { get; set; }

        public int? pesCodigo { get; set; }

        public int? tipCodigo { get; set; }
    }
}
