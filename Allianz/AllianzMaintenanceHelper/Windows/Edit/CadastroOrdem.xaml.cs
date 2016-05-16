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
    public partial class CadastroOrdem : Window
    {
        public CadastroOrdem()
        {
            InitializeComponent();
            gItens.Visibility = Visibility.Hidden;
            Pessoa lPessoa = new Pessoa();
            Veiculo lVeiculo = new Veiculo();
            Produto lProduto = new Produto();
            Servico lServico = new Servico();
            List<PessoaDM> lPessoaDMList = lPessoa.SelecionarPorTipo(PessoaFeature.TipoPessoa.Cliente);
            pesCodigoC.ItemsSource = lPessoaDMList.ToDictionary(x => x.pesCodigo, x => x.pesNome);
            pesCodigoC.DisplayMemberPath = "Value";
            pesCodigoC.SelectedValuePath = "Key";

            lPessoaDMList = lPessoa.SelecionarPorTipo(PessoaFeature.TipoPessoa.Funcionario);
            pesCodigoF.ItemsSource = lPessoaDMList.ToDictionary(x => x.pesCodigo, x => x.pesNome);
            pesCodigoF.DisplayMemberPath = "Value";
            pesCodigoF.SelectedValuePath = "Key";

            List<VeiculoDM> lVeiculoDMList = lVeiculo.SelecionarTudo();
            veiCodigo.ItemsSource = lVeiculoDMList.ToDictionary(x => x.veiCodigo, x => x.veiPlaca);
            veiCodigo.DisplayMemberPath = "Value";
            veiCodigo.SelectedValuePath = "Key";

            List<ProdutoDM> lProdutoDMList = lProduto.SelecionarTudo();
            List<InterfaceManagement.Item> lItemList = new List<InterfaceManagement.Item>();
            InterfaceManagement.Item lAddItem;
            foreach(ProdutoDM lItem in lProdutoDMList)
            {
                lAddItem = new InterfaceManagement.Item(lItem.proNome, lItem.proCodigo, lItem.proValorUnitario, "proCodigo");
                lItemList.Add(lAddItem);

            }
            
            List<ServicoDM> lServicoDMList = lServico.SelecionarTudo();
            foreach (ServicoDM lItem in lServicoDMList)
            {
                lAddItem = new InterfaceManagement.Item(lItem.serDescricao, lItem.serCodigo, lItem.serValor, "serCodigo");
                lItemList.Add(lAddItem);
            }    
        
            proCodigo.Items.Clear();
            proCodigo.ItemsSource = lItemList;
            proCodigo.DisplayMemberPath = "Name";
            proCodigo.SelectedValuePath = "Value";
        }        

        private void Load(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.LoadByValue(((Button)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(),
                                             this,
                                             LoadOrdem,
                                             txtCodigoCarregar.Text);
        }

        private void LoadOrdem(string pCodigo)
        {
            Clear();
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Ordem lOrdem = new Ordem();
            OrdemDM lOrdemDM = lOrdem.SelectCodigo(pCodigo);

            if (lInterfaceManagement.CarregarDM(this, lOrdemDM))
            {
                gItens.Visibility = Visibility.Visible;
                LoadSub(lOrdemDM.ordCodigo.ToString());
            }

            txtCodigoCarregar.Text = null;
        }

        private void LoadSub(string pCodigo)
        {
            ClearSub();
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            ServicoxOrdem lServicoxOrdem = new ServicoxOrdem();
            List<ServicoxOrdemDM> lServicoxOrdemDMList = lServicoxOrdem.SelectPorOrdem(pCodigo);
            dtItens.ItemsSource = lServicoxOrdemDMList;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Ordem lOrdem = new Ordem();
            List<string> lErrosValidacao = new List<string>();
            OrdemDM lOrdemDM = (OrdemDM)lInterfaceManagement.BuildDM(this, typeof(OrdemDM), ((Button)sender).Name, lErrosValidacao);
            if (lErrosValidacao != null && lErrosValidacao.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, lErrosValidacao));
            }
            else
            {
                if (!string.IsNullOrEmpty(ordCodigo.Text))
                {
                    if (lOrdem.EditarCliente(lOrdemDM))                    
                        MessageBox.Show("Editado com sucesso");                    
                    else
                        MessageBox.Show("Erro ao editar");
                }
                else
                {
                    lOrdemDM.ordDataEntrada = DateTime.Now;
                    if (lOrdem.NovoCliente(lOrdemDM, LoadOrdem))                    
                        MessageBox.Show("Salvo com sucesso");
                    else
                        MessageBox.Show("Erro ao salvar");
                }                
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Ordem lOrdem = new Ordem();
            lOrdem.ExcluirCliente(ordCodigo.Text);
            Clear();
        }

        private void Clear()
        {
            ordCodigo.Text = null;
            pesCodigoC.Text = null;
            pesCodigoF.Text = null;
            veiCodigo.Text = null;            
            proCodigo.Text = null;
            genQuantidade.Text = null;
            genValorUnitario.Text = null;
            genValorTotal.Text = null;
            dtItens.ItemsSource = null;
            gItens.Visibility = Visibility.Hidden; 
        }

        private void ClearSub()
        {
            genCodigo.Text = null;            
            proCodigo.SelectedValue = null;
            genValorUnitario.Text = null;
            genQuantidade.Text = null;
            genValorTotal.Text = null;            
        }

        private void btnSalvarSub_Click(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            ServicoxOrdem lServicoxOrdem = new ServicoxOrdem();
            List<string> lErrosValidacao = new List<string>();
            ServicoxOrdemDM lServicoxOrdemDM = (ServicoxOrdemDM)lInterfaceManagement.BuildDM(this, typeof(ServicoxOrdemDM), ((Button)sender).Name, lErrosValidacao);
            if (lErrosValidacao != null && lErrosValidacao.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, lErrosValidacao));
            }
            else
            {
                if (!string.IsNullOrEmpty(genCodigo.Text))
                {
                    if (lServicoxOrdem.EditarCliente(lServicoxOrdemDM))
                        MessageBox.Show("Editado com sucesso");
                    else
                        MessageBox.Show("Erro ao editar");
                }
                else
                {
                    if (lServicoxOrdem.NovoCliente(lServicoxOrdemDM))
                        MessageBox.Show("Salvo com sucesso");
                    else
                        MessageBox.Show("Erro ao salvar");
                }

                LoadSub(ordCodigo.Text);
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

        private void proCodigo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(proCodigo.SelectedItem != null)
                genValorUnitario.Text = (proCodigo.SelectedItem as InterfaceManagement.Item).Adicional.ToString();
        }
    }
}
