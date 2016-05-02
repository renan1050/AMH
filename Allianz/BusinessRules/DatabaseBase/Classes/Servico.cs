using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class Servico
    {
        public static string gTabela = "servico";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<ServicoDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(ServicoDM)).Cast<ServicoDM>().ToList();
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
        public ServicoDM SelectCodigo(string pChave)
        {
            return (ServicoDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(ServicoDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(ServicoDM pServicoDM)
        {
            return Database.Update(gTabela, pServicoDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(ServicoDM));
        }
    }
}
