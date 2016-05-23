using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class ContasReceberDM
    {
        //[FormatedName("Código")]
        public int? corCodigo { get; set; }

        [FormatedName("Tipo cobrança")]
        public int? corTipoCobranca { get; set; }

        [FormatedName("Forma de pagamento")]
        public int? corFormaPagamento { get; set; }

        [FormatedName("Data de entrada")]
        public DateTime corDataEntrada { get; set; }

        [FormatedName("Número do documento")]
        public string corNDocumento { get; set; }

        [FormatedName("Número de parcelas")]
        public int? copNParcelas { get; set; }

        [FormatedName("Valor total")]
        public decimal? corValorTotal { get; set; }

        [FormatedName("Origem")]
        public string corOrigem { get; set; }

        [FormatedName("Vencimento")]
        public DateTime corVencimento { get; set; }

        [FormatedName("Taxa de juros")]
        public decimal? corTaxaJuros { get; set; }

        [FormatedName("Observação")]
        public string corObservacao { get; set; }

        [FormatedName("Status da conta")]
        public string corStatusConta { get; set; }
    }
}
