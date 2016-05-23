using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class VeiculoDM
    {
        [FormatedName("Código")]
        public int? veiCodigo { get; set; }

        [FormatedName("Marca")]
        public string veiMarca { get; set; }

        [FormatedName("Modelo")]
        public string veiModelo { get; set; }

        [FormatedName("Placa")]
        public string veiPlaca { get; set; }

        [FormatedName("RENAVAM")]
        public string veiRENAVAM { get; set; }

        public int? pesCodigoC { get; set; }
    }
}
