using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class ServicoxOrcamento
    {
        public static string gTabela = "servicoxorcamento";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<ServicoxOrcamentoDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(ServicoxOrcamentoDM)).Cast<ServicoxOrcamentoDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(ServicoxOrcamentoDM pServicoxOrcamentoDM)
        {
            return Database.Insert(gTabela, pServicoxOrcamentoDM);
        }

        //seleciona um cliente de acordo com seu codigo
        public ServicoxOrcamentoDM SelectCodigo(string pChave)
        {
            return (ServicoxOrcamentoDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(ServicoxOrcamentoDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(ServicoxOrcamentoDM pServicoxOrcamentoDM)
        {
            return Database.Update(gTabela, pServicoxOrcamentoDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(ServicoxOrcamentoDM));
        }
    }
}
