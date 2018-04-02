using ECommerce.View;
using ECommerce.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ECommerce.Model
{
    interface IDialogService
    {
        MessageBoxResult ShowMessageBox(string content,string title, MessageBoxButton buttons);

        void ShowDialog(Type viewModel, ChildWindow dialog, Action<bool, object> Onclose = null);

        void ShowDialog(Type viewModel, ChildWindow dialog,dynamic context, Action<bool, object> Onclose = null);

        Type ViewModel { get; set; }

        Window DialogWindow { get; set; }
    }

    public class DialogService : IDialogService
    {
        public Type ViewModel { get; set; }
        public Window DialogWindow { get; set; }
        public MessageBoxResult ShowMessageBox(string content,

        string title, MessageBoxButton buttons)
        {
            return MessageBox.Show(content, title, buttons);
        }


        public void ShowDialog(Type viewModel, ChildWindow dialog, Action<bool, object> Onclose = null)
        {
            DialogWindow = dialog;
            ViewModel = viewModel;
            //dialog.Title = viewModel.Title;
            dialog.DataContext = ViewModel;
            dialog.Owner = Application.Current.MainWindow;
            dialog.ShowInTaskbar = false;
            dialog.Closed += Dialog_Closed;
            (dialog.DataContext as ViewModelBase).OnCloseAction = Onclose;
            dialog.ShowDialog();
        }

        private void Dialog_Closed(object sender, EventArgs e)
        {
            var v = sender as ChildWindow;
            var vm = v.DataContext as ViewModelBase;
            if (vm!=null && vm!=null)
            {
                vm.OnCloseAction(true, v.DataContext);
            }
        }

        public void ShowDialog(Type viewModel, ChildWindow dialog, dynamic context, Action<bool, object> Onclose = null)
        {
            DialogWindow = dialog;
            //ViewModel = viewModel;
            dynamic ViewModel = Activator.CreateInstance(viewModel, context);
            //dialog.Title = viewModel.Title;
            dialog.DataContext = ViewModel;
            dialog.Owner = Application.Current.MainWindow;
            dialog.ShowInTaskbar = false;
            dialog.Closed += Dialog_Closed;
            (dialog.DataContext as ViewModelBase).OnCloseAction = Onclose;
            dialog.ShowDialog();
        }
    }
}
