﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.DatabaseBase.Classes
{
    public class Database
    {   
        public static DataTable SelecionarTudo(string pTabela)
        {
            MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();

            string lSelect = "SELECT * FROM " + pTabela;

            MySqlDataAdapter lAdapter = new MySqlDataAdapter(lSelect, lConnection);

            DataTable lTable = new DataTable();

            lAdapter.Fill(lTable);

            return lTable;
        }

        public static string SelecionarUltimoId(string pTabela)
        {
            string Id = null;
            MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();
            lConnection.Open();
            string lSelect = "SELECT * from " + pTabela + " order by 1 desc limit 1";

            MySqlCommand lCommand = new MySqlCommand(lSelect, lConnection);

            MySqlDataReader lReader = lCommand.ExecuteReader();

            while (lReader.Read())
            {
                Id = lReader[0].ToString();                    
            }

            lConnection.Close();

            return Id;
            
        }

        public static List<object> SelecionarTudo(string pTabela,Type pTipoObjeto)
        {
            List<object> lRetornos = new List<object>();
            MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();
            lConnection.Open();
            string lSelect = "SELECT * FROM " + pTabela;

            MySqlCommand lCommand = new MySqlCommand(lSelect, lConnection);

            MySqlDataReader lReader = lCommand.ExecuteReader();

            var lPropriedades = pTipoObjeto.GetProperties();

            object lRetorno;

            while (lReader.Read())
            {
                lRetorno = Activator.CreateInstance(pTipoObjeto);

                foreach (var lPropriedade in lPropriedades)
                {  
                    lPropriedade.SetValue(lRetorno, Convert.ChangeType(lReader[lPropriedade.Name], lPropriedade.PropertyType));    
                }

                lRetornos.Add(lRetorno);
            }

            lConnection.Close();

            return lRetornos;
        }        

        public static bool Insert(string pTabela, object pRegistro)
        {
            MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();
            lConnection.Open();

            string lInsert = "Insert into " + pTabela + " (";

            var lPropriedades = pRegistro.GetType().GetProperties();

            foreach (var lPropriedade in lPropriedades)
            {   
                if(!lPropriedade.Name.Equals(lPropriedades.First().Name))
                {
                   lInsert = string.Concat(lInsert,lPropriedade.Name,(lPropriedade.Name.Equals(lPropriedades.Last().Name) ? ") values (" : ", "));
                }                
            }

            foreach (var lPropriedade in lPropriedades)
            {   
                if(!lPropriedade.Name.Equals(lPropriedades.First().Name))
                {
                   lInsert = string.Concat(lInsert,"@",lPropriedade.Name,(lPropriedade.Name.Equals(lPropriedades.Last().Name) ? ")" : ", "));
                }                
            }

            MySqlCommand lCommand = new MySqlCommand(lInsert, lConnection);

            foreach (var lPropriedade in lPropriedades)
            {
                if (!lPropriedade.Name.Equals(lPropriedades.First().Name))
                {
                    lCommand.Parameters.AddWithValue("@" + lPropriedade.Name, lPropriedade.GetValue(pRegistro));                    
                }
            }
            string query = lCommand.CommandText;
                        
            if (lCommand.ExecuteNonQuery() > 0)
            {
                lConnection.Close();
                return true;
            }
            else
            {
                lConnection.Close();
                return false;
            }
        }

        public static object SelecionarPorCodigo(string pTabela, string pCodigo, Type pTipoObjeto)
        {   
            MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();
            var lPropriedades = pTipoObjeto.GetProperties();
            lConnection.Open();
            string lSelect = "SELECT * FROM " + pTabela + " WHERE " + lPropriedades.First().Name + " = " + pCodigo;

            MySqlCommand lCommand = new MySqlCommand(lSelect, lConnection);

            MySqlDataReader lReader = lCommand.ExecuteReader();
            
            object lRetorno = null;

            while (lReader.Read())
            {
                lRetorno = Activator.CreateInstance(pTipoObjeto);

                foreach (var lPropriedade in lPropriedades)
                {                    
                    lPropriedade.SetValue(lRetorno, Convert.ChangeType(lReader[lPropriedade.Name], lPropriedade.PropertyType));
                }
            }

            lConnection.Close();

            return lRetorno;
        }    

        public static bool Update(string pTabela, object pRegistro)
        {
            MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();
            lConnection.Open();

            string lUpdate = "Update " + pTabela + " set ";

            var lPropriedades = pRegistro.GetType().GetProperties();

            foreach (var lPropriedade in lPropriedades)
            {   
                if(!lPropriedade.Name.Equals(lPropriedades.First().Name))
                {
                    lUpdate = string.Concat(lUpdate, lPropriedade.Name, " = @", lPropriedade.Name, (lPropriedade.Name.Equals(lPropriedades.Last().Name) ? "" : ", "));
                }                
            }

            lUpdate = lUpdate + " where " + lPropriedades.First().Name + " = " + lPropriedades.First().GetValue(pRegistro);


            MySqlCommand lCommand = new MySqlCommand(lUpdate, lConnection);

            foreach (var lPropriedade in lPropriedades)
            {
                if (!lPropriedade.Name.Equals(lPropriedades.First().Name))
                {
                    lCommand.Parameters.AddWithValue("@" + lPropriedade.Name, lPropriedade.GetValue(pRegistro));                    
                }
            }

            if (lCommand.ExecuteNonQuery() > 0)
            {
                lConnection.Close();
                return true;
            }
            else
            {
                lConnection.Close();
                return false;
            }
        }

        public static bool Delete(string pTabela, string pCodigo, Type pTipoObjeto)
        {
            MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();
            lConnection.Open();

            var lPropriedades = pTipoObjeto.GetProperties();

            string lDelete = "Delete from " + pTabela + " where " + lPropriedades.First().Name + " = " + pCodigo;
                  
            MySqlCommand lCommand = new MySqlCommand(lDelete, lConnection);
                        
            if (lCommand.ExecuteNonQuery() > 0)
            {
                lConnection.Close();
                return true;
            }
            else
            {
                lConnection.Close();
                return false;
            }
        }

        public static List<object> SelecionarPorCliente(string pTabela, Type pTipoObjeto, int pPesCodigo)
        {
            List<object> lRetornos = new List<object>();
            MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();
            lConnection.Open();
            string lSelect = "SELECT * FROM " + pTabela + " WHERE pessoaxtipo.pesCodigo = " + pPesCodigo;

            MySqlCommand lCommand = new MySqlCommand(lSelect, lConnection);

            MySqlDataReader lReader = lCommand.ExecuteReader();

            var lPropriedades = pTipoObjeto.GetProperties();

            object lRetorno;

            while (lReader.Read())
            {
                lRetorno = Activator.CreateInstance(pTipoObjeto);

                foreach (var lPropriedade in lPropriedades)
                {
                    lPropriedade.SetValue(lRetorno, Convert.ChangeType(lReader[lPropriedade.Name], lPropriedade.PropertyType));
                }

                lRetornos.Add(lRetorno);
            }

            lConnection.Close();

            return lRetornos;
        }            

        public static List<object> SelecionarPorTipo(string pTabela, Type pTipoObjeto, int pTipo)
        {
            List<object> lRetornos = new List<object>();
            MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();
            lConnection.Open();
            string lSelect = "SELECT * FROM " + pTabela + " INNER JOIN pessoaxtipo ON " + pTabela + ".pesCodigo = pessoaxtipo.pesCodigo WHERE pessoaxtipo.tipCodigo = " + pTipo;

            MySqlCommand lCommand = new MySqlCommand(lSelect, lConnection);

            MySqlDataReader lReader = lCommand.ExecuteReader();

            var lPropriedades = pTipoObjeto.GetProperties();

            object lRetorno;

            while (lReader.Read())
            {
                lRetorno = Activator.CreateInstance(pTipoObjeto);

                foreach (var lPropriedade in lPropriedades)
                {
                    lPropriedade.SetValue(lRetorno, Convert.ChangeType(lReader[lPropriedade.Name], lPropriedade.PropertyType));
                }

                lRetornos.Add(lRetorno);
            }

            lConnection.Close();

            return lRetornos;
        }    

    }
}
