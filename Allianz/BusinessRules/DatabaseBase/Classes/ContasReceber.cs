using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class ContasReceber
    {
        public static string gTabela = "contasreceber";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<ContasReceberDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(ContasReceberDM)).Cast<ContasReceberDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(VeiculoDM pVeiculoDM, Action<string> pCarregar)
        {
            if (Database.Insert(gTabela, pVeiculoDM))
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
        public ContasReceberDM SelectCodigo(string pChave)
        {
            return (ContasReceberDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(ContasReceberDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(ContasReceberDM pContasReceberDM)
        {
            return Database.Update(gTabela, pContasReceberDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(ContasReceberDM));
        }
    }
}
