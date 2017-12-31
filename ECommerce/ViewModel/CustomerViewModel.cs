using ECommerce.Model;
using ECommerce.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

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
        public Visibility IsBusy { get; set; }
        public ProgressModel progressModel { get; set; } 
        public CustomerViewModel()
        {

            progressModel= new ProgressModel() { IsBusy = false };
            customer = new Customer() { ID=1,Name="Mohsin",IsChecked=false};

            Customers = new ObservableCollection<Customer>();
            Customers.CollectionChanged += Customers_CollectionChanged;

            ButtonCommand = new DelegateCommand(Display);
            GridDoubleClick = new DelegateCommand(DoubleClick);
            GridRightClick = new DelegateCommand(RightClick);
            SaveCommand = new DelegateCommandT<object>(ExecSaveCommand);

            int[] arr = new int[8] {1,5,0,9,7,4,10,4};
          
            int max=0, nmax=0;
            for (int i = 0; i < arr.Length; i++)
            {
               // nmax = arr[i];
                if (arr[i]>max)
                {
                    nmax = max;
                    max = arr[i];
                }
                if (i==0)
                {
                    nmax = arr[i];
                }
                
            }
            int p=0;
        }

        private void ExecSaveCommand(object param)
        {
            CustomerClient cc = new CustomerClient("ep2");
            cc.ClientCredentials.UserName.UserName = "a";
            cc.ClientCredentials.UserName.Password = "a";
            //cc. = System.Net.CredentialCache.DefaultCredentials;
            var x=cc.GetOrderOfCustomer(1);
            //Customers.Where(s => s.ID == 1).FirstOrDefault().Name = param.ToString();
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

            //IDialogService<ObservableCollection<Customer>> dv = new DialogService<ObservableCollection<Customer>>();
            //dv.ShowDialog(Customers, new ChildWindow());

            try
            {
                progressModel.IsBusy = true;
                HttpWebRequest req = HttpWebRequest.CreateHttp("http://localhost:91/CustomerService.svc/GetCustomer/1");
                req.Method = "GET";
                req.ContentType = "application/json";
                req.BeginGetResponse(new AsyncCallback(WebResponse), req);

                HttpWebRequest req2 = HttpWebRequest.CreateHttp("http://localhost:91/CustomerService.svc/GetCustomerlist/1");
                req2.Method = "GET";
                byte[] bytes = Encoding.UTF8.GetBytes("test:test");
                string base64 = Convert.ToBase64String(bytes);
                req2.Headers[HttpRequestHeader.Authorization] = base64;
                req2.ContentType = "application/json";
                req2.Credentials = new NetworkCredential("test", "test");
                req2.BeginGetResponse(new AsyncCallback(WebResponseList), req2);
                

                #region GET
                //HttpWebRequest re = HttpWebRequest.CreateHttp("http://localhost:91/CustomerService.svc/GetCustomer/1");
                //re.Method = "GET";
                //re.ContentType = "application/json";
                //re.ContentLength = 0;
                //HttpWebResponse response = re.GetResponse() as HttpWebResponse;                
                //Stream st = response.GetResponseStream();                
                //string content = string.Empty;
                ////using (Stream stream = response.GetResponseStream())
                ////{
                ////    using (StreamReader sr = new StreamReader(stream))
                ////    {
                ////        content = sr.ReadToEnd();
                ////    }
                ////}
                //DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(Customer));
                //Customer emp = (Customer)ds.ReadObject(st);
                #endregion

                //int j = 0;
                //tk.Wait();
                //Customers.Add(tk.Result);
            }
            catch (Exception ex)
            {

                //throw;
            }
        }



        public async Task<Customer> GetResponse(HttpWebRequest req)
        {
            WebResponse response = await req.GetResponseAsync();
            Stream st = response.GetResponseStream();
            string content = string.Empty;            
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(Customer));
            Customer emp = (Customer)ds.ReadObject(st);
           // Customers.Add(tk.Result);
            return emp;
        }
        private void WebResponse(IAsyncResult callbackResult)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)callbackResult.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(callbackResult);
                string responseString = string.Empty;
                Stream streamResponse = response.GetResponseStream();
                StreamReader reader = new StreamReader(streamResponse);
                //responseString = reader.ReadToEnd();
                DataContractJsonSerializer dc = new DataContractJsonSerializer(typeof(Customer));
                Customer ob = (Customer)dc.ReadObject(streamResponse); 
                streamResponse.Flush();
                Dispatcher.CurrentDispatcher.BeginInvoke(
                   (Action) (()=> {
                       Customers.Add(ob);
                   })
                    );
                
            }
            catch (Exception ex)
            {

                
            }
        }

        private void WebResponseList(IAsyncResult callbackResult)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)callbackResult.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(callbackResult);
                string responseString = string.Empty;
                Stream streamResponse = response.GetResponseStream();
                StreamReader reader = new StreamReader(streamResponse);
                //responseString = reader.ReadToEnd();
                DataContractJsonSerializer dc = new DataContractJsonSerializer(typeof(List<Customer>));
                List<Customer> ob = (List<Customer>)dc.ReadObject(streamResponse);
                streamResponse.Flush();

                progressModel.IsBusy = false;
            }
            catch (Exception ex)
            {


            }
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
    public static class ExtnClass
    {
        static string StringEx(this string st)
        {
            return st.Substring(0, 1);
        }
    }
}
