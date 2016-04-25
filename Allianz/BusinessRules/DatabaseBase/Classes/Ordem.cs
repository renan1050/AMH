﻿using BusinessRules.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class Ordem
    {
        public static string gTabela = "ordem";
        //seleciona todos dados no banco clientes e retorna um datatable
        public DataTable AtualizarGrade()
        {
            return Database.SelecionarTudo(gTabela);
        }

        public List<OrdemDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(OrdemDM)).Cast<OrdemDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(OrdemDM pOrdemDM)
        {
            return Database.Insert(gTabela, pOrdemDM);
        }

        //seleciona um cliente de acordo com seu codigo
        public OrdemDM SelectCodigo(string pChave)
        {
            return (OrdemDM)Database.SelecionarPorCodigo(gTabela, pChave, typeof(OrdemDM));
        }

        //salva ediçoes no cliente
        public bool EditarCliente(OrdemDM pOrdemDM)
        {
            return Database.Update(gTabela, pOrdemDM);
        }

        //exclui cliente com o codigo informado
        public bool ExcluirCliente(string pChave)
        {
            return Database.Delete(gTabela, pChave, typeof(OrdemDM));
        }
    }
}