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
    /// Interaction logic for CadastroOrcamento.xaml
    /// </summary>
    public partial class CadastroOrcamento : Window
    {
        public CadastroOrcamento()
        {
            InitializeComponent();
            gItens.Visibility = Visibility.Hidden;
            Pessoa lPessoa = new Pessoa();            
            Produto lProduto = new Produto();            
            List<PessoaDM> lPessoaDMList = lPessoa.SelecionarPorTipo(PessoaFeature.TipoPessoa.Cliente);
            pesCodigoC.ItemsSource = lPessoaDMList.ToDictionary(x => x.pesCodigo, x => x.pesNome);
            pesCodigoC.DisplayMemberPath = "Value";
            pesCodigoC.SelectedValuePath = "Key";

            List<ProdutoDM> lProdutoDMList = lProduto.SelecionarTudo();
            List<InterfaceManagement.Item> lItemList = new List<InterfaceManagement.Item>();
            InterfaceManagement.Item lAddItem;
            foreach (ProdutoDM lItem in lProdutoDMList)
            {
                lAddItem = new InterfaceManagement.Item(lItem.proNome, lItem.proCodigo, lItem.proValorUnitario, "proCodigo");
                lItemList.Add(lAddItem);

            }            

            proCodigo.Items.Clear();
            proCodigo.ItemsSource = lItemList;
            proCodigo.DisplayMemberPath = "Name";
            proCodigo.SelectedValuePath = "Value";

            Logs.Log("CadastroOrcamento", "Abrir");
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

            Logs.Log("CadastroOrcamento", "Carregar orçamento, código: " + pCodigo);
        }

        private void LoadSub(string pCodigo)
        {
            ClearSub();
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            ProdutoxOrcamento lProdutoxOrcamento = new ProdutoxOrcamento();
            List<ProdutoxOrcamentoDM> lProdutoxOrcamentoDMList = lProdutoxOrcamento.SelectPorOrcamento(pCodigo);
            dtItens.ItemsSource = null;
            dtItens.ItemsSource = lProdutoxOrcamentoDMList;
            dtItens.AutoGenerateColumns = false;
            dtItens.AutoGenerateColumns = true;

            int lCount = 0;
            FormatedName lAtributo;
            List<int> lRemover = new List<int>();
            foreach (PropertyInfo lProperty in typeof(OrcamentoDM).GetProperties())
            {
                lAtributo = lProperty.GetCustomAttributes(typeof(FormatedName), false).Cast<FormatedName>().FirstOrDefault();
                if (lAtributo != null)
                {
                    dtItens.Columns[lCount].Header = lAtributo.Name;
                    dtItens.Columns[lCount].IsReadOnly = true;
                    lCount++;
                }
                else
                {
                    dtItens.Columns.RemoveAt(lCount);
                }

            }
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

                    Logs.Log("CadastroOrcamento", "Editar orçamento, código: " + lOrcamentoDM.orcCodigo);                    
                }
                else
                {
                    lOrcamentoDM.orcDataCriacao = DateTime.Now;
                    if (lOrcamento.NovoCliente(lOrcamentoDM, LoadOrcamento))
                        MessageBox.Show("Salvo com sucesso");
                    else
                        MessageBox.Show("Erro ao salvar");

                    Logs.Log("CadastroOrcamento", "Inserir orçamento");
                }
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Orcamento lOrcamento = new Orcamento();
            lOrcamento.ExcluirCliente(orcCodigo.Text);
            Logs.Log("CadastroOrcamento", "Excluir orçamento, código: " + orcCodigo.Text);
            Clear();            
        }

        private void Clear()
        {
            orcCodigo.Text = null;
            pesCodigoC.Text = null;
            orcStatus.Text = null;
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
            ProdutoxOrcamento lProdutoxOrcamento = new ProdutoxOrcamento();
            List<string> lErrosValidacao = new List<string>();
            ProdutoxOrcamentoDM lProdutoxOrcamentoDM = (ProdutoxOrcamentoDM)lInterfaceManagement.BuildDM(this, typeof(ProdutoxOrcamentoDM), ((Button)sender).Name, lErrosValidacao);
            if (lErrosValidacao != null && lErrosValidacao.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, lErrosValidacao));
            }
            else
            {
                if (!string.IsNullOrEmpty(genCodigo.Text))
                {
                    if (lProdutoxOrcamento.EditarCliente(lProdutoxOrcamentoDM))
                        MessageBox.Show("Editado com sucesso");
                    else
                        MessageBox.Show("Erro ao editar");

                    Logs.Log("CadastroOrcamento", "Editar item orçamento, código: " + lProdutoxOrcamentoDM.orcCodigo);
                }
                else
                {
                    if (lProdutoxOrcamento.NovoCliente(lProdutoxOrcamentoDM))
                        MessageBox.Show("Salvo com sucesso");
                    else
                        MessageBox.Show("Erro ao salvar");

                    Logs.Log("CadastroOrcamento", "Inserir item de orçamento");
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
            if (!decimal.TryParse(genValorUnitario.Text, out lValorUnitario))
                lValorUnitario = 0;

            if (!int.TryParse(genQuantidade.Text, out lQuantidade))
                lQuantidade = 0;

            genValorTotal.Text = ((decimal)(lQuantidade * lValorUnitario)).ToString();
        }

        private void proCodigo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (proCodigo.SelectedItem != null)
                genValorUnitario.Text = (proCodigo.SelectedItem as InterfaceManagement.Item).Adicional.ToString();
        }

        private void dtItens_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((sender as DataGrid).SelectedItem != null)
            {
                ProdutoxOrcamento lProdutoxOrcamento = new ProdutoxOrcamento();
                string lCodigo = ((sender as DataGrid).SelectedItem as ProdutoxOrcamentoDM).genCodigo.ToString();
                if (lProdutoxOrcamento.ExcluirCliente(lCodigo))
                {
                    LoadSub(orcCodigo.Text);
                    Logs.Log("CadastroOrcamento", "Excluir item de orçamento, código: " + lCodigo);
                }
            }
        }
    }
}
