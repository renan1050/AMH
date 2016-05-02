using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InterfaceBase
{
    public class InterfaceManagement
    {        
        #region MÉTODO PARA A EXIBIÇÃO/ESCONDAÇÃO DO CAMPO CODIGO E DO CARREGAMENTO DE UM REGISTRO
        public void LoadByValue(string pValue, Window pWindow, Action<string> pLoadAction = null, string pLoadValue = null)
        {
            if (!string.IsNullOrEmpty(pValue))
            {
                List<DependencyObject> lDependencyObjectList = DependencyObjectHelper.GetDependencyObjectsWithProperty(pWindow,"RelativeField").Where(x => x.GetValue(WPFExtension.RelativeFieldProperty).Equals(pValue)).ToList();
                foreach (DependencyObject lItem in lDependencyObjectList)
                {
                    UIElement lUIElement = lItem as UIElement;

                    if (lUIElement == null)
                        return;
                    else
                    {
                        if (lUIElement.IsVisible)
                        {
                            if (!string.IsNullOrEmpty(pLoadValue))
                            {
                                if (pLoadAction != null)
                                    pLoadAction(pLoadValue);                                
                            }

                            lUIElement.Visibility = Visibility.Hidden;
                        }
                        else
                            lUIElement.Visibility = Visibility.Visible;
                    }
                }
            }
        }
        #endregion

        #region MÉTODOS PARA MOSTRAR/ESCONDER CAMPOS COM BASE NO VALOR DE ATRIBUTOS
        public void ShowByAttribute(string pValue, Window pWindow)
        {
            if (!string.IsNullOrEmpty(pValue))
            {
                List<DependencyObject> lDependencyObjectList = DependencyObjectHelper.GetDependencyObjectsWithProperty(pWindow, "RelativeField").Where(x => x.GetValue(WPFExtension.RelativeFieldProperty).Equals(pValue)).ToList();
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

        public void HideByAttribute(string pValue, Window pWindow)
        {
            if (!string.IsNullOrEmpty(pValue))
            {
                List<DependencyObject> lDependencyObjectList = DependencyObjectHelper.GetDependencyObjectsWithProperty(pWindow, "RelativeField").Where(x => x.GetValue(WPFExtension.RelativeFieldProperty).Equals(pValue)).ToList();
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
        #endregion

        #region MÉTODOS PARA PEGAR OS VALORES DOS FORMS
        public object BuildDM(DependencyObject pWindow, Type pTipoObjeto, string pEnviador, List<string> pErrosValidacao)
        {
            object lRetorno = Activator.CreateInstance(pTipoObjeto);
            var lPropriedades = pTipoObjeto.GetProperties();
            List<DependencyObject> lLabels = DependencyObjectHelper.GetDependencyObjectsWithProperty(pWindow, "Refers").ToList();
            List<Object> lChildren = new List<Object>();
            GetControlsList(pWindow,lChildren);
            PropertyInfo lPropriedade;
            foreach (Object lSender in lChildren)
            {
                if(lSender is TextBox){
                    TextBox lTextBox = (TextBox)lSender;
                    if (!string.IsNullOrEmpty(lTextBox.Text) && lPropriedades.Select(x => x.Name).ToList().Contains(lTextBox.Name))
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lTextBox.Name).First();

                        lPropriedade.SetValue(lRetorno, Convert.ChangeType(lTextBox.Text, lPropriedade.PropertyType));
                    }
                    else
                    {
                        DependencyProperty lRequired = DependencyObjectHelper.GetDependencyPropertyByName("Required", lTextBox);

                        if (lRequired != null && lTextBox.GetValue(lRequired).ToString().Split(';').Contains(pEnviador))
                        {
                            var lLabel = lLabels.Where(x => x.GetValue(WPFExtension.RefersProperty).Equals(lTextBox.Name)).FirstOrDefault();
                            pErrosValidacao.Add(string.Concat("O campo ", (lLabel != null ? ((Label)lLabel).Content.ToString() : lTextBox.Name), " é obrigatório."));
                        }
                    }
                }
                else if (lSender is ComboBox)
                {
                    ComboBox lComboBox = (ComboBox)lSender;
                    if (!string.IsNullOrEmpty(lComboBox.SelectedValue.ToString()) && lPropriedades.Select(x => x.Name).ToList().Contains(lComboBox.Name))
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lComboBox.Name).First();

                        lPropriedade.SetValue(lRetorno, Convert.ChangeType(lComboBox.SelectedValue.ToString(), lPropriedade.PropertyType));
                    }
                    else
                    {
                        DependencyProperty lRequired = DependencyObjectHelper.GetDependencyPropertyByName("Required", lComboBox);

                        if (lRequired != null && lComboBox.GetValue(lRequired).ToString().Split(';').Contains(pEnviador))
                        {
                            var lLabel = lLabels.Where(x => x.GetValue(WPFExtension.RefersProperty).Equals(lComboBox.Name)).FirstOrDefault();
                            pErrosValidacao.Add(string.Concat("O campo ", (lLabel != null ? ((Label)lLabel).Content.ToString() : lComboBox.Name), " é obrigatório."));
                        }
                    }
                }
                else if (lSender is CheckBox)
                {
                    CheckBox lCheckBox = (CheckBox)lSender;
                    DependencyProperty lValue = DependencyObjectHelper.GetDependencyPropertyByName("Value", lCheckBox);
                    if (lValue != null)
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lCheckBox.Name.Split('_').FirstOrDefault()).First();

                        lPropriedade.SetValue(lRetorno, Convert.ChangeType(lCheckBox.GetValue(lValue).ToString(), lPropriedade.PropertyType));
                    }
                    else
                    {
                        DependencyProperty lRequired = DependencyObjectHelper.GetDependencyPropertyByName("Required", lCheckBox);

                        if (lRequired != null && lCheckBox.GetValue(lRequired).ToString().Split(';').Contains(pEnviador))
                        {
                            pErrosValidacao.Add(string.Concat("O campo ", (lCheckBox.Content != null ? lCheckBox.Content.ToString() : lCheckBox.Name), " é obrigatório."));
                        }
                    }
                }
                else if (lSender is RadioButton)
                {
                    RadioButton lRadioButton = (RadioButton)lSender;
                    DependencyProperty lValue = DependencyObjectHelper.GetDependencyPropertyByName("Value", lRadioButton);
                    if (lValue != null)
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lRadioButton.Name.Split('_').FirstOrDefault()).First();

                        lPropriedade.SetValue(lRetorno, Convert.ChangeType(lRadioButton.GetValue(lValue).ToString(), lPropriedade.PropertyType));
                    }
                    else
                    {
                        DependencyProperty lRequired = DependencyObjectHelper.GetDependencyPropertyByName("Required", lRadioButton);

                        if (lRequired != null && lRadioButton.GetValue(lRequired).ToString().Split(';').Contains(pEnviador))
                        {
                            pErrosValidacao.Add(string.Concat("O campo ", (lRadioButton.Content != null ? lRadioButton.Content.ToString() : lRadioButton.Name), " é obrigatório."));
                        }
                    }
                }
                //tem que remover isso aqui depois, ou tratar melhor
                else 
                {
                    pErrosValidacao.Add(string.Concat("Erro bem loco"));
                }
                
            }                

            return lRetorno;
        
        }

        public void CarregarDM(DependencyObject pWindow, object pDM)
        {
            if (pDM == null)
            {
                MessageBox.Show("Registro não encontrado");
                return;
            }

            var lPropriedades = pDM.GetType().GetProperties();

            List<Object> lChildren = new List<Object>();
            GetControlsList(pWindow, lChildren);
            PropertyInfo lPropriedade;
            foreach (Object lSender in lChildren)
            {
                if (lSender is TextBox)
                {
                    TextBox lTextBox = (TextBox)lSender;
                    if (lPropriedades.Select(x => x.Name).ToList().Contains(lTextBox.Name))
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lTextBox.Name).First();
                        if (lPropriedade.GetValue(pDM) != null)
                            lTextBox.Text = lPropriedade.GetValue(pDM).ToString();
                        else
                            lTextBox.Text = string.Empty;
                    }
                }
                else if (lSender is ComboBox)
                {
                    ComboBox lComboBox = (ComboBox)lSender;
                    if (lPropriedades.Select(x => x.Name).ToList().Contains(lComboBox.Name))
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lComboBox.Name).First();
                        if (lPropriedade.GetValue(pDM) != null)
                            lComboBox.Text = lPropriedade.GetValue(pDM).ToString();
                        else
                            lComboBox.Text = string.Empty;
                    }
                }
                else if (lSender is CheckBox)
                {
                    CheckBox lCheckBox = (CheckBox)lSender;
                }
                else if (lSender is RadioButton)
                {
                    RadioButton lComboBox = (RadioButton)lSender;
                }
                else 
                {
                    //deu erro
                }
            }
        }

        private void GetControlsList(DependencyObject pControl,List<Object> pChildren)
        {   
            int lChildNumber = VisualTreeHelper.GetChildrenCount(pControl);

            for (int i = 0; i <= lChildNumber - 1; i++)
            {
                var lChild = VisualTreeHelper.GetChild(pControl, i);

                if (lChild is TextBox)
                    pChildren.Add(lChild as TextBox);
                else if (lChild is ComboBox)
                    pChildren.Add(lChild as ComboBox);
                else if (lChild is CheckBox && (bool)((CheckBox)lChild).IsChecked)
                    pChildren.Add(lChild as CheckBox);
                else if (lChild is RadioButton && (bool)((RadioButton)lChild).IsChecked)
                    pChildren.Add(lChild as RadioButton);

                if (VisualTreeHelper.GetChildrenCount(lChild) > 0)
                {
                    GetControlsList(lChild, pChildren);
                }
            }
        }

        #endregion
    } 
}
