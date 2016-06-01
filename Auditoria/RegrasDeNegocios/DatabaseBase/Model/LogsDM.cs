using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Model
{
    public class LogsDM
    {        
        public int? logCodigo { get; set; }
        
        public string logDiaHora { get; set; }

        public string logClasse { get; set; }

        public string logMetodo { get; set; }
        
        public int? usuCodigo { get; set; }
    }
}
