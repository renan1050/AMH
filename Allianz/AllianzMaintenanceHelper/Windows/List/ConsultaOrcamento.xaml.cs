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

namespace AllianzMaintenanceHelper
{
    /// <summary>
    /// Interaction logic for ConsultaOrcamento.xaml
    /// </summary>
    public partial class ConsultaOrcamento : Window
    {
        public ConsultaOrcamento()
        {
            InitializeComponent();
            Pessoa lPessoa = new Pessoa();
            List<PessoaDM> lPessoaDMList = lPessoa.SelecionarPorTipo(PessoaFeature.TipoPessoa.Cliente);
            pesCodigoC.ItemsSource = lPessoaDMList.ToDictionary(x => x.pesCodigo, x => x.pesNome);
            pesCodigoC.DisplayMemberPath = "Value";
            pesCodigoC.SelectedValuePath = "Key";
            Atualizar(true);
        }

        private void Atualizar(bool pAbrindo = false)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Orcamento lOrcamento = new Orcamento();
            Dictionary<string, string> lParametro = new Dictionary<string, string>();

            lParametro.Add(orcDataCriacao_Inicio.Name, orcDataCriacao_Inicio.Text);
            lParametro.Add(orcDataCriacao_Fim.Name, orcDataCriacao_Fim.Text);
            lParametro.Add(pesCodigoC.Name, (pesCodigoC.SelectedValue != null ? pesCodigoC.SelectedValue.ToString() : ""));
            dtRegistros.ItemsSource = null;
            dtRegistros.ItemsSource = lOrcamento.AtualizarGrade(lParametro);
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

        private void pesCodigoC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Atualizar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CadastroOrcamento lCadastroOrcamento = new CadastroOrcamento();
            lCadastroOrcamento.Show();
        }
    }
}
