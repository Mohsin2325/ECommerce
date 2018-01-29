using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ECommerce.Converters
{
    public class AttributeDictionary :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                    new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        private Dictionary<string, string> _displatAttribute = new Dictionary<string, string>();
        public Dictionary<string, string> DisplayAttribute
        {
            get
            {
                return _displatAttribute;
            }
            set
            {
                _displatAttribute = value;
                OnPropertyChanged("DisplayAttribute");
            }
        }

        

        public void GetAttributes(Type type)
        {
            foreach (var item in type.GetProperties())
            {
                DisplayAttribute v2 = item.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                if (!DisplayAttribute.ContainsKey(item.Name))
                {
                    
                    //var v1= item.GetCustomAttribute<DisplayNameAttribute>(false);
                    DisplayAttribute.Add(item.Name, v2!=null?v2.Name:"");
                }
            }
        }

        public static string GetPropertyDisplayName(Expression<Func<object>> expression)
        {
            MemberExpression propertyExpression = (MemberExpression)expression.Body;
            MemberInfo propertyMember = propertyExpression.Member;

            Object[] displayAttributes = propertyMember.GetCustomAttributes(typeof(DisplayAttribute), true);
            if (displayAttributes != null && displayAttributes.Length == 1)
                return ((DisplayAttribute)displayAttributes[0]).Name;

            return propertyMember.Name;
        }

    }
    public class DisplayAttributeConverter : IMultiValueConverter
    {
        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    if (value!=null)
        //    {
        //        //var v=GetPropertyName<Type>()
        //        //TextBlock tb =targetType as TextBlock;
        //        //targetType.Get
        //        foreach(var s in targetType.GetMethods())
        //        {
        //           var m= s.Name;
        //        }
        //    }
        //    return "shaikh";
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    return null;
        //}

        public static string GetMemberName<TType, TMemberType>(Expression<Func<TType, TMemberType>> memberAccessExpression)
        {
            MemberExpression memberExpression = memberAccessExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                UnaryExpression unaryExpression = memberAccessExpression.Body as UnaryExpression;

                if (unaryExpression != null)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;

                    if (memberExpression == null)
                    {
                        throw new NotSupportedException();
                    }
                }
                else
                {
                    throw new NotSupportedException();
                }
            }

            //MemberExpression nm = GetMemberExpression(member);

            return GetPropertyDisplayName(memberExpression.Member as PropertyInfo);

            //return memberExpression.Member.Name;
        }
        public static string GetPropertyDisplayName(PropertyInfo propInfo)
        {
            string propertyDisplayName;
            DisplayAttribute attr = propInfo.GetCustomAttribute<DisplayAttribute>();
            if (attr == null)
            {
                propertyDisplayName = propInfo.Name;
            }
            else
            {
                propertyDisplayName = attr.Name;
            }

            return propertyDisplayName;
        }
        public static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            MemberExpression propertyExpression = (MemberExpression)expression.Body;
            MemberInfo propertyMember = propertyExpression.Member;

            Object[] displayAttributes = propertyMember.GetCustomAttributes(typeof(DisplayAttribute), true);
            if (displayAttributes != null && displayAttributes.Length == 1)
                return ((DisplayAttribute)displayAttributes[0]).Name;

            return propertyMember.Name;
        }

        TextBlock myControl;
        object theValue;

      

        public object Convert(object[] values, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            myControl = values[1] as TextBlock;
            theValue = values[0];
            BindingExpression v= myControl.GetBindingExpression(TextBlock.TextProperty);
           //myControl.ApplyTemplate()

            return myControl.Name + " : " + values[1];
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object[] { null, myControl.Name + " : " + theValue };
        }
    }

    public class DisplayNameHelper
    {
        public string GetDisplayName(object obj, string propertyName)
        {
            if (obj == null) return null;
            return GetDisplayName(obj.GetType(), propertyName);

        }

        public string GetDisplayName(Type type, string propertyName)
        {
            var property = type.GetProperty(propertyName);
            if (property == null) return null;

            return GetDisplayName(property);
        }

        public string GetDisplayName(PropertyInfo property)
        {
            var attrName = GetAttributeDisplayName(property);
            if (!string.IsNullOrEmpty(attrName))
                return attrName;

            var metaName = GetMetaDisplayName(property);
            if (!string.IsNullOrEmpty(metaName))
                return metaName;

            return property.Name.ToString();
        }

        private string GetAttributeDisplayName(PropertyInfo property)
        {
            var atts = property.GetCustomAttributes(
                typeof(DisplayNameAttribute), true);
            if (atts.Length == 0)
                return null;
            return (atts[0] as DisplayNameAttribute).DisplayName;
        }

        private string GetMetaDisplayName(PropertyInfo property)
        {
            var atts = property.DeclaringType.GetCustomAttributes(
                typeof(MetadataTypeAttribute), true);
            if (atts.Length == 0)
                return null;

            var metaAttr = atts[0] as MetadataTypeAttribute;
            var metaProperty =
                metaAttr.MetadataClassType.GetProperty(property.Name);
            if (metaProperty == null)
                return null;
            return GetAttributeDisplayName(metaProperty);
        }
    }

    
}
