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
    /// Interaction logic for CadastroContasPagar.xaml
    /// </summary>
    public partial class CadastroContasPagar : Window
    {
        public CadastroContasPagar()
        {
            InitializeComponent();
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Load(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.LoadByValue(((Button)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(),
                                             this,
                                             LoadContaPagar,
                                             txtCodigoCarregar.Text);
        }

        private void LoadContaPagar(string pCodigo)
        {
            Clear();
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            ContasPagar lContasPagar = new ContasPagar();
            ContasPagarDM ContasPagarDM = lContasPagar.SelectCodigo(pCodigo);
            lInterfaceManagement.CarregarDM(this, ContasPagarDM);
            txtCodigoCarregar.Text = null;
        }


        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            ContasPagar lContasPagar = new ContasPagar();
            List<string> lErrosValidacao = new List<string>();
            ContasPagarDM ContasReceberDM = (ContasPagarDM)lInterfaceManagement.BuildDM(this, typeof(ContasPagarDM), ((Button)sender).Name, lErrosValidacao);
            if (lErrosValidacao != null && lErrosValidacao.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, lErrosValidacao));
            }
            else
            {
                if (!string.IsNullOrEmpty(copCodigo.Text))
                {
                    if (lContasPagar.EditarCliente(ContasReceberDM))
                        MessageBox.Show("Editado com sucesso");
                    else
                        MessageBox.Show("Erro ao editar");
                }
                else
                {
                    if (lContasPagar.NovoCliente(ContasReceberDM, LoadContaPagar))
                        MessageBox.Show("Salvo com sucesso");
                    else
                        MessageBox.Show("Erro ao salvar");
                }
            }
        }

        private void CheckTipo(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            RadioButton lRadioButton = sender as RadioButton;
            if (lRadioButton.Name == "copFormaPagamento_Boleto")
            { 
                UncheckCartao();
                UncheckCheque();
                UncheckDinheiro();
                UncheckTransferencia();
            }
            else 
                if ((lRadioButton.Name == "copFormaPagamento_Cartao"))
                {
                    UncheckBoleto();
                    UncheckCheque();
                    UncheckDinheiro();
                    UncheckTransferencia();
                }
                else
                    if ((lRadioButton.Name == "copFormaPagamento_Cheque"))
                    {
                        UncheckBoleto();
                        UncheckCartao();
                        UncheckDinheiro();
                        UncheckTransferencia();
                    }
                    else
                        if (lRadioButton.Name == "copFormaPagamento_Dinheiro")
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
            if (copFormaPagamento_Boleto != null)
                copFormaPagamento_Boleto.IsChecked = false;
        }

        private void UncheckCartao()
        {
            if (copFormaPagamento_Cartao != null)
                copFormaPagamento_Cartao.IsChecked = false;
        }

        private void UncheckCheque()
        {
            if (copFormaPagamento_Cheque != null)
                copFormaPagamento_Cheque.IsChecked = false;
        }

        private void UncheckDinheiro()
        {
            if (copFormaPagamento_Dinheiro != null)
                copFormaPagamento_Dinheiro.IsChecked = false;
        }

        private void UncheckTransferencia()
        {
            if (copFormaPagamento_Transferencia != null)
                copFormaPagamento_Transferencia.IsChecked = false;
        }


        private void Clear()
        {
            copCodigo.Text = null;
            copNomeCredor.Text = null;
            copNDocumento.Text = null;
            copValorTotal.Text = null;
            copObservacao.Text = null;
        }

    }
}
