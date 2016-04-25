using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class Veiculo
    {
        public static string gTabela = "veiculo";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<VeiculoDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(VeiculoDM)).Cast<VeiculoDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(VeiculoDM pVeiculoDM)
        {
            return Database.Insert(gTabela, pVeiculoDM);
        }

        //seleciona um cliente de acordo com seu codigo
        public VeiculoDM SelectCodigo(string pChave)
        {
            return (VeiculoDM) Database.SelecionarPorCodigo(gTabela, pChave, typeof(VeiculoDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(VeiculoDM pVeiculoDM)
        {
            return Database.Update(gTabela, pVeiculoDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave,typeof(VeiculoDM));
        }
    }
}
