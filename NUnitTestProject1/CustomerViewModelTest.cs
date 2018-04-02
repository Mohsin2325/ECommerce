using System;
using NUnit.Framework;
using ECommerce.ViewModel;
using ECommerce.Model;
using System.Collections.ObjectModel;
using Moq;

namespace NUnitTestProject1
{
    [TestFixture]
    public class CustomerViewModelTest
    {
        CustomerViewModel cust;
        [SetUp]
        public void Initialize()
        {
            cust = new CustomerViewModel();
            //cust.Customers.Add(new Customer() { ID =1 ,Name="abc" });
            
            //cc.Add(new Customer() { ID = 3, Name = "MH", IsChecked = true });
            
            
         }
        [Test]
        public void TestMethod1()
        {
            ObservableCollection<Customer> cc = new ObservableCollection<Customer>();
            cc.Add(new Customer() { ID = 3, Name = "MH", IsChecked = true });
            var v = new Mock<ICustomerView>();
            //v.SetupProperty(c=>c.Customers,new System.Collections.ObjectModel.ObservableCollection<Customer>()  .Add(new Customer() { ID = 3, Name = "MH", IsChecked = true }));
            v.Setup(c=>c.CanAccess(It.IsAny<ICustomerView>())).Returns(true);
            //cust.customer = v.Object as Customer;
            //cust.CanAccess
            cust.RightClickForunitTest(v.Object);
            Assert.AreEqual(cust.Customers[0].ID,cc[0].ID);
        }
        
    }
}