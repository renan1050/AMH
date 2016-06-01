using RegrasDeNegocios.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Classes
{
    public class Perfil
    {
        public static string gTabela = "perfil";
        //seleciona todos dados no banco clientes e retorna um datatable
        public List<PerfilDM> AtualizarGrade(Dictionary<string, string> pParamentros)
        {
            return Database.SelecionarTudo(gTabela, pParamentros, typeof(PerfilDM)).Cast<PerfilDM>().ToList();
        }

        public List<PerfilDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(PerfilDM)).Cast<PerfilDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(PerfilDM pPerfilDM, Action<string> pCarregar)
        {
            if (Database.Insert(gTabela, pPerfilDM))
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
        public PerfilDM SelectCodigo(string pChave)
        {
            return (PerfilDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(EstadoDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(PerfilDM pPerfilDM)
        {
            return Database.Update(gTabela, pPerfilDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(PerfilDM));
        }
    }
}
