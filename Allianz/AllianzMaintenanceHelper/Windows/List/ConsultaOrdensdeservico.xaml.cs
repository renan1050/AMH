using System;
using InterfaceBase;
using System.Collections.Generic;
using System.Linq;
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
using BusinessRules.DatabaseBase.Classes;
using BusinessRules.DatabaseBase.Model;
using System.Reflection;

namespace AllianzMaintenanceHelper
{
    /// <summary>
    /// Interaction logic for ConsultarOrdens_de_servico.xaml
    /// </summary>
    public partial class ConsultarOrdens_de_servico : Window
    {
        public ConsultarOrdens_de_servico()
        {
            InitializeComponent();
            Pessoa lPessoa = new Pessoa();
            List<PessoaDM> lPessoaDMList = lPessoa.SelecionarPorTipo(PessoaFeature.TipoPessoa.Cliente);
            pesCodigoC.ItemsSource = lPessoaDMList.ToDictionary(x => x.pesCodigo, x => x.pesNome);
            pesCodigoC.DisplayMemberPath = "Value";
            pesCodigoC.SelectedValuePath = "Key";

            Veiculo lVeiculo = new Veiculo();
            List<VeiculoDM> lVeiculoDMList = lVeiculo.SelecionarTudo();
            pesCodigoC.ItemsSource = lVeiculoDMList.ToDictionary(x => x.veiCodigo, x => x.veiPlaca);
            pesCodigoC.DisplayMemberPath = "Value";
            pesCodigoC.SelectedValuePath = "Key";

            Atualizar(true);
            
        }


        
        private void Atualizar(bool pAbrindo = false)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Ordem lOrdem = new Ordem();
            Dictionary<string, string> lParametro = new Dictionary<string, string>();

            lParametro.Add(pesCodigoC.Name, pesCodigoC.Text);
            lParametro.Add(veiCodigo.Name, veiCodigo.Text);

            lParametro.Add(ordDataEntrada_Inicio.Name, ordDataEntrada_Inicio.Text);
            lParametro.Add(ordDataEntrada_Fim.Name, ordDataEntrada_Fim.Text);

            lParametro.Add(ordDataSaida_Inicio.Name, ordDataSaida_Inicio.Text);
            lParametro.Add(ordDataSaida_Fim.Name, ordDataSaida_Fim.Text);
            dtRegistros.ItemsSource = null;
            dtRegistros.ItemsSource = lOrdem.AtualizarGrade(lParametro);
            dtRegistros.AutoGenerateColumns = false;
            dtRegistros.AutoGenerateColumns = true;

            if (!pAbrindo)
            {
                int lCount = 0;
                FormatedName lAtributo;
                List<int> lRemover = new List<int>();
                foreach (PropertyInfo lProperty in typeof(ServicoxOrdemDM).GetProperties())
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

        }

        private void Event_LostFocus(object sender, RoutedEventArgs e)
        {
            Atualizar();
        }

        private void Event_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Atualizar();
        }

    }
}
