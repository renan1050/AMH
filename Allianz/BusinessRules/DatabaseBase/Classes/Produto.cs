using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class Produto
    {
        public static string gTabela = "produto";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<ProdutoDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(ProdutoDM)).Cast<ProdutoDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(ProdutoDM pProdutoDM, Action<string> pCarregar)
        {
            if (Database.Insert(gTabela, pProdutoDM))
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
        public ProdutoDM SelectCodigo(string pChave)
        {
            return (ProdutoDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(ProdutoDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(ProdutoDM pProdutoDM)
        {
            return Database.Update(gTabela, pProdutoDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(ProdutoDM));
        }
    }
}
