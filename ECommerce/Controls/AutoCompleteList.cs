using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace ECommerce.Controls
{
    public class AutoCompleteList : ListBox
    {
        public static readonly DependencyProperty MaxVisibleItemsProperty = DependencyProperty.Register("MaxVisibleItems", typeof(int), typeof(AutoCompleteList), new PropertyMetadata(5, new PropertyChangedCallback(OnMaxVisibleItemsChanged)));

        public int MaxVisibleItems
        {
            get
            {
                return (int)GetValue(MaxVisibleItemsProperty);
            }
            set
            {
                SetValue(MaxVisibleItemsProperty, value);
            }
        }

        public AutoCompleteList()
        {
            DefaultStyleKey = typeof(AutoCompleteList);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = base.MeasureOverride(availableSize);

            ////get number of containers to be displayed to see if the list size needs to be maintained
            //IEnumerable<ListBoxItem> containers = this.GetContainers().Cast<ListBoxItem>();
            ////System.Diagnostics.Debug.WriteLine(containers.Count() + " containers");
            //ScrollViewer scrollViewer = this.GetScrollHost();
            //containers.ToList()
            //    .ForEach
            //    (
            //        c => {
            //            //remove any existing tooltips so that we don't have any left from items that aren't 
            //            //currently visible
            //            if (ToolTipService.GetToolTip(c) != null)
            //            {
            //                ToolTipService.SetToolTip(c, null);
            //            }
            //        }
            //    );

            //ListBoxItem firstElement = containers.FirstOrDefault();
            //if (firstElement != null)
            //{
            //    //adjust item containers with long contents for visibility
            //    containers.Take(MaxVisibleItems).ToList()
            //        .ForEach
            //        (
            //            c => {
            //                if (scrollViewer.ExtentWidth > scrollViewer.ViewportWidth)
            //                {
            //                    //if too long, then add tooltip to display the full contents
            //                    if (ToolTipService.GetToolTip(c) != null)
            //                    {
            //                        ToolTip toolTip = new ToolTip();
            //                        toolTip.Content = c.Content;
            //                        toolTip.ContentTemplate = Application.Current.Resources["AutoCompleteListToolTipTemplate"] as DataTemplate;
            //                        ToolTipService.SetToolTip(c, null);
            //                    }
            //                }
            //            }
            //        );
            //}

            return size;
        }

        private static void OnMaxVisibleItemsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            AutoCompleteList d = obj as AutoCompleteList;
            if (d.MaxVisibleItems < 1)
            {
                d.MaxVisibleItems = (int)e.OldValue;
                throw new InvalidOperationException("MaxVisibleItems must be greater than or equal to 1");
            }
        }
    }
}