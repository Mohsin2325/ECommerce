using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ECommerce.Model
{
    public sealed class DelegateCommandT<T> : ICommand, IRaisableCommand, IDisposable
    {
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        private Func<T, bool> _canExecute;
        private Action<T> _executeAction;
        private DateTime _lastExecute = DateTime.MinValue;

        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class that 
        /// can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
        public DelegateCommandT(Action<T> execute)
            : this(execute, null) { }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
        public DelegateCommandT(Action<T> execute, Func<T, bool> canExecute, bool betweenExecuteDelay = true)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            BetweenExecuteDelay = betweenExecuteDelay;
            _executeAction = execute;
            _canExecute = canExecute;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);

        }

        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "Consumer of commmand needs a way to raise the event.")]
        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Defines the method that determines whether the command 
        /// can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. 
        /// If the command does not require data to be passed,
        /// this object can be set to null.
        /// </param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(T parameter)
        {
            bool tempCanExecute = true;

            if (_canExecute != null)
            {
                tempCanExecute = _canExecute(parameter);
            }

            return tempCanExecute;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. 
        /// If the command does not require data to be passed, 
        /// this object can be set to null.
        /// </param>
        public void Execute(T parameter)
        {
           
                if (CanExecute(parameter))
                {
                    _executeAction(parameter);
                }
            
        }


        public bool BetweenExecuteDelay
        {
            get;
            set;
        }

        public void Dispose()
        {
            IsDisposed = true;
            _canExecute = null;
            _executeAction = null;
        }
    }
}
