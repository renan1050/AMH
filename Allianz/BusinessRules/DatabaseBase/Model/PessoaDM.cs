using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Model
{
    public class PessoaDM
    {
        public int pesCodigo { get; set; }
        public string pesNome { get; set; }
        public string pesRazaoSocial { get; set; }
        public char pesTipoPessoa { get; set; }
        public string pesRG { get; set; }
        public string pesCPF { get; set; }
        public string pesCNPJ { get; set; }
        public string pesTelResidencial { get; set; }
        public string pesTelComercial { get; set; }
        public string pesCelular { get; set; }
        public string pesEmail { get; set; }
        public DateTime pesNascimento { get; set; }
        public string pesCEP { get; set; }
        public string pesEndereco { get; set; }
        public string pesNumero { get; set; }
        public string pesComplemento { get; set; }
        public string pesBairro { get; set; }
        public string pesCargo { get; set; }
        public int estCodigo { get; set; }
        public int cidCodigo { get; set; }
    }
}
