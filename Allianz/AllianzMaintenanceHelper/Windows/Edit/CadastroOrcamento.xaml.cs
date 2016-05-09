using BusinessRules.DatabaseBase.Classes;
using BusinessRules.DatabaseBase.Model;
using InterfaceBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AllianzMaintenanceHelper
{
    /// <summary>
    /// Interaction logic for CadastroOrcamento.xaml 
    /// </summary>
    public partial class CadastroOrcamento : Window
    {
        public CadastroOrcamento()
        {
            InitializeComponent();
            Pessoa lPessoa = new Pessoa();
            Veiculo lVeiculo = new Veiculo();
            List<PessoaDM> lPessoaDMList = lPessoa.SelecionarPorTipo(PessoaFeature.TipoPessoa.Cliente);
            pesCodigoC.ItemsSource = lPessoaDMList.ToDictionary(x => x.pesCodigo, x => x.pesNome);
            pesCodigoC.DisplayMemberPath = "Value";
            pesCodigoC.SelectedValuePath = "Key";

            List<VeiculoDM> lVeiculoDMList = lVeiculo.SelecionarTudo();
            veiCodigo.ItemsSource = lVeiculoDMList.ToDictionary(x => x.veiCodigo, x => x.veiPlaca);
            veiCodigo.DisplayMemberPath = "Value";
            veiCodigo.SelectedValuePath = "Key";
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.LoadByValue(((Button)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(),
                                             this,
                                             LoadOrcamento,
                                             txtCodigoCarregar.Text);
        }

        private void LoadOrcamento(string pCodigo)
        {
            Clear();
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Orcamento lOrcamento = new Orcamento();
            OrcamentoDM lOrcamentoDM = lOrcamento.SelectCodigo(pCodigo);
            lInterfaceManagement.CarregarDM(this, lOrcamentoDM);
            LoadSub(lOrcamentoDM.orcCodigo.ToString());
            txtCodigoCarregar.Text = null;
        }

        private void LoadSub(string pCodigo)
        {
            ClearSub();
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            ServicoxOrcamento lServicoxOrcamento = new ServicoxOrcamento();
            List<ServicoxOrcamentoDM> lServicoxOrcamentoDMList = lServicoxOrcamento.SelectPorOrcamento(pCodigo);
            dtItens.ItemsSource = lServicoxOrcamentoDMList;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Orcamento lOrcamento = new Orcamento();
            List<string> lErrosValidacao = new List<string>();
            OrcamentoDM lOrcamentoDM = (OrcamentoDM)lInterfaceManagement.BuildDM(this, typeof(OrcamentoDM), ((Button)sender).Name, lErrosValidacao);
            if (lErrosValidacao != null && lErrosValidacao.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, lErrosValidacao));
            }
            else
            {
                if (!string.IsNullOrEmpty(orcCodigo.Text))
                {
                    if (lOrcamento.EditarCliente(lOrcamentoDM))                    
                        MessageBox.Show("Editado com sucesso");                    
                    else
                        MessageBox.Show("Erro ao editar");
                }
                else
                {
                    if (lOrcamento.NovoCliente(lOrcamentoDM, LoadOrcamento))                    
                        MessageBox.Show("Salvo com sucesso");
                    else
                        MessageBox.Show("Erro ao salvar");
                }                
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Orcamento lOrcamento = new Orcamento();
            lOrcamento.ExcluirCliente(veiCodigo.Text);
            Clear();
        }

        private void Clear()
        {
            orcCodigo.Text = null;
            pesCodigoC.Text = null;
            veiCodigo.Text = null;            
            proCodigo.Text = null;
            genQuantidade.Text = null;
            genValorUnitario.Text = null;
            genValorTotal.Text = null;
            dtItens.ItemsSource = null;
             
        }

        private void ClearSub()
        {
            genCodigo.Text = null;
            serCodigo.SelectedValue = null;
            proCodigo.SelectedValue = null;
            genValorUnitario.Text = null;
            genQuantidade.Text = null;
            genValorTotal.Text = null;            
        }

        private void btnSalvarSub_Click(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            ServicoxOrcamento lServicoxOrcamento = new ServicoxOrcamento();
            List<string> lErrosValidacao = new List<string>();
            ServicoxOrcamentoDM lServicoxOrcamentoDM = (ServicoxOrcamentoDM)lInterfaceManagement.BuildDM(this, typeof(ServicoxOrcamentoDM), ((Button)sender).Name, lErrosValidacao);
            if (lErrosValidacao != null && lErrosValidacao.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, lErrosValidacao));
            }
            else
            {
                if (!string.IsNullOrEmpty(genCodigo.Text))
                {
                    if (lServicoxOrcamento.EditarCliente(lServicoxOrcamentoDM))
                        MessageBox.Show("Editado com sucesso");
                    else
                        MessageBox.Show("Erro ao editar");
                }
                else
                {
                    if (lServicoxOrcamento.NovoCliente(lServicoxOrcamentoDM))
                        MessageBox.Show("Salvo com sucesso");
                    else
                        MessageBox.Show("Erro ao salvar");
                }

                LoadSub(orcCodigo.Text);
            }
        }

        private void genValorUnitario_LostFocus(object sender, RoutedEventArgs e)
        {
            ChangeTotalValue();
        }

        private void genQuantidade_LostFocus(object sender, RoutedEventArgs e)
        {
            ChangeTotalValue();
        }      

        private void ChangeTotalValue()
        {
            decimal lValorUnitario; 
            int lQuantidade; 
            if(!decimal.TryParse(genValorUnitario.Text, out lValorUnitario))
               lValorUnitario = 0;
            
            if(!int.TryParse(genQuantidade.Text, out lQuantidade))
               lQuantidade = 0;

            genValorTotal.Text = ((decimal)(lQuantidade * lValorUnitario)).ToString();
        }
    }
}
