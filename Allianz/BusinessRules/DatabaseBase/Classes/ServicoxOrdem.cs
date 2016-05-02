using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class ServicoxOrdem
    {
        public static string gTabela = "servicoxordem";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<ServicoxOrdemDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(ServicoxOrdemDM)).Cast<ServicoxOrdemDM>().ToList();
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
        public ServicoxOrdemDM SelectCodigo(string pChave)
        {
            return (ServicoxOrdemDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(ServicoxOrdemDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(ServicoxOrdemDM pServicoxOrdemDM)
        {
            return Database.Update(gTabela, pServicoxOrdemDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(ServicoxOrdemDM));
        }
    }
}
