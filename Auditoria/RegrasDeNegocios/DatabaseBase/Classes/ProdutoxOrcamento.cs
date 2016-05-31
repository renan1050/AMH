using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class ProdutoxOrcamento
    {
        public static string gTabela = "produtoxorcamento";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<ProdutoxOrcamentoDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(ProdutoxOrcamentoDM)).Cast<ProdutoxOrcamentoDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(ProdutoxOrcamentoDM pProdutoxOrcamentoDM, Action<string> pCarregar = null)
        {
            if (Database.Insert(gTabela, pProdutoxOrcamentoDM))
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
        public ProdutoxOrcamentoDM SelectCodigo(string pChave)
        {
            return (ProdutoxOrcamentoDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(ProdutoxOrcamentoDM));
        }

        public List<ProdutoxOrcamentoDM> SelectPorOrcamento(string pChave)
        {
            return Database.SelecionarPorCampo(gTabela, pChave, "orcCodigo", typeof(ProdutoxOrcamentoDM)).Cast<ProdutoxOrcamentoDM>().ToList();
        }

        //salva ediçoes no cliente
        public bool EditarCliente(ProdutoxOrcamentoDM pProdutoxOrcamentoDM)
        {
            return Database.Update(gTabela, pProdutoxOrcamentoDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(ProdutoxOrcamentoDM));
        }
    }
}
