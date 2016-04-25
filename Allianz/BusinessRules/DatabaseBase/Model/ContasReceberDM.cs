using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class ContasReceberDM
    {
        public int corCodigo { get; set; }
        public int ordCodigo { get; set; }
        public int corTipoCobranca { get; set; }
        public int corFormaPagamento { get; set; }
        public DateTime corDataEntrada { get; set; }
        public string corNDocumento { get; set; }
        public int copNParcelas { get; set; }
        public decimal corValorTotal { get; set; }
        public string corOrigem { get; set; }
        public DateTime corVencimento { get; set; }
        public decimal corTaxaJuros { get; set; }
        public string corObservacao { get; set; }
        public char corStatusConta { get; set; }
    }
}
