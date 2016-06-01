using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Model
{
    public class PerfilxTelaDM
    {
        [FormatedName("Código")]
        public int? pxtCodigo { get; set; }

        public int? perCodigo { get; set; }

        public string pxtTela { get; set; }
    }
}
