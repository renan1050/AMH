using System;
using System.Collections.Generic;
using System.Linq;
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
    }
    #endregion
}
