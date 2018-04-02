using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ECommerce.Model
{
    public class ViewModelBase 
    {
        // public string Title { get; set; }
        //public event EventHandler CanExecuteChanged;

        //public bool CanExecute(object parameter)
        //{
        //    return true;
        //}

        //public void Execute(object parameter)
        //{

        //}

        private Action<bool, object> _onActionClosed;
        public Action<bool, object> OnCloseAction
        {
            get { return _onActionClosed; }
            set { _onActionClosed = value; }
        }

    }
}
