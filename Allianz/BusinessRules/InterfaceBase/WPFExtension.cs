﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup.Primitives;

namespace InterfaceBase
{
    //Classe criada como extensão dp WPF, nela serão criados os atributos personalizados que serão usados nos controls do xaml
    public class WPFExtension
    {        
        #region Relative field code será a propriedade adicionada a um control que possua campos relacionados a seu valor
        public static readonly DependencyProperty RelativeFieldCodeProperty = DependencyProperty.RegisterAttached("RelativeFieldCode", typeof(string), typeof(WPFExtension), new PropertyMetadata(default(string)));
        
        public static void SetRelativeFieldCode(UIElement element, string value)
        {
            element.SetValue(RelativeFieldCodeProperty, value);
        }

        public static string GetRelativeFieldCode(UIElement element)
        {
            return (string)element.GetValue(RelativeFieldCodeProperty);
        }
        #endregion

        #region Relative field será a propriedade adicionada a um control que esteja relacionado ao valor de outro campo
        public static readonly DependencyProperty RelativeFieldProperty = DependencyProperty.RegisterAttached("RelativeField", typeof(string), typeof(WPFExtension), new PropertyMetadata(default(string)));

        public static void SetRelativeField(UIElement element, string value)
        {
            element.SetValue(RelativeFieldProperty, value);
        }

        public static string GetRelativeField(UIElement element)
        {
            return (string)element.GetValue(RelativeFieldProperty);
        }
        #endregion

        #region Required será a propriedade adicionada a um control obrigatório
        public static readonly DependencyProperty RequiredProperty = DependencyProperty.RegisterAttached("Required", typeof(string), typeof(WPFExtension), new PropertyMetadata(default(string)));

        public static void SetRequired(UIElement element, string value)
        {
            element.SetValue(RequiredProperty, value);
        }

        public static string GetRequired(UIElement element)
        {
            return (string)element.GetValue(RequiredProperty);
        }
        #endregion

        #region Refers deve ser adicionada aos labels que forem ligados a um campo obrigatório
        public static readonly DependencyProperty RefersProperty = DependencyProperty.RegisterAttached("Refers", typeof(string), typeof(WPFExtension), new PropertyMetadata(default(string)));

        public static void SetRefers(UIElement element, string value)
        {
            element.SetValue(RefersProperty, value);
        }

        public static string GetRefers(UIElement element)
        {
            return (string)element.GetValue(RefersProperty);
        }
        #endregion

        #region Value para CheckBox e RadioButton
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(string), typeof(WPFExtension), new PropertyMetadata(default(string)));

        public static void SetValue(UIElement element, string value)
        {
            element.SetValue(ValueProperty, value);
        }

        public static string GetValue(UIElement element)
        {
            return (string)element.GetValue(ValueProperty);
        }
        #endregion
    }

    #region Esta classe implementa o método necessário para encontrar um control que tenha uma propriedade vinculada    
    public static class DependencyObjectHelper
    {
        public static IList<DependencyObject> GetDependencyObjectsWithProperty(DependencyObject pDependencyObject, string pPropertyName)
        {
            var lDependencyObjectList = new List<DependencyObject>();

            GetDependencyObjectsWithPropertyRecursive(pPropertyName, pDependencyObject, lDependencyObjectList);

            return lDependencyObjectList;
        }

        private static void GetDependencyObjectsWithPropertyRecursive(string pPropertyName, DependencyObject pDependencyObject, ICollection<DependencyObject> pSources)
        {
            var lDependencyPropertyList = new List<DependencyProperty>();
            lDependencyPropertyList.AddRange(MarkupWriter.GetMarkupObjectFor(pDependencyObject).Properties.Where(x => x.DependencyProperty != null).Select(x => x.DependencyProperty).ToList());
            lDependencyPropertyList.AddRange(MarkupWriter.GetMarkupObjectFor(pDependencyObject).Properties.Where(x => x.IsAttached && x.DependencyProperty != null).Select(x => x.DependencyProperty).ToList());

            if (lDependencyPropertyList.Select(x => x.Name).Contains(pPropertyName))
            {
                pSources.Add(pDependencyObject);
            }

            var lChildren = LogicalTreeHelper.GetChildren(pDependencyObject).OfType<DependencyObject>().ToList();
            if (lChildren.Count == 0)
                return;

            foreach (var lChild in lChildren)
            {
                GetDependencyObjectsWithPropertyRecursive(pPropertyName, lChild, pSources);
            }
        }
        public static DependencyProperty GetDependencyPropertyByName(string pPropertyName, DependencyObject pDependencyObject)
        {
            DependencyProperty lDependencyProperty = null;
            var lDependencyPropertyList = new List<DependencyProperty>();
            lDependencyPropertyList.AddRange(MarkupWriter.GetMarkupObjectFor(pDependencyObject).Properties.Where(x => x.DependencyProperty != null).Select(x => x.DependencyProperty).ToList());
            lDependencyPropertyList.AddRange(MarkupWriter.GetMarkupObjectFor(pDependencyObject).Properties.Where(x => x.IsAttached && x.DependencyProperty != null).Select(x => x.DependencyProperty).ToList());

            lDependencyProperty = lDependencyPropertyList.Where(x => x.Name == pPropertyName).FirstOrDefault();
            return lDependencyProperty;
        }
    }
    #endregion
    
}
