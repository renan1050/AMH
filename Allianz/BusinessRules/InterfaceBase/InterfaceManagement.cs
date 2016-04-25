using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InterfaceBase
{
    public class InterfaceManagement
    {        
        #region MÉTODO PARA A EXIBIÇÃO/ESCONDAÇÃO DO CAMPO CODIGO E DO CARREGAMENTO DE UM REGISTRO
        public void LoadByValue(string pValue, Window pWindow, Action pLoadAction = null, string pLoadValue = null)
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
                                    pLoadAction();                                
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
    } 
}
