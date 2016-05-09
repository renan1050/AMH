using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class Orcamento
    {
        public static string gTabela = "orcamento";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<OrcamentoDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(OrcamentoDM)).Cast<OrcamentoDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(OrcamentoDM pOrcamentoDM, Action<string> pCarregar)
        {
            if (Database.Insert(gTabela, pOrcamentoDM))
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
        public OrcamentoDM SelectCodigo(string pChave)
        {
            return (OrcamentoDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(OrcamentoDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(OrcamentoDM pOrcamentoDM)
        {
            return Database.Update(gTabela, pOrcamentoDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(OrcamentoDM));
        }
    }
}
