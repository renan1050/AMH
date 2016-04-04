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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CadastroPessoas : Window
    {
        public CadastroPessoas()
        {  
            InitializeComponent();
            radPF.IsChecked = true;
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            string lCode = ((Button)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString();
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
                        if (lUIElement.IsVisible)
                        {
                            if (!string.IsNullOrEmpty(txtCodigoCarregar.Text))
                            {
                                //TODO: CODIGO PARA CARREGAR O REGISTRO AQUI
                                MessageBox.Show("Carregaria");
                            }

                            lUIElement.Visibility = Visibility.Hidden;
                        }
                        else
                            lUIElement.Visibility = Visibility.Visible;
                    }
                }
            }
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

        private void CheckCategoria(object sender, RoutedEventArgs e)
        {
            string lCode = ((CheckBox)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString();
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

        private void UncheckCategoria(object sender, RoutedEventArgs e)
        {
            string lCode = ((CheckBox)sender).GetValue(WPFExtension.RelativeFieldCodeProperty).ToString();
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
        
    }
}
