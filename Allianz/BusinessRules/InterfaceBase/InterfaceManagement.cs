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
            List<TextBox> lChildren = new List<TextBox>();
            GetControlsList(pWindow,lChildren);
            PropertyInfo lPropriedade;
            foreach (TextBox lText in lChildren)
            {                
                if(!string.IsNullOrEmpty(lText.Text) && lPropriedades.Select(x => x.Name).ToList().Contains(lText.Name))
                {
                    lPropriedade = lPropriedades.Where(x => x.Name == lText.Name).First();

                    lPropriedade.SetValue(lRetorno, Convert.ChangeType(lText.Text, lPropriedade.PropertyType));
                }
                else
                {
                    DependencyProperty lRequired = DependencyObjectHelper.GetDependencyPropertyByName("Required", lText);                                        

                    if(lRequired != null && lText.GetValue(lRequired).ToString().Split(';').Contains(pEnviador))
                    {
                        var lLabel = lLabels.Where(x => x.GetValue(WPFExtension.RefersProperty).Equals(lText.Name)).FirstOrDefault();
                        pErrosValidacao.Add(string.Concat("O campo ", (lLabel != null ? ((Label)lLabel).Content.ToString() : lText.Name), " é obrigatório."));                        
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

            List<TextBox> lChildren = new List<TextBox>();
            GetControlsList(pWindow, lChildren);
            PropertyInfo lPropriedade;
            foreach (TextBox lText in lChildren)
            {
                if (lPropriedades.Select(x => x.Name).ToList().Contains(lText.Name))
                {
                    lPropriedade = lPropriedades.Where(x => x.Name == lText.Name).First();
                    if (lPropriedade.GetValue(pDM) != null)
                        lText.Text = lPropriedade.GetValue(pDM).ToString();
                    else
                        lText.Text = string.Empty;
                }
            }
        }

        private void GetControlsList(DependencyObject pControl,List<TextBox> pChildren)
        {   
            int lChildNumber = VisualTreeHelper.GetChildrenCount(pControl);

            for (int i = 0; i <= lChildNumber - 1; i++)
            {
                var lChild = VisualTreeHelper.GetChild(pControl, i);

                if (lChild is TextBox)
                    pChildren.Add(lChild as TextBox);

                if (VisualTreeHelper.GetChildrenCount(lChild) > 0)
                {
                    GetControlsList(lChild, pChildren);
                }
            }
        }

        #endregion
    } 
}
