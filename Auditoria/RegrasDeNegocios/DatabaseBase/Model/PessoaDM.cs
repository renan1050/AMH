using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Model
{
    public class PessoaDM
    {
        [FormatedName("Código")]
        public int? pesCodigo { get; set; }

        [FormatedName("Nome")]
        public string pesNome { get; set; }

        [FormatedName("Razão social")]
        public string pesRazaoSocial { get; set; }

        [FormatedName("Tipo de pessoa")]
        public string pesTipoPessoa { get; set; }

        [FormatedName("RG")]
        public string pesRG { get; set; }

        [FormatedName("CPF")]
        public string pesCPF { get; set; }

        [FormatedName("CNPJ")]
        public string pesCNPJ { get; set; }

        [FormatedName("Telefone residencial")]
        public string pesTelResidencial { get; set; }

        [FormatedName("Telefone comercial")]
        public string pesTelComercial { get; set; }

        [FormatedName("Celular")]
        public string pesCelular { get; set; }

        [FormatedName("Email")]
        public string pesEmail { get; set; }

        [FormatedName("Data de nascimento")]
        public DateTime pesNascimento { get; set; }

        [FormatedName("CEP")]
        public string pesCEP { get; set; }

        [FormatedName("Endereço")]
        public string pesEndereco { get; set; }

        [FormatedName("Número")]
        public string pesNumero { get; set; }

        [FormatedName("Complemento")]
        public string pesComplemento { get; set; }

        [FormatedName("Bairro")]
        public string pesBairro { get; set; }

        [FormatedName("Cargo")]
        public string pesCargo { get; set; }

        public int? estCodigo { get; set; }

        public int? cidCodigo { get; set; }
    
    
    }
}
