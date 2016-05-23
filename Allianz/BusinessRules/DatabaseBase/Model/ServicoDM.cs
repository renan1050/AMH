using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class ServicoDM
    {
        [FormatedName("Código")]
        public int? serCodigo { get; set; }

        [FormatedName("Descrição")]
        public string serDescricao { get; set; }

        [FormatedName("Valor")]
        public decimal? serValor { get; set; }
    }
}
