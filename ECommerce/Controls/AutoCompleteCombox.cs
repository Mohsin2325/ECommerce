using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ECommerce.Controls
{
    //[TemplatePart(Name = "PART_ListBox", Type =typeof(ListBox))]
    //[TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    public class AutoCompleteCombox: Control
    {

        //TextBlock _textBlock { get; set; }
        //protected TextBlock TextBlock
        //{
        //    get
        //    {
        //        return _textBlock;
        //    }
        //    set
        //    {
        //        _textBlock = value;
        //    }
        //}
        //public static readonly DependencyProperty TextValueProperty = DependencyProperty.Register("TextValue", typeof(string), typeof(CustomTextBlock), null);
        ListBox _listBox;
        TextBox _textbox;
        //IEnumerable SourceCollection;
        public AutoCompleteCombox()
        {
            
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _listBox = this.Template.FindName("PART_ListBox",this) as ListBox;
            _textbox= this.Template.FindName("PART_TextBox", this) as TextBox;
            if (_textbox!=null)
            {
                _textbox.TextChanged += _textbox_TextChanged;
            }
        }

        private void _textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        public static readonly DependencyProperty SourceCollectionProperty = DependencyProperty.Register("SourceCollection", typeof(IEnumerable), typeof(AutoCompleteCombox), new PropertyMetadata(ItemsSourcePropertyChangedCallback));
        public IEnumerable SourceCollection
        {
            get
            {
                return (IEnumerable)GetValue(SourceCollectionProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
                
            }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(AutoCompleteCombox),new PropertyMetadata(ItemsSourcePropertyChangedCallback));
        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
                SourceCollection = ItemsSource;
            }
        }

        private static void ItemsSourcePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
           // e.NewValue
            AutoCompleteCombox box = sender as AutoCompleteCombox;
            //foreach (var item in Items)
            //{

            //}        
        }

        private void SetFilter()
        {
            FilteredItems = ItemsSource;
        }
        public IEnumerable FilteredItems
        {
            get { return (IEnumerable)GetValue(FilteredItemsProperty); }
            set { SetValue(FilteredItemsProperty, value); }
        }
        public static readonly DependencyProperty FilteredItemsProperty =
            DependencyProperty.Register("FilteredItems", typeof(IEnumerable), typeof(AutoCompleteCombox), new PropertyMetadata(null));

        


    }


}
