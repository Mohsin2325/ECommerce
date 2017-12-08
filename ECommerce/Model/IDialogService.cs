using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ECommerce.Model
{
    interface IDialogService<T>
    {
        MessageBoxResult ShowMessageBox(string content,string title, MessageBoxButton buttons);

        void ShowDialog(T viewModel, Window dialog);

        T ViewModel { get; set; }

        Window DialogWindow { get; set; }
    }

    public class DialogService<T> : IDialogService<T>
    {
        public T ViewModel { get; set; }
        public Window DialogWindow { get; set; }
        public MessageBoxResult ShowMessageBox(string content,

        string title, MessageBoxButton buttons)
        {
            return MessageBox.Show(content, title, buttons);
        }


        public void ShowDialog(T viewModel, Window dialog)
        {
            DialogWindow = dialog;
            ViewModel = viewModel;
            //dialog.Title = viewModel.Title;
            dialog.DataContext = ViewModel;
            dialog.Owner = Application.Current.MainWindow;
            dialog.ShowInTaskbar = false;
            dialog.ShowDialog();
        }
    }
}
