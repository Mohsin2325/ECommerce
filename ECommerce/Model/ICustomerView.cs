using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model
{
    public interface ICustomerView
    {
        ObservableCollection<Customer> Customers { get; set; }
        bool CanAccess(ICustomerView cc);
    }
}
