using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfControlDev.Extend
{
    /// <summary>
	/// A command whose sole purpose is to relay its functionality to other
	/// objects by invoking delegates. The default return value for the CanExecute
	/// method is 'true'.  This class does not allow you to accept command parameters in the
	/// Execute and CanExecute callback methods.
	/// </summary>
	/// <remarks>If you are using this class in WPF4.5 or above, you need to use the 
	/// GalaSoft.MvvmLight.CommandWpf namespace (instead of GalaSoft.MvvmLight.Command).
	/// This will enable (or restore) the CommandManager class which handles
	/// automatic enabling/disabling of controls based on the CanExecute delegate.</remarks>
    public class RelayCommand : ICommand
    {
        private readonly WeakAction _execute;

        private readonly WeakFunc<bool> _canExecute;

        private EventHandler _requerySuggestedLocal;

        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this._canExecute != null)
                {
                    EventHandler eventHandler = this._requerySuggestedLocal;
                    EventHandler eventHandler2;
                    do
                    {
                        eventHandler2 = eventHandler;
                        EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
                        eventHandler = Interlocked.CompareExchange<EventHandler>(ref this._requerySuggestedLocal, value2, eventHandler2);
                    }
                    while (eventHandler != eventHandler2);
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (this._canExecute != null)
                {
                    EventHandler eventHandler = this._requerySuggestedLocal;
                    EventHandler eventHandler2;
                    do
                    {
                        eventHandler2 = eventHandler;
                        EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
                        eventHandler = Interlocked.CompareExchange<EventHandler>(ref this._requerySuggestedLocal, value2, eventHandler2);
                    }
                    while (eventHandler != eventHandler2);
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class that 
        /// can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <exception cref="T:System.ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action execute) : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="T:System.ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            this._execute = new WeakAction(execute);
            if (canExecute != null)
            {
                this._canExecute = new WeakFunc<bool>(canExecute);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:HSUCF.WPF.Core.RelayCommand.CanExecuteChanged" /> event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">This parameter will always be ignored.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            return this._canExecute == null || ((this._canExecute.IsStatic || this._canExecute.IsAlive) && this._canExecute.Execute());
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked. 
        /// </summary>
        /// <param name="parameter">This parameter will always be ignored.</param>
        public virtual void Execute(object parameter)
        {
            if (this.CanExecute(parameter) && this._execute != null && (this._execute.IsStatic || this._execute.IsAlive))
            {
                this._execute.Execute();
            }
        }
    }
    /// <summary>
	/// A generic command whose sole purpose is to relay its functionality to other
	/// objects by invoking delegates. The default return value for the CanExecute
	/// method is 'true'. This class allows you to accept command parameters in the
	/// Execute and CanExecute callback methods.
	/// </summary>
	/// <typeparam name="T">The type of the command parameter.</typeparam>
	/// <remarks>If you are using this class in WPF4.5 or above, you need to use the 
	/// GalaSoft.MvvmLight.CommandWpf namespace (instead of GalaSoft.MvvmLight.Command).
	/// This will enable (or restore) the CommandManager class which handles
	/// automatic enabling/disabling of controls based on the CanExecute delegate.</remarks>
	public class RelayCommand<T> : ICommand
    {
        private readonly WeakAction<T> _execute;

        private readonly WeakFunc<T, bool> _canExecute;

        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this._canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (this._canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class that 
        /// can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <exception cref="T:System.ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="T:System.ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            this._execute = new WeakAction<T>(execute);
            if (canExecute != null)
            {
                this._canExecute = new WeakFunc<T, bool>(canExecute);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:HSUCF.WPF.Core.RelayCommand`1.CanExecuteChanged" /> event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data 
        /// to be passed, this object can be set to a null reference</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            if (this._canExecute == null)
            {
                return true;
            }
            if (this._canExecute.IsStatic || this._canExecute.IsAlive)
            {
                if (parameter == null && typeof(T).IsValueType)
                {
                    return this._canExecute.Execute(default(T));
                }
                if (parameter == null || parameter is T)
                {
                    return this._canExecute.Execute((T)((object)parameter));
                }
            }
            return false;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked. 
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data 
        /// to be passed, this object can be set to a null reference</param>
        public virtual void Execute(object parameter)
        {
            object obj = parameter;
            if (parameter != null && parameter.GetType() != typeof(T) && parameter is IConvertible)
            {
                obj = Convert.ChangeType(parameter, typeof(T), null);
            }
            if (this.CanExecute(obj) && this._execute != null && (this._execute.IsStatic || this._execute.IsAlive))
            {
                if (obj == null)
                {
                    if (typeof(T).IsValueType)
                    {
                        this._execute.Execute(default(T));
                        return;
                    }
                    this._execute.Execute((T)((object)obj));
                    return;
                }
                else
                {
                    this._execute.Execute((T)((object)obj));
                }
            }
        }
    }
}
