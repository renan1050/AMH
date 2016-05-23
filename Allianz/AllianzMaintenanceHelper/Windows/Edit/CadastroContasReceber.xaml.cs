using BusinessRules.DatabaseBase.Classes;
using BusinessRules.DatabaseBase.Model;
using InterfaceBase;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AllianzMaintenanceHelper.Windows.Edit
{
    /// <summary>
    /// Interaction logic for CadastroContasReceber.xaml
    /// </summary>
    public partial class CadastroContasReceber : Window
    {
        public CadastroContasReceber()
        {
            InitializeComponent();
        }


        private void CheckTipo(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            RadioButton lRadioButton = sender as RadioButton;
                if (lRadioButton.Name == "corFormaPagamento_Boleto")
                {
                    UncheckCartao();
                    UncheckCheque();
                    UncheckDinheiro();
                    UncheckTransferencia();
                }
                else
                    if ((lRadioButton.Name == "corFormaPagamento_Cartao"))
                    {
                        UncheckBoleto();
                        UncheckCheque();
                        UncheckDinheiro();
                        UncheckTransferencia();
                    }
                    else
                        if ((lRadioButton.Name == "corFormaPagamento_Cheque"))
                        {
                            UncheckBoleto();
                            UncheckCartao();
                            UncheckDinheiro();
                            UncheckTransferencia();
                        }
                        else
                            if (lRadioButton.Name == "corFormaPagamento_Dinheiro")
                            {
                                UncheckBoleto();
                                UncheckCartao();
                                UncheckCheque();
                                UncheckTransferencia();
                            }
                            else
                                {
                                    UncheckBoleto();
                                    UncheckCartao();
                                    UncheckCheque();
                                    UncheckDinheiro();
                                }
            lInterfaceManagement.ShowByAttribute(lRadioButton.GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);
        }

        private void UncheckTipo(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.HideByAttribute(((RadioButton)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);
        }


        private void UncheckBoleto()
        {
            if (corFormaPagamento_Boleto != null)
                corFormaPagamento_Boleto.IsChecked = false;
        }

        private void UncheckCartao()
        {
            if (corFormaPagamento_Cartao != null)
                corFormaPagamento_Cartao.IsChecked = false;
        }

        private void UncheckCheque()
        {
            if (corFormaPagamento_Cheque != null)
                corFormaPagamento_Cheque.IsChecked = false;
        }

        private void UncheckDinheiro()
        {
            if (corFormaPagamento_Dinheiro != null)
                corFormaPagamento_Dinheiro.IsChecked = false;
        }

        private void UncheckTransferencia()
        {
            if (corFormaPagamento_Transferencia != null)
                corFormaPagamento_Transferencia.IsChecked = false;
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.LoadByValue(((Button)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(),
                                             this,
                                             LoadContaReceber,
                                             txtCodigoCarregar.Text);
        }

        private void LoadContaReceber(string pCodigo)
        {
            Clear();
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            ContasReceber lContasReceber = new ContasReceber();
            ContasReceberDM ContasReceberDM = lContasReceber.SelectCodigo(pCodigo);
            lInterfaceManagement.CarregarDM(this, ContasReceberDM);
            txtCodigoCarregar.Text = null;
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            ContasReceber lContasReceber = new ContasReceber();
            lContasReceber.ExcluirCliente(corCodigo.Text);
            Clear();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            ContasReceber lContasReceber = new ContasReceber();
            List<string> lErrosValidacao = new List<string>();
            ContasReceberDM ContasReceberDM = (ContasReceberDM)lInterfaceManagement.BuildDM(this, typeof(ContasReceberDM), ((Button)sender).Name, lErrosValidacao);
            if (lErrosValidacao != null && lErrosValidacao.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, lErrosValidacao));
            }
            else
            {
                if (!string.IsNullOrEmpty(corCodigo.Text))
                {
                    if (lContasReceber.EditarCliente(ContasReceberDM))
                        MessageBox.Show("Editado com sucesso");
                    else
                        MessageBox.Show("Erro ao editar");
                }
                else
                {
                    if (lContasReceber.NovoCliente(ContasReceberDM, LoadContaReceber))
                        MessageBox.Show("Salvo com sucesso");
                    else
                        MessageBox.Show("Erro ao salvar");
                }
            }

        }

        private void Clear()
        {
            corCodigo.Text = null;            
            corTipoCobranca.Text = null;
            corNDocumento.Text = null;
            corValorTotal.Text = null;
            corTaxaJuros.Text = null;
            corObservacao.Text = null;
        }
    }
}
