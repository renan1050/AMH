using BusinessRules.DatabaseBase.Classes;
using BusinessRules.DatabaseBase.Model;
using InterfaceBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            gItens.Visibility = Visibility.Hidden;
            Pessoa lPessoa = new Pessoa();
            Veiculo lVeiculo = new Veiculo();
            Produto lProduto = new Produto();
            Servico lServico = new Servico();
            List<PessoaDM> lPessoaDMList = lPessoa.SelecionarPorTipo(PessoaFeature.TipoPessoa.Cliente);
            pesCodigoC.ItemsSource = lPessoaDMList.ToDictionary(x => x.pesCodigo, x => x.pesNome);
            pesCodigoC.DisplayMemberPath = "Value";
            pesCodigoC.SelectedValuePath = "Key";

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
                                             LoadOrcamento,
                                             txtCodigoCarregar.Text);
        }

        private void LoadOrcamento(string pCodigo)
        {
            Clear();
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Orcamento lOrcamento = new Orcamento();
            OrcamentoDM lOrcamentoDM = lOrcamento.SelectCodigo(pCodigo);

            if (lInterfaceManagement.CarregarDM(this, lOrcamentoDM))
            {
                gItens.Visibility = Visibility.Visible;
                LoadSub(lOrcamentoDM.orcCodigo.ToString());
            }

            txtCodigoCarregar.Text = null;
        }

        private void LoadSub(string pCodigo)
        {
            ClearSub();
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            ServicoxOrcamento lServicoxOrcamento = new ServicoxOrcamento();
            List<ServicoxOrcamentoDM> lServicoxOrcamentoDMList = lServicoxOrcamento.SelectPorOrcamento(pCodigo);
            dtItens.ItemsSource = lServicoxOrcamentoDMList;
            int lCount = 0;
            FormatedName lAtributo;
            List<int> lRemover = new List<int>();
            foreach (PropertyInfo lProperty in typeof(ServicoxOrcamentoDM).GetProperties())
            {
                lAtributo = lProperty.GetCustomAttributes(typeof(FormatedName), false).Cast<FormatedName>().FirstOrDefault();
                if (lAtributo != null)
                {
                    dtItens.Columns[lCount].Header = lAtributo.Name;
                    dtItens.Columns[lCount].IsReadOnly = true;
                }
                else
                    lRemover.Add(lCount);
                lCount++;
            }

            foreach (int lIndex in lRemover)
                dtItens.Columns.RemoveAt(lIndex);
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
                    lOrcamentoDM.orcDataCriacao = DateTime.Now;
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
            lOrcamento.ExcluirCliente(orcCodigo.Text);
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

        private void proCodigo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(proCodigo.SelectedItem != null)
                genValorUnitario.Text = (proCodigo.SelectedItem as InterfaceManagement.Item).Adicional.ToString();
        }

        private void dtItens_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((sender as DataGrid).SelectedItem != null)
            {
                ServicoxOrcamento lServicoxOrcamento = new ServicoxOrcamento();
                if (lServicoxOrcamento.ExcluirCliente(((sender as DataGrid).SelectedItem as ServicoxOrcamentoDM).genCodigo.ToString()))
                {
                    LoadSub(orcCodigo.Text);
                }
            }
        }
    }
}
