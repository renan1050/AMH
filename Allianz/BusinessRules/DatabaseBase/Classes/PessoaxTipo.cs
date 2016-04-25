using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class PessoaxTipo
    {
        public static string gTabela = "pessoaxtipo";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<PessoaxTipoDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(PessoaxTipoDM)).Cast<PessoaxTipoDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(PessoaxTipoDM pPessoaxTipoDM)
        {
            return Database.Insert(gTabela, pPessoaxTipoDM);
        }

        //seleciona um cliente de acordo com seu codigo
        public PessoaxTipoDM SelectCodigo(string pChave)
        {
            return (PessoaxTipoDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(PessoaxTipoDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(PessoaxTipoDM pPessoaxTipoDM)
        {
            return Database.Update(gTabela, pPessoaxTipoDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(PessoaxTipoDM));
        }
    }
}
