using BusinessRules.DatabaseBase;
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
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.LoadByValue(((Button)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(),
                                             this,
                                             LoadVeiculo,
                                             txtCodigoCarregar.Text);
        }

        private void LoadVeiculo(string pCodigo)
        {
            Veiculo lVeiculo = new Veiculo();
            VeiculoDM lVeiculoDM = lVeiculo.SelectCodigo(pCodigo);

            if (lVeiculoDM != null)
            {
                pesCodigoC.Text = lVeiculoDM.pesCodigoC.ToString();
                veiCodigo.Text = lVeiculoDM.veiCodigo.ToString();
                veiMarca.Text = lVeiculoDM.veiMarca;
                veiModelo.Text = lVeiculoDM.veiModelo;
                veiPlaca.Text = lVeiculoDM.veiPlaca;
                veiRENAVAM.Text = lVeiculoDM.veiRENAVAM;
            }
            else
            {
                MessageBox.Show("Registro não encontrado");
            }

            txtCodigoCarregar.Text = null;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Veiculo lVeiculo = new Veiculo();
            VeiculoDM lVeiculoDM = new VeiculoDM();
            lVeiculoDM.pesCodigoC = int.Parse(pesCodigoC.Text);            
            lVeiculoDM.veiMarca = veiMarca.Text;
            lVeiculoDM.veiModelo = veiModelo.Text;
            lVeiculoDM.veiPlaca = veiPlaca.Text;
            lVeiculoDM.veiRENAVAM = veiRENAVAM.Text;

            if(!string.IsNullOrEmpty(veiCodigo.Text))
            {
                lVeiculoDM.veiCodigo = int.Parse(veiCodigo.Text);
                if (lVeiculo.EditarCliente(lVeiculoDM))
                    MessageBox.Show("Editado com sucesso");
                else
                    MessageBox.Show("Erro ao editar");
            }
            else
            {
                if (lVeiculo.NovoCliente(lVeiculoDM))
                    MessageBox.Show("Salvo com sucesso");
                else
                    MessageBox.Show("Erro ao salvar");
            }
        }

    }
}
