using RegrasDeNegocios.DatabaseBase.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RegrasDeNegocios.DatabaseBase.Classes
{
    public class Logs
    {
        private static string gTabela = "logs";

        public List<LogsVM> AtualizarGrade(Dictionary<string, string> pParamentros)
        {
            return Database.SelecionarTudoJoin(gTabela, pParamentros, typeof(LogsVM)).Cast<LogsVM>().ToList();
        }

        public static bool Log(string pClasse, string pMetodo)
        {
            LogsDM lLogsDM = new LogsDM();
            lLogsDM.logClasse = pClasse;
            lLogsDM.logMetodo = pMetodo;
            lLogsDM.logDiaHora = DateTime.Now.ToString();
            lLogsDM.usuCodigo = Database.getUsuario().usuCodigo;

            return Database.Insert(gTabela, lLogsDM);            
        }
    }
}
