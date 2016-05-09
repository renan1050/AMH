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
            GetControlsList(pWindow, lChildren, true);
            PropertyInfo lPropriedade;
            List<Object> lParents = new List<Object>();
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
                    if (lComboBox.SelectedValue != null && !string.IsNullOrEmpty(lComboBox.SelectedValue.ToString()) && lPropriedades.Select(x => x.Name).ToList().Contains(lComboBox.Name))
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
                    if (lValue != null && lPropriedades.Select(x => x.Name).ToList().Contains(lCheckBox.Name.Split('_').FirstOrDefault()) && lCheckBox.IsChecked == true)
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lCheckBox.Name.Split('_').FirstOrDefault()).First();

                        lPropriedade.SetValue(lRetorno, Convert.ChangeType(lCheckBox.GetValue(lValue).ToString(), lPropriedade.PropertyType));
                    }
                    else
                    {
                        DependencyProperty lRequired = DependencyObjectHelper.GetDependencyPropertyByName("Required", lCheckBox.Parent);

                        if (lRequired != null)
                        {
                            GetControlsList(lCheckBox.Parent, lParents);
                            if (lParents.Count == 0)
                            {
                                pErrosValidacao.Add(string.Concat("O campo ", ((GroupBox)((Grid)lCheckBox.Parent).Parent).Header.ToString(), " é obrigatório."));
                            }
                        }
                    }
                }
                else if (lSender is RadioButton)
                {
                    RadioButton lRadioButton = (RadioButton)lSender;
                    DependencyProperty lValue = DependencyObjectHelper.GetDependencyPropertyByName("Value", lRadioButton);
                    if (lValue != null && lPropriedades.Select(x => x.Name).ToList().Contains(lRadioButton.Name.Split('_').FirstOrDefault()) && lRadioButton.IsChecked == true)
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lRadioButton.Name.Split('_').FirstOrDefault()).First();

                        lPropriedade.SetValue(lRetorno, Convert.ChangeType(lRadioButton.GetValue(lValue).ToString(), lPropriedade.PropertyType));
                    }
                    else
                    {
                        DependencyProperty lRequired = DependencyObjectHelper.GetDependencyPropertyByName("Required", lRadioButton.Parent);

                        if (lRequired != null)
                        {
                            GetControlsList(lRadioButton.Parent, lParents);
                            if (lParents.Count == 0)
                            {
                                pErrosValidacao.Add(string.Concat("O campo ", ((GroupBox)((Grid)lRadioButton.Parent).Parent).Header.ToString(), " é obrigatório."));
                            }
                        }
                    }
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
            GetControlsList(pWindow, lChildren, true, true);
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
                            lComboBox.SelectedValue = lPropriedade.GetValue(pDM).ToString();
                        else
                            lComboBox.SelectedValue = string.Empty;
                    }
                }
                else if (lSender is CheckBox)
                {
                    CheckBox lCheckBox = (CheckBox)lSender;
                    if (lPropriedades.Select(x => x.Name).ToList().Contains(lCheckBox.Name.Split('_').FirstOrDefault()))
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lCheckBox.Name.Split('_').FirstOrDefault()).First();
                        if (lPropriedade.GetValue(pDM) != null && lPropriedade.GetValue(pDM).ToString() == lCheckBox.Name.Split('_').LastOrDefault())
                            lCheckBox.IsChecked = true;
                        else
                            lCheckBox.IsChecked = false;
                    }
                }
                else if (lSender is RadioButton)
                {
                    RadioButton lComboBox = (RadioButton)lSender;
                    if (lPropriedades.Select(x => x.Name).ToList().Contains(lComboBox.Name.Split('_').FirstOrDefault()))
                    {
                        lPropriedade = lPropriedades.Where(x => x.Name == lComboBox.Name.Split('_').FirstOrDefault()).First();
                        if (lPropriedade.GetValue(pDM) != null && lPropriedade.GetValue(pDM).ToString() == lComboBox.Name.Split('_').LastOrDefault())
                            lComboBox.IsChecked = true;
                        else
                            lComboBox.IsChecked = false;
                    }
                }
            }
        }

        private void GetControlsList(DependencyObject pControl, List<Object> pChildren, bool pNoCheck = false, bool pNoVisibilityCheck = false)
        {   
            int lChildNumber = VisualTreeHelper.GetChildrenCount(pControl);

            for (int i = 0; i <= lChildNumber - 1; i++)
            {
                var lChild = VisualTreeHelper.GetChild(pControl, i);

                if (lChild is TextBox && (pNoVisibilityCheck || ((TextBox)lChild).IsVisible))
                    pChildren.Add(lChild as TextBox);
                else if (lChild is ComboBox && (pNoVisibilityCheck || ((ComboBox)lChild).IsVisible))
                    pChildren.Add(lChild as ComboBox);
                else if (lChild is CheckBox && (pNoCheck || (bool)((CheckBox)lChild).IsChecked) && (pNoVisibilityCheck || ((CheckBox)lChild).IsVisible))
                    pChildren.Add(lChild as CheckBox);
                else if (lChild is RadioButton && (pNoCheck || (bool)((RadioButton)lChild).IsChecked) && (pNoVisibilityCheck || ((RadioButton)lChild).IsVisible))
                    pChildren.Add(lChild as RadioButton);

                if (VisualTreeHelper.GetChildrenCount(lChild) > 0)
                {
                    GetControlsList(lChild, pChildren, pNoCheck, pNoVisibilityCheck);
                }
            }
        }

        #endregion
    } 
}
