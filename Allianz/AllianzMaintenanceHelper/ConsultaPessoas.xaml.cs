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
        }

        private void CheckTipo(object sender, RoutedEventArgs e)
        {
            RadioButton lRadioButton = sender as RadioButton;
            if (lRadioButton.Name == "radPF")
                UncheckPJ();
            else
                UncheckPF();

            string lCode = lRadioButton.GetValue(WPFExtension.RelativeFieldCodeProperty).ToString();
            if (!string.IsNullOrEmpty(lCode))
            {
                List<DependencyObject> lDependencyObjectList = DependencyObjectHelper.GetDependencyObjectsWithProperty(this, "RelativeField").Where(x => x.GetValue(WPFExtension.RelativeFieldProperty).Equals(lCode)).ToList();
                foreach (DependencyObject lItem in lDependencyObjectList)
                {
                    UIElement lUIElement = lItem as UIElement;

                    if (lUIElement == null)
                        return;
                    else
                    {
                        lUIElement.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void UncheckTipo(object sender, RoutedEventArgs e)
        {
            string lCode = ((RadioButton)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString();
            if (!string.IsNullOrEmpty(lCode))
            {
                List<DependencyObject> lDependencyObjectList = DependencyObjectHelper.GetDependencyObjectsWithProperty(this, "RelativeField").Where(x => x.GetValue(WPFExtension.RelativeFieldProperty).Equals(lCode)).ToList();
                foreach (DependencyObject lItem in lDependencyObjectList)
                {
                    UIElement lUIElement = lItem as UIElement;

                    if (lUIElement == null)
                        return;
                    else
                    {
                        lUIElement.Visibility = Visibility.Hidden;
                    }
                }
            }
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
    }
}
