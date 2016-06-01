using RegrasDeNegocios.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Classes
{
    public class Usuario
    {
        public static string gTabela = "usuario";

        public List<UsuarioDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(UsuarioDM)).Cast<UsuarioDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(UsuarioDM pUsuarioDM, Action<string> pCarregar)
        {
            if (Database.Insert(gTabela, pUsuarioDM))
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
        public UsuarioDM SelectCodigo(string pChave)
        {
            return (UsuarioDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(UsuarioDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(UsuarioDM pUsuarioDM)
        {
            return Database.Update(gTabela, pUsuarioDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(UsuarioDM));
        }
    }
}
