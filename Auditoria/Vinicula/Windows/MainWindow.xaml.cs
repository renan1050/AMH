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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vinicula
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Pessoa

        private void PesCadastro_Click(object sender, RoutedEventArgs e)
        {
            CadastroPessoa lCadastroPessoa = new CadastroPessoa();
            lCadastroPessoa.Show();
        }

        private void PesConsulta_Click(object sender, RoutedEventArgs e)
        {
            ConsultaPessoa lConsultaPessoas = new ConsultaPessoa();
            lConsultaPessoas.Show();
        }

        #endregion

        #region Orçamento

        private void OrcCadastro_Click(object sender, RoutedEventArgs e)
        {
            CadastroOrcamento lCadastroOrcamento = new CadastroOrcamento();
            lCadastroOrcamento.Show();
        }

        private void OrcConsulta_Click(object sender, RoutedEventArgs e)
        {
            ConsultaOrcamento lConsultaOrcamento = new ConsultaOrcamento();
            lConsultaOrcamento.Show();
        }

        #endregion

        #region Produto

        private void ProCadastro_Click(object sender, RoutedEventArgs e)
        {
            CadastroProduto lCadastroProduto = new CadastroProduto();
            lCadastroProduto.Show();
        }

        private void ProConsulta_Click(object sender, RoutedEventArgs e)
        {
            ConsultaProduto lConsultaProduto = new ConsultaProduto();
            lConsultaProduto.Show();
        }

        #endregion

        #region Sobre

        private void Sobre_Click(object sender, RoutedEventArgs e)
        {
            Sobre lSobre = new Sobre();
            lSobre.Show();
        }


        #endregion
    }
}
