using ECommerce.Model;
using ECommerce.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ECommerce
{
    /// <summary>
    /// Interaction logic for ChildWindow.xaml
    /// </summary>
    public partial class ChildWindow
    {
        public ChildWindow()
        {
            InitializeComponent();
            var v = this.DataContext;
        }

        //private void textBox_Click(object sender, RoutedEventArgs e)
        //{
        //    if (this.DataContext!=null)
        //    {
        //        CustomerViewModel vm = this.DataContext as CustomerViewModel;
        //        vm.Customers.Clear();
        //        vm.Customers.Add( new Customer() { Name = "Rehman", ID = 1 });
                
        //    }
        //}
    }
}
