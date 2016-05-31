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

        public int? pesCodigoF { get; set; }

        [FormatedName("Data de entrada")]
        public string ordDataEntrada { get; set; }

        [FormatedName("Data de saída")]
        public string ordDataSaida { get; set; }

        [FormatedName("Status")]
        public int? ordStatus { get; set; }
    }
}
