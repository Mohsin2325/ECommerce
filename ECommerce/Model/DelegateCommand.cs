using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ECommerce.Model
{
    public interface IRaisableCommand
    {
        void RaiseCanExecuteChanged();
    }

    public sealed class DelegateCommand : ICommand, IRaisableCommand, IDisposable
        {

            /// <summary>
            /// Occurs when changes occur that affect whether the command should execute.
            /// </summary>
            public event EventHandler CanExecuteChanged;

            private Action _execute;
            private Func<bool> _canExecute;
            private bool _canExecuteCache;
            private DateTime _lastExecute = DateTime.MinValue;
            //private object p1;
            //private bool p2;

            public bool IsDisposed { get; private set; }

            /// <summary>
            /// Initializes a new instance of the RelayCommand class that can always execute.
            /// </summary>
            /// <param name="execute">The execution logic.</param>
            /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
            public DelegateCommand(Action execute, bool betweenExecuteDelay = true) : this(execute, null, betweenExecuteDelay) { }

            /// <summary>
            /// Initializes a new instance of the RelayCommand class.
            /// </summary>
            /// <param name="execute">The execution logic.</param>
            /// <param name="canExecute">The execution status logic.</param>
            /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
            public DelegateCommand(Action execute, Func<bool> canExecute, bool betweenExecuteDelay = true)
            {
                if (execute == null)
                {
                    throw new ArgumentNullException("execute");
                }
                BetweenExecuteDelay = betweenExecuteDelay;
                _execute = execute;
                _canExecute = canExecute;
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

            bool ICommand.CanExecute(object parameter)
            {
                return CanExecute();
            }
            void ICommand.Execute(object parameter)
            {
                Execute();
            }

            /// <summary>
            /// Defines the method that determines whether the command can execute in its current state.
            /// </summary>
            /// <returns>true if this command can be executed; otherwise, false.</returns>
            public bool CanExecute()
            {
                bool tempCanExecute = true;

                if (_canExecute != null)
                {
                    tempCanExecute = _canExecute();
                }

                if (_canExecuteCache != tempCanExecute)
                {
                    _canExecuteCache = tempCanExecute;
                    RaiseCanExecuteChanged();
                }

                return _canExecuteCache;
            }

            /// <summary>
            /// Defines the method to be called when the command is invoked.
            /// </summary>
            public void Execute()
            {
                
                    if (CanExecute())
                    {
                        _execute();
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
                _execute = null;
            }
        }
    }

