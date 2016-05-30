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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CadastroPessoas : Window
    {

        private List<CidadeDM> gSource = null;
        private int? gLastVal = 0;
        public CadastroPessoas()
        {
            InitializeComponent();
            pesTipoPessoa_PF .IsChecked = true;
            Estado lEstado = new Estado();
            List<EstadoDM> lEstadoDMList = lEstado.SelecionarTudo();
            estCodigo.ItemsSource = lEstadoDMList;
            estCodigo.DisplayMemberPath = "estSigla";
            estCodigo.SelectedValuePath = "estCodigo";

            Cidade lCidade = new Cidade();
            gSource = lCidade.SelecionarTudo();
            cidCodigo.ItemsSource = gSource;
            cidCodigo.DisplayMemberPath = "cidNome";
            cidCodigo.SelectedValuePath = "cidCodigo";
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            try
            {
                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                lInterfaceManagement.LoadByValue(((Button)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(),
                                                 this,
                                                 LoadPessoa,
                                                 txtCodigoCarregar.Text);
                if (txtCodigoCarregar.IsVisible)
                    txtCodigoCarregar.Focus();
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void LoadPessoa(string pCodigo)
        {
            try
            {
                Clear();

                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                Pessoa lPessoa = new Pessoa();
                PessoaDM lPessoaDM = lPessoa.SelectCodigo(pCodigo);
                lInterfaceManagement.CarregarDM(this, lPessoaDM, estCodigo_SelectionChanged, estCodigo.Name);

                if (lPessoaDM != null)
                {
                    PessoaxTipo lPessoaxTipo = new PessoaxTipo();
                    List<PessoaxTipoDM> lPessoaxTipoDMList = lPessoaxTipo.SelecionarPorCliente(lPessoaDM.pesCodigo);

                    foreach (int lCategoria in lPessoaxTipoDMList.Select(x => x.tipCodigo))
                    {
                        switch (lCategoria)
                        {
                            case PessoaFeature.TipoPessoa.Cliente:
                                {
                                    Categoria_C.IsChecked = true;
                                }
                                break;
                            case PessoaFeature.TipoPessoa.Filial:
                                {
                                    Categoria_Fi.IsChecked = true;
                                }
                                break;
                            case PessoaFeature.TipoPessoa.Funcionario:
                                {
                                    Categoria_F.IsChecked = true;
                                }
                                break;
                        }
                    }
                }

                txtCodigoCarregar.Text = null;
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void LoadPessoaAtt(string pCodigo)
        {
            AtualizarTipos(pCodigo);

            LoadPessoa(pCodigo);
        }

        private void AtualizarTipos(string pCodigo)
        {
            try
            {
                PessoaxTipo lPessoaxTipo = new PessoaxTipo();
                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                List<string> lTipos = new List<string>();
                List<Object> lChildren = new List<Object>();
                lInterfaceManagement.GetControlsList(Categoria, lChildren, pNoVisibilityCheck: true);
                foreach (Object lChild in lChildren)
                {
                    lTipos.Add(((CheckBox)lChild).GetValue(WPFExtension.ValueProperty).ToString());
                }

                if (!lPessoaxTipo.Refresh(int.Parse(pCodigo), lTipos.ToArray()))
                    MessageBox.Show("Erro ao salvar categorias");
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                Pessoa lPessoa = new Pessoa();
                List<string> lErrosValidacao = new List<string>();
                PessoaDM lPessoaDM = (PessoaDM)lInterfaceManagement.BuildDM(this, typeof(PessoaDM), ((Button)sender).Name, lErrosValidacao);

                if (lErrosValidacao != null && lErrosValidacao.Count > 0)
                {
                    MessageBox.Show(string.Join(Environment.NewLine, lErrosValidacao));
                }
                else
                {
                    if (!string.IsNullOrEmpty(pesCodigo.Text))
                    {
                        if (lPessoa.EditarCliente(lPessoaDM, AtualizarTipos))
                            MessageBox.Show("Editado com sucesso");
                        else
                            MessageBox.Show("Erro ao editar");
                    }
                    else
                    {
                        if (lPessoa.NovoCliente(lPessoaDM, LoadPessoaAtt))
                        {
                            MessageBox.Show("Salvo com sucesso");
                        }
                        else
                            MessageBox.Show("Erro ao salvar");
                    }
                }
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void CheckTipo(object sender, RoutedEventArgs e)
        {
            try
            {
                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                RadioButton lRadioButton = sender as RadioButton;
                if (lRadioButton.Name == "pesTipoPessoa_PF")
                    UncheckPJ();
                else
                    UncheckPF();

                lInterfaceManagement.ShowByAttribute(lRadioButton.GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void UncheckTipo(object sender, RoutedEventArgs e)
        {
            try
            {
                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                lInterfaceManagement.HideByAttribute(((RadioButton)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void UncheckPF()
        {
            if (pesTipoPessoa_PF != null)
                pesTipoPessoa_PF.IsChecked = false;
        }

        private void UncheckPJ()
        {
            if (pesTipoPessoa_PJ != null)
                pesTipoPessoa_PJ.IsChecked = false;
        }

        private void CheckCategoria(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.ShowByAttribute(((CheckBox)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);
        }

        private void UncheckCategoria(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.HideByAttribute(((CheckBox)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pessoa lPessoa = new Pessoa();
                lPessoa.ExcluirCliente(pesCodigo.Text);
                Clear();
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void Clear()
        {
            pesCodigo.Text = null;
            pesNome.Text = null;
            pesRazaoSocial.Text = null;
            pesTipoPessoa_PF.IsChecked = true;
            pesTipoPessoa_PJ.IsChecked = false;
            pesRG.Text = null;
            pesCPF.Text = null;
            pesCNPJ.Text = null;
            pesTelResidencial.Text = null;
            pesTelComercial.Text = null;
            pesCelular.Text = null;
            pesEmail.Text = null;
            pesNascimento.Text = null;
            pesCEP.Text = null;
            pesEndereco.Text = null;
            pesNumero.Text = null;
            pesComplemento.Text = null;
            pesBairro.Text = null;
            estCodigo.SelectedValue = null;
            cidCodigo.SelectedValue = null;
            pesCargo.Text = null;
            Categoria_C.IsChecked = false;
            Categoria_F.IsChecked = false;
            Categoria_Fi.IsChecked = false;
        }

        private void estCodigo_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int? lNewValue = (estCodigo.SelectedValue != null ? Convert.ToInt16(estCodigo.SelectedValue) : 0);

                if (estCodigo.SelectedValue == null)
                    lNewValue = null;

                if (gLastVal != null && gLastVal != lNewValue)
                {
                    List<CidadeDM> lFilter = gSource.Where(x => x.estCodigo == lNewValue).ToList();
                    cidCodigo.ItemsSource = lFilter;
                    cidCodigo.DisplayMemberPath = "cidNome";
                    cidCodigo.SelectedValuePath = "cidCodigo";

                    if (lNewValue == null)
                        gLastVal = 0;
                    else
                        gLastVal = lNewValue;
                }
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }
    }
}
