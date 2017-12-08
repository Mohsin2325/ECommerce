using ECommerce.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ViewModel
{
    public class ChildWindowViewModel : ViewModelBase
    {
        public object DataContext { get; set; }
        

        public ChildWindowViewModel(object datacontext)
        {
        }
    }
}
