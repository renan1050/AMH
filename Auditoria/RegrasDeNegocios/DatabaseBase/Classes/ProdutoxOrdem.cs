using RegrasDeNegocios.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Classes
{
    public class ProdutoxOrdem
    {
        public static string gTabela = "produtoxordem";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<ProdutoxOrdemDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(ProdutoxOrdemDM)).Cast<ProdutoxOrdemDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(ProdutoxOrdemDM pProdutoxOrdemDM, Action<string> pCarregar = null)
        {
            if (Database.Insert(gTabela, pProdutoxOrdemDM))
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
        public ProdutoxOrdemDM SelectCodigo(string pChave)
        {
            return (ProdutoxOrdemDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(ProdutoxOrdemDM));
        }

        public List<ProdutoxOrdemDM> SelectPorOrdem(string pChave)
        {
            return Database.SelecionarPorCampo(gTabela, pChave, "ordCodigo", typeof(ProdutoxOrdemDM)).Cast<ProdutoxOrdemDM>().ToList();
        }

        //salva ediçoes no cliente
        public bool EditarCliente(ProdutoxOrdemDM pProdutoxOrdemDM)
        {
            return Database.Update(gTabela, pProdutoxOrdemDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(ProdutoxOrdemDM));
        }
    }
}
