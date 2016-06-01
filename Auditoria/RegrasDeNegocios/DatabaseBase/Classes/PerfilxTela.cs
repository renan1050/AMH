using RegrasDeNegocios.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Classes
{
    public class PerfilxTela
    {
        public static string gTabela = "perfilxtela";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<PerfilxTelaDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(PerfilxTelaDM)).Cast<PerfilxTelaDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(PerfilxTelaDM pPerfilxTelaDM, Action<string> pCarregar = null)
        {
            if (Database.Insert(gTabela, pPerfilxTelaDM))
            {
                if (pCarregar != null)
                    pCarregar(Database.SelecionarUltimoId(gTabela));

                return true;
            }
            else
            {
                return false;
            }
        }

        //seleciona um cliente de acordo com seu codigo
        public PerfilxTelaDM SelectCodigo(string pChave)
        {
            return (PerfilxTelaDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(PerfilxTelaDM));
        }

        public List<PerfilxTelaDM> SelectPorPerfil(string pChave)
        {
            return Database.SelecionarPorCampo(gTabela, pChave, "perCodigo", typeof(PerfilxTelaDM)).Cast<PerfilxTelaDM>().ToList();
        }

        //salva ediçoes no cliente
        public bool EditarCliente(PerfilxTelaDM pPerfilxTelaDM)
        {
            return Database.Update(gTabela, pPerfilxTelaDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(PerfilxTelaDM));
        }
    }
}
