using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.IO;

namespace BusinessRules.DatabaseBase
{
   public class DatabaseConnection
    {
       private String gUsuario;
        private String gSenha;
        private String gHost;
        private String gBanco;
        private String gTipoServidor;

        private static DatabaseConnection gInstance = new DatabaseConnection();

        public static DatabaseConnection getInstance()
        {
            return gInstance;
        }

        private DatabaseConnection()
        {
            //Cria uma instância de um documento XML
            XmlDocument lXML = new XmlDocument();

            //Define o caminho do arquivo XML 
            string lXMLFile = @"C:\Users\0040481411023\AMH\Allianz\AllianzMaintenanceHelper\DataBase.xml";

            //carrega o arquivo XML
            lXML.Load(lXMLFile);

            //Lê o filho de um Nó Pai específico 
            gSenha = lXML.SelectSingleNode("bancodedados").ChildNodes[1].InnerText;
            gUsuario = lXML.SelectSingleNode("bancodedados").ChildNodes[0].InnerText;
            gHost = lXML.SelectSingleNode("bancodedados").ChildNodes[2].InnerText;
            gBanco = lXML.SelectSingleNode("bancodedados").ChildNodes[3].InnerText;
            gTipoServidor = lXML.SelectSingleNode("bancodedados").ChildNodes[4].InnerText;   
        }
        public MySqlConnection getConnection()
        {
            MySqlConnection MyCon = new MySqlConnection("server=" + gHost + "; Database = " + gBanco + "; user id= " + gUsuario + "; password= " + gSenha + "; pooling=false;");
            
            return MyCon;
        }
    }
}
