using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class ContasPagarDM
    {
        public int? copCodigo { get; set; }
        public string copNomeCredor { get; set; }
        public string copFormaPagamento { get; set; }
        public string copNDocumento { get; set; }
        public DateTime copDataEntrada { get; set; }
        public decimal copValorTotal { get; set; }
        public int? copNParcelas { get; set; }
        public DateTime copVencimento { get; set; }
        public string copObservacao { get; set; }
        public string copStatusConta { get; set; }
        public int? pesCodigoC { get; set; }
    }
}
