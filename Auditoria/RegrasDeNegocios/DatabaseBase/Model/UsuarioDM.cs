using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Model
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

    public class UsuarioVM
    {
        [FormatedName("Código")]
        public int? usuCodigo { get; set; }

        [FormatedName("Nome")]
        public string usuNome { get; set; }

        [FormatedName("Pessoa")]
        public string Pessoa { get; set; }

        [FormatedName("Perfil")]
        public string Perfil { get; set; }        
    }
}
