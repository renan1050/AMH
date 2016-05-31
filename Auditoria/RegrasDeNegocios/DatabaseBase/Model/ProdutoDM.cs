using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class ProdutoDM
    {
        [FormatedName("Código")]
        public int? proCodigo { get; set; }

        [FormatedName("Nome")]
        public string proNome { get; set; }

        [FormatedName("Descrição")]
        public string proDescricao { get; set; }

        [FormatedName("Valor unitário")]
        public decimal? proValorUnitario { get; set; }
    }
}
