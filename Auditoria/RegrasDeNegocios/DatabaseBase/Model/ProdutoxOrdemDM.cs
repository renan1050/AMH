using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Model
{
    public class ProdutoxOrdemDM
    {
        [FormatedName("Código")]
        public int? genCodigo { get; set; }
        
        public int? ordCodigo { get; set; }

        public int? proCodigo { get; set; }

        [FormatedName("Valor unitário")]
        public decimal? genValorUnitario { get; set; }

        [FormatedName("Quantidade")]
        public int? genQuantidade { get; set; }

        [FormatedName("Valor total")]
        public decimal? genValorTotal { get; set; }
    }

    public class FormatedName : Attribute
    {
        public string Name { get; set; }
        public FormatedName(string pName)
        {
            Name = pName;
        }
    }
}
