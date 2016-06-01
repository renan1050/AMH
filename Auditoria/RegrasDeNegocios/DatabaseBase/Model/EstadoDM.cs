using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Model
{
    public class EstadoDM
    {
        [FormatedName("Código")]
        public int? estCodigo { get; set; }
        
        [FormatedName("Sigla")]
        public string estSigla { get; set; }

        [FormatedName("Nome")]
        public string estNome { get; set; }
    }
}
