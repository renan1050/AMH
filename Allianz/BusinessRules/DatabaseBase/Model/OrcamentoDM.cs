using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class OrcamentoDM
    {
        [FormatedName("Código")]
        public int? orcCodigo { get; set; }

        [FormatedName("Data de criação")]
        public DateTime orcDataCriacao { get; set; }

        public int? pesCodigoC { get; set; }

        public int? veiCodigo { get; set; }
    }
}
