﻿using RegrasDeNegocios.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Classes
{
    public class Ordem
    {
        public static string gTabela = "ordem";
        //seleciona todos dados no banco clientes e retorna um datatable
        public List<OrdemDM> AtualizarGrade(Dictionary<string, string> pParamentros)
        {
            return Database.SelecionarTudo(gTabela, pParamentros, typeof(OrdemDM)).Cast<OrdemDM>().ToList();
        }

        public List<OrdemDM> SelecionarTudo()
        {
            return Database.SelecionarTudo(gTabela, typeof(OrdemDM)).Cast<OrdemDM>().ToList();
        }

        //insere novo cliente
        public bool NovoCliente(OrdemDM pOrdemDM, Action<string> pCarregar)
        {
            if (Database.Insert(gTabela, pOrdemDM))
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
