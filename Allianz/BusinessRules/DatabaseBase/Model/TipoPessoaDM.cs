using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class TipoPessoaDM
    {
        [FormatedName("Código")]
        public int? tipCodigo { get; set; }

        [FormatedName("Sigla")]
        public string tipSigla { get; set; }

        [FormatedName("Nome")]
        public string tipNome { get; set; }
    }
}
