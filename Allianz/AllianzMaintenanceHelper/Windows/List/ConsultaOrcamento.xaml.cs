using BusinessRules.DatabaseBase.Classes;
using BusinessRules.DatabaseBase.Model;
using InterfaceBase;
using System;
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
            Atualizar();
        }

        private void Atualizar()
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Orcamento lOrcamento = new Orcamento();
            Dictionary<string, string> lParametro = new Dictionary<string, string>();

            lParametro.Add(orcDataCriacao_Inicio.Name, orcDataCriacao_Inicio.Text);
            lParametro.Add(orcDataCriacao_Fim.Name, orcDataCriacao_Fim.Text);
            lParametro.Add(pesCodigoC.Name, (pesCodigoC.SelectedValue != null ? pesCodigoC.SelectedValue.ToString() : ""));

            dtRegistros.ItemsSource = lOrcamento.AtualizarGrade(lParametro);

        }
    }
}
