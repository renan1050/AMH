using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class OrdemDM
    {
        [FormatedName("Código")]
        public int? ordCodigo { get; set; }

        public int? pesCodigoC { get; set; }

        public int? veiCodigo { get; set; }

        public int? pesCodigoF { get; set; }

        [FormatedName("Data de entrada")]
        public DateTime ordDataEntrada { get; set; }

        [FormatedName("Data de saída")]
        public DateTime ordDataSaida { get; set; }
    }
}
