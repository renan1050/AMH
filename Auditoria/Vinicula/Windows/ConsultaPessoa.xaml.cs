using BusinessRules.DatabaseBase.Classes;
using BusinessRules.DatabaseBase.Model;
using InterfaceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Vinicula
{
    /// <summary>
    /// Interaction logic for ConsultaPessoa.xaml
    /// </summary>
    public partial class ConsultaPessoa : Window
    {
        public ConsultaPessoa()
        {
            InitializeComponent();
            Atualizar(true);
            //pesTipoPessoa_PF.IsChecked = true;            
        }

        private void CheckTipo(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            RadioButton lRadioButton = sender as RadioButton;
                    
            if (lRadioButton.Name == "pesTipoPessoa_PF")
            {
                UncheckPJ();
                Atualizar(); 
            }            
            else
            {
                UncheckPF();
                Atualizar();
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
            dtRegistros.ItemsSource = null;
            dtRegistros.ItemsSource = lPessoa.AtualizarGrade(lParametro);
            dtRegistros.AutoGenerateColumns = false;
            dtRegistros.AutoGenerateColumns = true;
            
            if (!pAbrindo)
            {

                int lCount = 0;
                FormatedName lAtributo;
                List<int> lRemover = new List<int>();
                foreach (PropertyInfo lProperty in typeof(PessoaDM).GetProperties())
                {
                    lAtributo = lProperty.GetCustomAttributes(typeof(FormatedName), false).Cast<FormatedName>().FirstOrDefault();
                    if (lAtributo != null)
                    {
                        dtRegistros.Columns[lCount].Header = lAtributo.Name;
                        dtRegistros.Columns[lCount].IsReadOnly = true;
                        lCount++;
                    }
                    else
                    {
                        dtRegistros.Columns.RemoveAt(lCount);
                    }
                    
                }   
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CadastroPessoa lCadastroPessoas = new CadastroPessoa();
            lCadastroPessoas.Show();
            
            
        }

        private void pesNome_LostFocus(object sender, RoutedEventArgs e)
        {   
            Atualizar();            
        }
    }
}
