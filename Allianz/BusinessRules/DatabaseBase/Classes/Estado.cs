using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class Estado
    {
        public static string gTabela = "estado";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<EstadoDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(EstadoDM)).Cast<EstadoDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(EstadoDM pEstadoDM)
        {
            return Database.Insert(gTabela, pEstadoDM);
        }

        //seleciona um cliente de acordo com seu codigo
        public EstadoDM SelectCodigo(string pChave)
        {
            return (EstadoDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(EstadoDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(EstadoDM pEstadoDM)
        {
            return Database.Update(gTabela, pEstadoDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(EstadoDM));
        }
    }
}
