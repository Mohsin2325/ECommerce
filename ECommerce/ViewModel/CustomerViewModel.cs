using ECommerce.Model;
using ECommerce.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ViewModel
{
   public  class CustomerViewModel : ViewModelBase
    {
       public  Customer customer { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public int ID = 1;
        public DelegateCommand ButtonCommand { get; set; }
        public DelegateCommand GridDoubleClick { get; set; }
        public DelegateCommand GridRightClick { get; set; }
        public DelegateCommandT<object> SaveCommand { get; set; }
        public CustomerViewModel()
        {

            customer = new Customer() { ID=1,Name="Mohsin"};

            Customers = new ObservableCollection<Customer>();
            Customers.CollectionChanged += Customers_CollectionChanged;

            ButtonCommand = new DelegateCommand(Display);
            GridDoubleClick = new DelegateCommand(DoubleClick);
            GridRightClick = new DelegateCommand(RightClick);
            SaveCommand = new DelegateCommandT<object>(ExecSaveCommand);
        }

        private void ExecSaveCommand(object param)
        {
            Customers.Where(s => s.ID == 1).FirstOrDefault().Name = param.ToString();
        }
        private void Customers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        void RightClick()
        {
            Customers.Add(new Customer() { ID = 3, Name = "MH", IsChecked = true });
        }

        void DoubleClick()
        {
            //Customers.Add(new Customer() { ID = 3, Name = "MH", IsChecked = true });

            IDialogService<ObservableCollection<Customer>> dv = new DialogService<ObservableCollection<Customer>>();
            dv.ShowDialog(Customers, new ChildWindow());
        }

        void Display()
        {
            //Customers = null
            Customers.Clear();
            Customers.Add(new Customer() { ID = 1, Name = "Mohsin", IsChecked = true });
            Customers.Add(new Customer() { ID = 2, Name = "Shaikh", IsChecked = false });
        }

        bool CanExecute()
        {
            return true; 
        }
    }
}
