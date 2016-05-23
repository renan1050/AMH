using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class ContasPagarDM
    {
        [FormatedName("Código")]
        public int? copCodigo { get; set; }

        [FormatedName("Nome credor")]
        public string copNomeCredor { get; set; }

        [FormatedName("Forma de pagamento")]
        public string copFormaPagamento { get; set; }

        [FormatedName("Documento")]
        public string copNDocumento { get; set; }

        [FormatedName("Entrada")]
        public DateTime copDataEntrada { get; set; }

        [FormatedName("Valor total")]
        public decimal copValorTotal { get; set; }

        [FormatedName("Número de parcelas")]
        public int? copNParcelas { get; set; }

        [FormatedName("Vencimento")]
        public DateTime copVencimento { get; set; }

        [FormatedName("Observação")]
        public string copObservacao { get; set; }

        [FormatedName("Status")]
        public string copStatusConta { get; set; }
                
        public int? pesCodigoC { get; set; }
    }
}
