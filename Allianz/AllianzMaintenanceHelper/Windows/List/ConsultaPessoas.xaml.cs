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
            pesTipoPessoa_PF.IsChecked = true;
            Atualizar(true);
        }

        private void CheckTipo(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            RadioButton lRadioButton = sender as RadioButton;
                    
            if (lRadioButton.Name == "pesTipoPessoa_PF")
            {
                UncheckPJ();
                Atualizar(true); 
            }            
            else
            {
                UncheckPF();
                Atualizar(true);
            }
                
            
            lInterfaceManagement.ShowByAttribute(lRadioButton.GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);
        }

        private void UncheckTipo(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.HideByAttribute(((RadioButton)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);
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

        private void Atualizar(bool pAbrindo = false)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            Pessoa lPessoa = new Pessoa();
            Dictionary<string, string> lParametro = new Dictionary<string, string>();
            List<Object> lChildren = new List<Object>();
            lInterfaceManagement.GetControlsList(pesTipoPessoa, lChildren, pNoVisibilityCheck: pAbrindo);
            if (lChildren.Count > 0)
            {
                string[] lTipoPessoa = ((RadioButton)lChildren.First()).Name.Split('_');
                lParametro.Add(lTipoPessoa[0], lTipoPessoa[1]);

            }
            lParametro.Add(pesNome.Name, pesNome.Text);
            lParametro.Add(pesCPF.Name, pesCPF.Text);
            lParametro.Add(pesCNPJ.Name, pesCNPJ.Text);
            lParametro.Add(pesRG.Name, pesRG.Text);
            
            //o campo pesTipoPessoa ta no grid com o nome pesTipoPessoa
            // vc tem q usar o metodo GetControlsList para trazer o radio marcado
            // com o radio marcado vc usa o .Split("_")
            // ele a criar um string[] com o testo do nome do radio, por exemplo pesTipoPessoa_PF vira string[]{"pesTipoPessoa","PF"}
            // vc adiciona no dicionario string[0] na key e string[1] no value

            

            //lParametro.Add(radPF.Name, radPF.Text);

            dtRegistros.ItemsSource = lPessoa.AtualizarGrade(lParametro);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CadastroPessoas lCadastroPessoas = new CadastroPessoas();
            lCadastroPessoas.Show();
            
            
        }

        private void pesNome_LostFocus(object sender, RoutedEventArgs e)
        {   
            Atualizar();            
        }
    }
}
