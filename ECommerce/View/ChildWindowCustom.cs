using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ECommerce.View
{
    public partial class ChildWindowCustom : Window,IChildWindow
    {
        public static readonly DependencyProperty OnClosedActionProperty = DependencyProperty.Register("OnClosedAction", typeof(Action<bool?>), typeof(ChildWindowCustom), null);
        public Action<bool?> OnClosedAction
        {
            get { return (Action<bool?>)GetValue(OnClosedActionProperty); }
            set { SetValue(OnClosedActionProperty, value); }
        }
    }
    interface IChildWindow {
        //public Action<bool?> OnClosedAction();
    }
}
