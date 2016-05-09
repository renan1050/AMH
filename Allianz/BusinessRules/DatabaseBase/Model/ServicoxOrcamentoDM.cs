using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class ServicoxOrcamentoDM
    {
        public int genCodigo { get; set; }
        public int serCodigo { get; set; }
        public int orcCodigo { get; set; }
        public int proCodigo { get; set; }
        public decimal genValorUnitario { get; set; }
        public int genQuantidade { get; set; }
        public decimal genValorTotal { get; set; }
    }
}
