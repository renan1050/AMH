using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceBase
{
    public class WPFExtension
    {
        public static readonly DependencyProperty RelativeFieldCodeProperty =
        DependencyProperty.RegisterAttached("RelativeFieldCode", typeof(double), typeof(WPFExtension), new PropertyMetadata(default(string)));

        public static void SetRelativeFieldCode(UIElement element, double value)
        {
            element.SetValue(RelativeFieldCodeProperty, value);
        }

        public static string GetRelativeFieldCode(UIElement element)
        {
            return (string)element.GetValue(RelativeFieldCodeProperty);
        }
    }
}
