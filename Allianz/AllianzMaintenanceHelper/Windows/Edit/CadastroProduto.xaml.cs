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
    /// Interaction logic for CadastroProduto.xaml
    /// </summary>
    /// 

    public partial class CadastroProduto : Window
    {
        public CadastroProduto()
        {
            InitializeComponent();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.LoadByValue(((Button)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(),
                                             this,
                                             LoadProduto,
                                             txtCodigoCarregar.Text);
            if (txtCodigoCarregar.IsVisible)
                txtCodigoCarregar.Focus();
        }

        private void LoadProduto(string pCodigo)
        {
            Clear();
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Produto lProduto = new Produto();
            ProdutoDM lProdutoDM = lProduto.SelectCodigo(pCodigo);
            lInterfaceManagement.CarregarDM(this, lProdutoDM);
            txtCodigoCarregar.Text = null;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Produto lProduto = new Produto();
            List<string> lErrosValidacao = new List<string>();
            ProdutoDM lProdutoDM = (ProdutoDM)lInterfaceManagement.BuildDM(this, typeof(ProdutoDM), ((Button)sender).Name, lErrosValidacao);
            if (lErrosValidacao != null && lErrosValidacao.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, lErrosValidacao));
            }
            else
            {
                if (!string.IsNullOrEmpty(proCodigo.Text))
                {
                    if (lProduto.EditarCliente(lProdutoDM))
                        MessageBox.Show("Editado com sucesso");
                    else
                        MessageBox.Show("Erro ao editar");
                }
                else
                {
                    if (lProduto.NovoCliente(lProdutoDM, LoadProduto))
                        MessageBox.Show("Salvo com sucesso");
                    else
                        MessageBox.Show("Erro ao salvar");
                }
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Produto lProduto = new Produto();
            lProduto.ExcluirCliente(proCodigo.Text);
            Clear();
        }

        private void Clear()
        {
            proCodigo.Text = null;
            proNome.Text = null;
            proValorUnitario.Text = null;
        }
    }
}
