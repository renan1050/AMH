using RegrasDeNegocios.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Classes
{
    public class PessoaxTipo
    {
        public static string gTabela = "pessoaxtipo";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<PessoaxTipoDM> SelecionarPorCliente(int? pPesCodigo)
        {
            return Database.SelecionarPorCliente(gTabela, typeof(PessoaxTipoDM),  pPesCodigo).Cast<PessoaxTipoDM>().ToList();
        }

        public List<PessoaxTipoDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(PessoaxTipoDM)).Cast<PessoaxTipoDM>().ToList();
        }

        public bool Refresh(int? pPesCodigo, string[] pTipCodigo)
        {
            List<PessoaxTipoDM> lPessoaxTipoDMList = Database.SelecionarPorCliente(gTabela, typeof(PessoaxTipoDM), pPesCodigo).Cast<PessoaxTipoDM>().ToList();
            if (lPessoaxTipoDMList.Count == 0 || Database.Delete(gTabela, lPessoaxTipoDMList.Select(x => x.genCodigo.ToString()).ToArray(), typeof(PessoaxTipoDM)))
            {
                return Salvar(pPesCodigo, pTipCodigo);
            }
            else
            {
                return false;
            }
        }

        //insere novo cliente
        public bool Salvar(int? pPesCodigo, string[] pTipCodigo, Action<string> pCarregar = null)
        {
            PessoaxTipoDM lPessoaxTipoDM;
            foreach (string lTipo in pTipCodigo)
            {
                lPessoaxTipoDM = new PessoaxTipoDM();
                lPessoaxTipoDM.pesCodigo = pPesCodigo;
                lPessoaxTipoDM.tipCodigo = int.Parse(lTipo);
                if (!Database.Insert(gTabela, lPessoaxTipoDM))
                {                    
                    return false;
                }
            }

            if (pCarregar != null)
                pCarregar(Database.SelecionarUltimoId(gTabela));

            return true;
        }

        //seleciona um cliente de acordo com seu codigo
        public PessoaxTipoDM SelectCodigo(string pChave)
        {
            return (PessoaxTipoDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(PessoaxTipoDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(PessoaxTipoDM pPessoaxTipoDM)
        {
            return Database.Update(gTabela, pPessoaxTipoDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(PessoaxTipoDM));
        }
    }
}
