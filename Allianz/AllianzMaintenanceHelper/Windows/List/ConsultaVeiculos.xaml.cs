using System;
using InterfaceBase;
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
    /// Interaction logic for ConsultarVeiculos.xaml
    /// </summary>
    public partial class ConsultarVeiculos : Window
    {
        public ConsultarVeiculos()
        {
            InitializeComponent();
        }

        private void CheckTipo(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            RadioButton lRadioButton = sender as RadioButton;
            if (lRadioButton.Name == "rad_pesNome")
            {
                UncheckPlaca();
                UncheckMarca();
            }
            else if (lRadioButton.Name == "rad_veiPlaca")
            {
                UncheckNome();
                UncheckMarca();
            }
            else
            {
                UncheckNome();
                UncheckPlaca();
            }
                

            lInterfaceManagement.ShowByAttribute(lRadioButton.GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);
        }

        private void UncheckTipo(object sender, RoutedEventArgs e)
        {
            InterfaceManagement lInterfaceManagement = new InterfaceManagement();
            lInterfaceManagement.HideByAttribute(((RadioButton)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString(), this);
        }

        private void UncheckNome()
        {
            if (rad_pesNome != null)
                rad_pesNome.IsChecked = false;
        }
                
        private void UncheckPlaca()
        {
            if (rad_veiPlaca != null)
                rad_veiPlaca.IsChecked = false;
        }

        private void UncheckMarca()
        {
            if (rad_veiMarca != null)
                rad_veiMarca.IsChecked = false;
        }

    }
}

