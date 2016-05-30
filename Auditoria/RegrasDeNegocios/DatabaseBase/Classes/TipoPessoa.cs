using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class TipoPessoa
    {
        public static string gTabela = "tipopessoa";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<TipoPessoaDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(TipoPessoaDM)).Cast<TipoPessoaDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(TipoPessoaDM pTipoPessoaDM, Action<string> pCarregar)
        {
            if (Database.Insert(gTabela, pTipoPessoaDM))
            {
                pCarregar(Database.SelecionarUltimoId(gTabela));
                return true;
            }
            else
            {
                return false;
            }
        }

        //seleciona um cliente de acordo com seu codigo
        public TipoPessoaDM SelectCodigo(string pChave)
        {
            return (TipoPessoaDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(TipoPessoaDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(TipoPessoaDM pTipoPessoaDM)
        {
            return Database.Update(gTabela, pTipoPessoaDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(TipoPessoaDM));
        }
    }
}
