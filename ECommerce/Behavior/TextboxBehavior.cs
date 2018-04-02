using ECommerce.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ECommerce.Behavior
{
   public class TextboxBehavior: Behavior<UIElement>
    {
        public static readonly DependencyProperty CorrectTextboxProperty =
    DependencyProperty.RegisterAttached("CorrectTextbox", typeof(string), typeof(TextboxBehavior),
                      new PropertyMetadata("",new PropertyChangedCallback(AttachTexchangeEvent)));

        public static string GetCorrectTextboxProperty(DependencyObject obj)
        {
            return (string)obj.GetValue(CorrectTextboxProperty);
        }

        public static void SetCorrectTextboxProperty(DependencyObject obj, string value)
        {
            obj.SetValue(CorrectTextboxProperty, value);
        }
        public static void AttachTexchangeEvent(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            TextBox tx = obj as TextBox;
            tx.TextChanged += (sender,e)=> 
            {
                string tp = "";
                if (tx.Text.Any(char.IsDigit))
                {
                    int i = 0;
                    tx.Text.ToCharArray().ToList().ForEach( s=>
                    {
                        if (!Int32.TryParse(s.ToString(), out i))
                        {


                            tp = tp + s.ToString();
                        }
                    }
                        );        
                }
                tx.Text = tp;
            };
        }

        private static void Tx_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        protected override void OnAttached()
        {
            TextBox bx = this.AssociatedObject as TextBox;
            bx.TextChanged += Bx_TextChanged;
          
        }

        private void Bx_TextChanged(object sender, TextChangedEventArgs e)
        {
            int i;
            if (String.IsNullOrEmpty( (sender as TextBox).Text))
            {
                (sender as TextBox).Background = System.Windows.Media.Brushes.Red;
            }
        }

        protected override void OnDetaching()
        {
            
        }
    }




    public class DynamicDataTemplateSelector : DataTemplateSelector
    {
        #region Constructors
        /*
         * The default constructor
         */
        public DataTemplate data1 { get; set; }
        public DataTemplate data2 { get; set; }

        public DynamicDataTemplateSelector()
        {
        }
        #endregion

        #region Functions
        public override DataTemplate SelectTemplate(object inItem, DependencyObject inContainer)
        {
            Customer row = inItem as Customer;

            if (row != null)
            {
                FrameworkElement window = inContainer as FrameworkElement;
                if (row.ID==1)
                {
                    return (DataTemplate)window.FindResource("DataTemplateKey1");
                }
                else
                {
                    return (DataTemplate)window.FindResource("DataTemplateKey2");
                }
                
                //if (row.DataView.Table.Columns.Contains("IntVal"))
                //{
                //    if ((int)row["IntVal"] > 0)
                //    {
                //        return (DataTemplate)w.FindResource("FixThisTemplate");
                //    }
                //}
                //return (DataTemplate)w.FindResource("NormalTemplate");
            }
            return null;
        }
        #endregion
    }
    }
