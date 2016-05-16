using AllianzMaintenanceHelper.Windows.Edit;
using AllianzMaintenanceHelper.Windows.List;
using BusinessRules.DatabaseBase.Classes;
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

namespace AllianzMaintenanceHelper.Windows
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        #region Pessoa

        private void PesCadastro_Click(object sender, RoutedEventArgs e)
        {
            CadastroPessoas lCadastroPessoas = new CadastroPessoas();
            lCadastroPessoas.Show();
        }

        private void PesConsulta_Click(object sender, RoutedEventArgs e)
        {
            ConsultaPessoas lConsultaPessoas = new ConsultaPessoas();
            lConsultaPessoas.Show();
        }

        #endregion

        #region Veículo

        private void VeiCadastro_Click(object sender, RoutedEventArgs e)
        {
            CadastroVeiculo lCadastroVeiculo = new CadastroVeiculo();
            lCadastroVeiculo.Show();
        }

        private void VeiConsulta_Click(object sender, RoutedEventArgs e)
        {
            ConsultarVeiculos lConsultarVeiculos = new ConsultarVeiculos();
            lConsultarVeiculos.Show();
        }

        #endregion

        #region Serviço

        private void SerCadastro_Click(object sender, RoutedEventArgs e)
        {
            CadastroServico lCadastroServico = new CadastroServico();
            lCadastroServico.Show();
        }

        private void SerConsulta_Click(object sender, RoutedEventArgs e)
        {
            ConsultaProduto lConsultaServico = new ConsultaProduto();
            lConsultaServico.Show();
        }

        #endregion

        #region OrdemServiço

        private void OrdCadastro_Click(object sender, RoutedEventArgs e)
        {
            CadastroOrdem lCadastroOrdem = new CadastroOrdem();
            lCadastroOrdem.Show();
        }

        private void OrdConsulta_Click(object sender, RoutedEventArgs e)
        {
            ConsultarOrdens_de_servico lConsultarOrdens_de_servico = new ConsultarOrdens_de_servico();
            lConsultarOrdens_de_servico.Show();
        }

        #endregion

        #region ContasPagar

        private void CopCadastro_Click(object sender, RoutedEventArgs e)
        {
            CadastroContasPagar lCadastroContasPagar = new CadastroContasPagar();
            lCadastroContasPagar.Show();
        }

        //private void CopConsulta_Click(object sender, RoutedEventArgs e)
        //{
        //    ConsultaC lContasPagar = new ContasPagar();
        //    lContasPagar.Show();
        //}

        #endregion

        #region ContasReceber

        private void CorCadastro_Click(object sender, RoutedEventArgs e)
        {
            CadastroContasReceber lCadastroContasReceber = new CadastroContasReceber();
            lCadastroContasReceber.Show();
        }

        //private void CopConsulta_Click(object sender, RoutedEventArgs e)
        //{
        //    ConsultaC lContasPagar = new ContasPagar();
        //    lContasPagar.Show();
        //}

        #endregion
    }
}
