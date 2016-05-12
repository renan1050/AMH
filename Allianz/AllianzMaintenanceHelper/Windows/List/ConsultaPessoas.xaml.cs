using BusinessRules.DatabaseBase.Classes;
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
    /// Interaction logic for ConsultaPessoas.xaml
    /// </summary>
    public partial class ConsultaPessoas : Window
    {
        public ConsultaPessoas()
        {
            InitializeComponent();
            Atualizar();
        }

        private void CheckTipo(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            RadioButton lRadioButton = sender as RadioButton;
            if (lRadioButton.Name == "radPF")
                UncheckPJ();
            else
                UncheckPF();

            lInterfaceManagement.ShowByAttribute(lRadioButton.GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);
        }

        private void UncheckTipo(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.HideByAttribute(((RadioButton)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);
        }

        private void UncheckPF()
        {
            if (radPF != null)
                radPF.IsChecked = false;
        }

        private void UncheckPJ()
        {
            if (radPJ != null)
                radPJ.IsChecked = false;
        }

        private void Atualizar()
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Pessoa lPessoa = new Pessoa();
            Dictionary<string, string> lParametro = new Dictionary<string, string>();
            lParametro.Add(pesNome.Name, pesNome.Text);
            lParametro.Add(pesCPF.Name, pesCPF.Text);
            lParametro.Add(pesCNPJ.Name, pesCNPJ.Text);
            lParametro.Add(pesRG.Name, pesRG.Text);
            //lParametro.Add(radPF.Name, radPF.Text);

            dtRegistros.ItemsSource = lPessoa.AtualizarGrade(lParametro);
            
        }
    }
}
