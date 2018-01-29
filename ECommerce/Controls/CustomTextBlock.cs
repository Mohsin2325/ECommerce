using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ECommerce.Controls
{
   public class CustomTextBlock: ContentControl
    {
        TextBlock _textBlock { get; set; }
        protected TextBlock TextBlock
        {
            get
            {
                return _textBlock;
            }
            set
            {
                _textBlock = value;
            }
        }
        public static readonly DependencyProperty TextValueProperty = DependencyProperty.Register("TextValue", typeof(string), typeof(CustomTextBlock), null);

        
        

    }

    
}
