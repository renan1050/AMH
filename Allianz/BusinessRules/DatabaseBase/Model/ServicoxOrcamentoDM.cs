using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class ServicoxOrcamentoDM
    {
        [FormatedName("Código")]
        public int? genCodigo { get; set; }

        [FormatedName("Serviço")]
        public int? serCodigo { get; set; }

        [FormatedName("Orçamento")]
        public int? orcCodigo { get; set; }

        [FormatedName("Produto")]
        public int? proCodigo { get; set; }

        [FormatedName("Valor unitário")]
        public decimal? genValorUnitario { get; set; }

        [FormatedName("Quantidade")]
        public int? genQuantidade { get; set; }

        [FormatedName("Valor total")]
        public decimal? genValorTotal { get; set; }
    }
}
