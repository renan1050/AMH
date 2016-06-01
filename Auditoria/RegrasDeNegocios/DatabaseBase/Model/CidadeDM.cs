using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Model
{
    public class CidadeDM
    {
        [FormatedName("Código")]
        public int? cidCodigo { get; set; }

        public int? estCodigo { get; set; }

        [FormatedName("Nome")]
        public string cidNome { get; set; }
    }
}
