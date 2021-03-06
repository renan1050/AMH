﻿using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class Pessoa
    {
        public static string gTabela = "pessoa";

        public List<PessoaDM> SelecionarPorTipo(int pTipo)
        {
            return Database.SelecionarPorTipo(gTabela, typeof(PessoaDM), pTipo).Cast<PessoaDM>().ToList();
        }

        //seleciona todos dados no banco clientes e retorna um datatable
        public List<PessoaDM> AtualizarGrade(Dictionary<string, string> pParamentros)
        {
            return Database.SelecionarTudo(gTabela, pParamentros, typeof(PessoaDM)).Cast<PessoaDM>().ToList();
        }

        public List<PessoaDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(PessoaDM)).Cast<PessoaDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(PessoaDM pPessoaDM, Action<string> pCarregar)
        {
            if (Database.Insert(gTabela, pPessoaDM))
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
        public PessoaDM SelectCodigo(string pChave)
        {
            return (PessoaDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(PessoaDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(PessoaDM pPessoaDM, Action<string> pCarregar)
        {
            if(Database.Update(gTabela, pPessoaDM))
            {
                pCarregar(pPessoaDM.pesCodigo.ToString());
                return true;            
            }
            else
            {
                return false;  
            }
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(PessoaDM));
        }
    }

    public static class PessoaFeature
    {
        public static class TipoPessoa
        {
            public const int Cliente = 1;
            public const int Filial = 2;
            public const int Funcionario = 3;
        }
    }
}
