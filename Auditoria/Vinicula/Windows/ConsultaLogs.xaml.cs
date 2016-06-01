using RegrasDeNegocios.DatabaseBase.Classes;
using RegrasDeNegocios.DatabaseBase.Model;
using InterfaceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Vinicula
{
    /// <summary>
    /// Interaction logic for ConsultaOrcamento.xaml
    /// </summary>
    public partial class ConsultaLogs : Window
    {
        public ConsultaLogs()
        {
            InitializeComponent();
            Usuario lUsuario = new Usuario();
            List<UsuarioDM> lUsuarioDMList = lUsuario.SelecionarTudo();
            usuCodigo.ItemsSource = lUsuarioDMList.ToDictionary(x => x.usuCodigo, x => x.usuNome);
            usuCodigo.DisplayMemberPath = "Value";
            usuCodigo.SelectedValuePath = "Key";
            Atualizar(true);
            Logs.Log("ConsultaLogs", "Abrir");
        }

        private void Atualizar(bool pAbrindo = false)
        {
            try
            {
                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                Logs lLogs = new Logs();
                Dictionary<string, string> lParametro = new Dictionary<string, string>();

                lParametro.Add(usuCodigo.Name, (usuCodigo.SelectedValue != null ? usuCodigo.SelectedValue.ToString() : ""));
                dtRegistros.ItemsSource = null;
                dtRegistros.ItemsSource = lLogs.AtualizarGrade(lParametro);
                dtRegistros.AutoGenerateColumns = false;
                dtRegistros.AutoGenerateColumns = true;

                if (!pAbrindo)
                {
                    int lCount = 0;
                    FormatedName lAtributo;
                    List<int> lRemover = new List<int>();
                    foreach (PropertyInfo lProperty in typeof(LogsVM).GetProperties())
                    {
                        lAtributo = lProperty.GetCustomAttributes(typeof(FormatedName), false).Cast<FormatedName>().FirstOrDefault();
                        if (lAtributo != null)
                        {
                            dtRegistros.Columns[lCount].Header = lAtributo.Name;
                            dtRegistros.Columns[lCount].IsReadOnly = true;
                            lCount++;
                        }
                        else
                        {
                            dtRegistros.Columns.RemoveAt(lCount);
                        }

                    }
                }

                dtRegistros.CanUserAddRows = false;
                Logs.Log("ConsultaLogs", "Atualizar");
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }

        }

        private void usuCodigo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Atualizar();
        }
    }
}
