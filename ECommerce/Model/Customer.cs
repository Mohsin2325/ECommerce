using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ECommerce.Model
{
    [DataContract (Name ="Customer"),Serializable]
    public class Customer:INotifyPropertyChanged
    {
        private string _name;
        private int _id;
        private bool _isChecked;

        public Customer()
        {
            //_displatAttribute = new Dictionary<string, string>();
            //GetAttributes(typeof(Customer));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                    new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        //private Dictionary<string, string> _displatAttribute;
        //public Dictionary<string, string> DisplayAttribute
        //{
        //    get
        //    {
        //        return _displatAttribute;
        //    }
        //}



        //public void GetAttributes(Type type)
        //{
        //    foreach (var item in type.GetProperties())
        //    {
        //        DisplayAttribute v2 = item.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
        //        if (!_displatAttribute.ContainsKey(item.Name))
        //        {

        //            //var v1= item.GetCustomAttribute<DisplayNameAttribute>(false);
        //            _displatAttribute.Add(item.Name, v2 != null ? v2.Name : "");
        //        }
        //    }
        //    OnPropertyChanged("DisplayAttribute");
        //}

        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        
        [Display(Name="First Name",Description ="Name details")]
        [DataMember(Name ="firstname")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        [DataMember(Name = "lastname")]
        public string LastName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("LastName");
            }
        }
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

    }
}
