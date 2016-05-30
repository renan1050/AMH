using BusinessRules.DatabaseBase.Classes;
using BusinessRules.DatabaseBase.Model;
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

namespace AllianzMaintenanceHelper.Windows.List
{
    /// <summary>
    /// Interaction logic for ConsultaServico.xaml
    /// </summary>
    public partial class ConsultaServico : Window
    {
        public ConsultaServico()
        {
            InitializeComponent();
            Atualizar(true);
            
        }

        private void Atualizar(bool pAbrindo = false)
        {
            try
            {
                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                Servico lServico = new Servico();
                Dictionary<string, string> lParametro = new Dictionary<string, string>();

                lParametro.Add(serDescricao.Name, serDescricao.Text);
                dtRegistros.ItemsSource = null;
                dtRegistros.ItemsSource = lServico.AtualizarGrade(lParametro);
                dtRegistros.AutoGenerateColumns = false;
                dtRegistros.AutoGenerateColumns = true;

                if (!pAbrindo)
                {
                    int lCount = 0;
                    FormatedName lAtributo;
                    List<int> lRemover = new List<int>();
                    foreach (PropertyInfo lProperty in typeof(OrcamentoDM).GetProperties())
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
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void serDescricao_LostFocus(object sender, RoutedEventArgs e)
        {
            Atualizar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CadastroServico lCadastroServico = new CadastroServico();
            lCadastroServico.Show();
        }
    }
}
