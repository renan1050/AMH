using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class VeiculoDM
    {
        public int? veiCodigo { get; set; }
        public string veiMarca { get; set; }
        public string veiModelo { get; set; }
        public string veiPlaca { get; set; }        
        public string veiRENAVAM { get; set; }
        public int? pesCodigoC { get; set; }
    }
}
