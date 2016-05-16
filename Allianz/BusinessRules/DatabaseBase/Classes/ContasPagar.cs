using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class ContasPagar
    {
        public static string gTabela = "contaspagar";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<ContasPagarDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(ContasPagarDM)).Cast<ContasPagarDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(ContasPagarDM pContasPagarDM, Action<string> pCarregar)
        {
            if (Database.Insert(gTabela, pContasPagarDM))
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
        public ContasPagarDM SelectCodigo(string pChave)
        {
            return (ContasPagarDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(ContasPagarDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(ContasPagarDM pContasPagarDM)
        {
            return Database.Update(gTabela, pContasPagarDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(ContasPagarDM));
        }
    }
}
