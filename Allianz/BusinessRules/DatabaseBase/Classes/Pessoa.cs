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
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<PessoaDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(PessoaDM)).Cast<PessoaDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(PessoaDM pPessoaDM)
        {
            return Database.Insert(gTabela, pPessoaDM);
        }

        //seleciona um cliente de acordo com seu codigo
        public PessoaDM SelectCodigo(string pChave)
        {
            return (PessoaDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(PessoaDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(PessoaDM pPessoaDM)
        {
            return Database.Update(gTabela, pPessoaDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(PessoaDM));
        }
    }
}