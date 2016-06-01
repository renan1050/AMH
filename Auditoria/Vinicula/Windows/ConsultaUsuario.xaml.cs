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
    public partial class ConsultaUsuario : Window
    {
        public ConsultaUsuario()
        {
            InitializeComponent();
            Perfil lPerfil = new Perfil();
            List<PerfilDM> lPerfilDMList = lPerfil.SelecionarTudo();
            perCodigo.ItemsSource = lPerfilDMList.ToDictionary(x => x.perCodigo, x => x.perNome);
            perCodigo.DisplayMemberPath = "Value";
            perCodigo.SelectedValuePath = "Key";
            Atualizar(true);
            Logs.Log("ConsultaLogs", "Abrir");
        }

        private void Atualizar(bool pAbrindo = false)
        {
            try
            {
                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                Usuario lUsuario = new Usuario();
                Dictionary<string, string> lParametro = new Dictionary<string, string>();

                lParametro.Add(perCodigo.Name, (perCodigo.SelectedValue != null ? perCodigo.SelectedValue.ToString() : ""));
                dtRegistros.ItemsSource = null;
                dtRegistros.ItemsSource = lUsuario.AtualizarGrade(lParametro);
                dtRegistros.AutoGenerateColumns = false;
                dtRegistros.AutoGenerateColumns = true;

                if (!pAbrindo)
                {
                    int lCount = 0;
                    FormatedName lAtributo;
                    List<int> lRemover = new List<int>();
                    foreach (PropertyInfo lProperty in typeof(UsuarioVM).GetProperties())
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
                Logs.Log("ConsultaUsuario", "Atualizar");
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
