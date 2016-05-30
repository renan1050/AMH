using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BusinessRules.DatabaseBase.Classes
{
    public class Database
    {
        public static DataTable SelecionarTudo(string pTabela)
        {
            try
            {
                MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();

                string lSelect = "SELECT * FROM " + pTabela;

                MySqlDataAdapter lAdapter = new MySqlDataAdapter(lSelect, lConnection);

                DataTable lTable = new DataTable();

                lAdapter.Fill(lTable);

                return lTable;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no servidor, contate o administrador do sistema");
                return null;
            }
        }

        public static List<object> SelecionarTudo(string pTabela, Dictionary<string, string> pParametros, Type pTipoObjeto)
        {
            try
            {
                MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();
                lConnection.Open();

                List<object> lRetornos = new List<object>();
                string lSelect = "SELECT * FROM " + pTabela + " WHERE ";
                bool lFirst = true;
                foreach (KeyValuePair<string, string> lParametro in pParametros)
                {
                    if (!lFirst)
                        lSelect = string.Concat(lSelect, " AND ");
                    else
                        lFirst = false;
                    if (lParametro.Key.Contains("_"))
                    {
                        string[] lDate = lParametro.Key.Split('_');
                        if (lDate[1] == "Inicio")
                            lSelect = string.Concat(lSelect, " ('", lParametro.Value, "' = '' OR (STR_TO_DATE(", lDate[0], ", '%Y-%c-%e %T') ", " >= STR_TO_DATE(", (string.IsNullOrEmpty(lParametro.Value) ? "''" : lParametro.Value), ", '%Y-%c-%e %T')))");
                        else
                            lSelect = string.Concat(lSelect, " ('", lParametro.Value, "' = '' OR (STR_TO_DATE(", lDate[0], ", '%Y-%c-%e %T') ", " <= STR_TO_DATE(", (string.IsNullOrEmpty(lParametro.Value) ? "''" : lParametro.Value), ", '%Y-%c-%e %T')))");
                    }
                    else
                        lSelect = string.Concat(lSelect, " (", lParametro.Key, " LIKE '%", lParametro.Value, "%' OR '", lParametro.Value, "' = '')");
                }


                MySqlCommand lCommand = new MySqlCommand(lSelect, lConnection);

                MySqlDataReader lReader = lCommand.ExecuteReader();

                var lPropriedades = pTipoObjeto.GetProperties();

                object lRetorno;

                while (lReader.Read())
                {
                    lRetorno = Activator.CreateInstance(pTipoObjeto);

                    foreach (var lPropriedade in lPropriedades)
                    {
                        lPropriedade.SetValue(lRetorno, lReader[lPropriedade.Name] is DBNull ? null : ChangeType(lReader[lPropriedade.Name], lPropriedade.PropertyType));
                    }

                    lRetornos.Add(lRetorno);
                }

                lConnection.Close();

                return lRetornos;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no servidor, contate o administrador do sistema");
                return null;
            }
        }

        public static string SelecionarUltimoId(string pTabela)
        {
            try
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
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no servidor, contate o administrador do sistema");
                return null;
            }
        }

        public static List<object> SelecionarTudo(string pTabela, Type pTipoObjeto)
        {
            try
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
                        lPropriedade.SetValue(lRetorno, lReader[lPropriedade.Name] is DBNull ? null : ChangeType(lReader[lPropriedade.Name], lPropriedade.PropertyType));
                    }

                    lRetornos.Add(lRetorno);
                }

                lConnection.Close();

                return lRetornos;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no servidor, contate o administrador do sistema");
                return null;
            }
        }

        public static bool Insert(string pTabela, object pRegistro)
        {
            try
            {
                MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();
                lConnection.Open();

                string lInsert = "Insert into " + pTabela + " (";

                var lPropriedades = pRegistro.GetType().GetProperties();

                foreach (var lPropriedade in lPropriedades)
                {
                    if (!lPropriedade.Name.Equals(lPropriedades.First().Name))
                    {
                        lInsert = string.Concat(lInsert, lPropriedade.Name, (lPropriedade.Name.Equals(lPropriedades.Last().Name) ? ") values (" : ", "));
                    }
                }

                foreach (var lPropriedade in lPropriedades)
                {
                    if (!lPropriedade.Name.Equals(lPropriedades.First().Name))
                    {
                        lInsert = string.Concat(lInsert, "@", lPropriedade.Name, (lPropriedade.Name.Equals(lPropriedades.Last().Name) ? ")" : ", "));
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
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no servidor, contate o administrador do sistema");
                return false;
            }
        }

        public static object SelecionarPorCodigo(string pTabela, string pCodigo, Type pTipoObjeto)
        {
            try
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
                        lPropriedade.SetValue(lRetorno, lReader[lPropriedade.Name] is DBNull ? null : ChangeType(lReader[lPropriedade.Name], lPropriedade.PropertyType));
                    }
                }

                lConnection.Close();

                return lRetorno;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no servidor, contate o administrador do sistema");
                return null;
            }
        }

        public static List<object> SelecionarPorCampo(string pTabela, string pCodigo, string pCampo, Type pTipoObjeto)
        {
            try
            {
                MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();
                List<object> lRetornos = new List<object>();
                lConnection.Open();
                string lSelect = "SELECT * FROM " + pTabela + " WHERE " + pCampo + " = " + pCodigo;

                MySqlCommand lCommand = new MySqlCommand(lSelect, lConnection);

                MySqlDataReader lReader = lCommand.ExecuteReader();

                object lRetorno = null;

                while (lReader.Read())
                {
                    lRetorno = Activator.CreateInstance(pTipoObjeto);
                    var lPropriedades = pTipoObjeto.GetProperties();
                    foreach (var lPropriedade in lPropriedades)
                    {
                        lPropriedade.SetValue(lRetorno, lReader[lPropriedade.Name] is DBNull ? null : ChangeType(lReader[lPropriedade.Name], lPropriedade.PropertyType));
                    }

                    lRetornos.Add(lRetorno);
                }

                lConnection.Close();

                return lRetornos;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no servidor, contate o administrador do sistema");
                return null;
            }
        }

        public static bool Update(string pTabela, object pRegistro)
        {
            try
            {
                MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();
                lConnection.Open();

                string lUpdate = "Update " + pTabela + " set ";

                var lPropriedades = pRegistro.GetType().GetProperties();

                foreach (var lPropriedade in lPropriedades)
                {
                    if (!lPropriedade.Name.Equals(lPropriedades.First().Name))
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
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no servidor, contate o administrador do sistema");
                return false;
            }
        }

        public static bool Delete(string pTabela, string pCodigo, Type pTipoObjeto)
        {
            try
            {
                var lDialogResult = MessageBox.Show("Tem certeza que deseja excluir?", "Confirmação", MessageBoxButton.YesNo);
                if (lDialogResult == MessageBoxResult.Yes)
                {
                    if (string.IsNullOrEmpty(pCodigo))
                    {
                        MessageBox.Show("Carregue um registro para excluir");
                        return false;
                    }

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
                else
                    return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no servidor, contate o administrador do sistema");
                return false;
            }
        }

        public static bool Delete(string pTabela, string[] pCodigo, Type pTipoObjeto)
        {
            try
            {
                MySqlConnection lConnection = DatabaseConnection.getInstance().getConnection();
                lConnection.Open();

                var lPropriedades = pTipoObjeto.GetProperties();

                string lDelete = "Delete from " + pTabela + " where " + lPropriedades.First().Name + " IN (" + string.Join(",", pCodigo) + ")";

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
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no servidor, contate o administrador do sistema");
                return false;
            }
        }

        public static List<object> SelecionarPorCliente(string pTabela, Type pTipoObjeto, int? pPesCodigo)
        {
            try
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
                        lPropriedade.SetValue(lRetorno, lReader[lPropriedade.Name] is DBNull ? null : ChangeType(lReader[lPropriedade.Name], lPropriedade.PropertyType));
                    }

                    lRetornos.Add(lRetorno);
                }

                lConnection.Close();

                return lRetornos;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no servidor, contate o administrador do sistema");
                return null;
            }
        }

        public static List<object> SelecionarPorTipo(string pTabela, Type pTipoObjeto, int? pTipo)
        {
            try
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
                        lPropriedade.SetValue(lRetorno, Convert.IsDBNull(lReader[lPropriedade.Name]) ? null : ChangeType(lReader[lPropriedade.Name], lPropriedade.PropertyType));
                    }

                    lRetornos.Add(lRetorno);
                }

                lConnection.Close();

                return lRetornos;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no servidor, contate o administrador do sistema");
                return null;
            }
        }

        public static object ChangeType(object value, Type conversion)
        {
            try
            {
                var t = conversion;

                if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    if (value == null)
                    {
                        return null;
                    }

                    t = Nullable.GetUnderlyingType(t);
                }

                return Convert.ChangeType(value, t);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no servidor, contate o administrador do sistema");
                return null;
            }
        }

    }
}
