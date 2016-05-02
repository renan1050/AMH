using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class Cidade
    {
        public static string gTabela = "cidade";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<CidadeDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(CidadeDM)).Cast<CidadeDM>().ToList();
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
        public CidadeDM SelectCodigo(string pChave)
        {
            return (CidadeDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(EstadoDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(CidadeDM pCidadeDM)
        {
            return Database.Update(gTabela, pCidadeDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(CidadeDM));
        }
    }
}
