﻿using BusinessRules.DatabaseBase;
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
    /// Interaction logic for CadastroVeiculo.xaml
    /// </summary>
    public partial class CadastroVeiculo : Window
    {
        public CadastroVeiculo()
        {
            InitializeComponent();
            Pessoa lPessoa = new Pessoa();
            List<PessoaDM> lPessoaDMList = lPessoa.SelecionarPorTipo(PessoaFeature.TipoPessoa.Cliente);
            pesCodigoC.ItemsSource = lPessoaDMList.ToDictionary(x => x.pesCodigo, x => x.pesNome);
            pesCodigoC.DisplayMemberPath = "Value";
            pesCodigoC.SelectedValuePath = "Key";

        }

        private void Load(object sender, RoutedEventArgs e)
        {
            try
            {
                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                lInterfaceManagement.LoadByValue(((Button)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(),
                                                 this,
                                                 LoadVeiculo,
                                                 txtCodigoCarregar.Text);
                if (txtCodigoCarregar.IsVisible)
                    txtCodigoCarregar.Focus();
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void LoadVeiculo(string pCodigo)
        {
            try
            {
                Clear();
                InterfaceManagement lInterfaceManagement = new InterfaceManagement();
                Veiculo lVeiculo = new Veiculo();
                VeiculoDM lVeiculoDM = lVeiculo.SelectCodigo(pCodigo);
                lInterfaceManagement.CarregarDM(this, lVeiculoDM);
                txtCodigoCarregar.Text = null;
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
                Veiculo lVeiculo = new Veiculo();
                List<string> lErrosValidacao = new List<string>();
                VeiculoDM lVeiculoDM = (VeiculoDM)lInterfaceManagement.BuildDM(this, typeof(VeiculoDM), ((Button)sender).Name, lErrosValidacao);
                if (lErrosValidacao != null && lErrosValidacao.Count > 0)
                {
                    MessageBox.Show(string.Join(Environment.NewLine, lErrosValidacao));
                }
                else
                {
                    if (!string.IsNullOrEmpty(veiCodigo.Text))
                    {
                        if (lVeiculo.EditarCliente(lVeiculoDM))
                            MessageBox.Show("Editado com sucesso");
                        else
                            MessageBox.Show("Erro ao editar");
                    }
                    else
                    {
                        if (lVeiculo.NovoCliente(lVeiculoDM, LoadVeiculo))
                            MessageBox.Show("Salvo com sucesso");
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

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Veiculo lVeiculo = new Veiculo();
                lVeiculo.ExcluirCliente(veiCodigo.Text);
                Clear();
            }
            catch (Exception pE)
            {
                MessageBox.Show(pE.Message);
            }
        }

        private void Clear()
        {
            veiCodigo.Text = null;
            veiMarca.Text = null;
            veiModelo.Text = null;
            veiPlaca.Text = null;
            veiRENAVAM.Text = null;
            pesCodigoC.Text = null;
        }

    }
}
