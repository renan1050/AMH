using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class UsuarioDM
    {
        [FormatedName("Código")]
        public int? usuCodigo { get; set; }

        [FormatedName("Nome")]
        public string usuNome { get; set; }

        public string usuSenha { get; set; }                
        public int? pesCodigo { get; set; }        
        public int? perCodigo { get; set; }
    }
}
