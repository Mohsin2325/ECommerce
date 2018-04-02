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
       public DelegateCommand ButtonClickCommand { get; set; }
        ObservableCollection<Customer> customers { get; set; }
        public ChildWindowViewModel(object datacontext)
        {
            ButtonClickCommand = new DelegateCommand(ExceuteCLick);
            customers = datacontext as ObservableCollection<Customer>;
        }
       private void ExceuteCLick()
        {
            customers.Add(new Customer() { ID = 2, Name = "Shaikh", IsChecked = false });
        }
    }
}
