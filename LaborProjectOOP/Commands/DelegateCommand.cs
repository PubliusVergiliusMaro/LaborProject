using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.Commands
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam _name="T"></typeparam>
	public class DelegateCommand<T> : ICommand
	{
		#region Variables
		/// <summary>
		/// 
		/// </summary>
		readonly Action<T> _execute = null;
		/// <summary>
		/// 
		/// </summary>
		readonly Predicate<T> _canExecute = null;
		#endregion

		#region Constructor
		/// <summary>
		/// 
		/// </summary>
		/// <param _name="execute"></param>
		public DelegateCommand(Action<T> execute)
			: this(execute, null)
		{
		}

		public DelegateCommand(Action<T> execute, Predicate<T> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");

			_execute = execute;
			_canExecute = canExecute;
		}
		#endregion

		#region ICommand Members

		[DebuggerStepThrough]
		public bool CanExecute(object parameter)
		{
			return _canExecute == null ? true : _canExecute((T)parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter)
		{
			_execute((T)parameter);
		}
		#endregion
	}

	/// <summary>
	/// DelegateCommand
	/// </summary>
	public sealed class DelegateCommand : ICommand
	{
		#region Variables

		private Action executeMethod;
		private Func<bool> canExecute;
		#endregion

		#region Constructor
		public DelegateCommand(Action executeMethod)
			: this(executeMethod, null)
		{
		}
		public DelegateCommand(Action execute, Func<bool> canExecute)
		{
			this.executeMethod = execute;
			this.canExecute = canExecute;
		}
		#endregion

		#region API - CanExecute
		public bool CanExecute(object parameter)
		{
			return canExecute == null ? true : canExecute();
		}
		#endregion

		#region Events

		public event EventHandler CanExecuteChanged
		{
			add
			{
				if (canExecute != null)
					CommandManager.RequerySuggested += value;
			}
			remove
			{
				if (canExecute != null)
					CommandManager.RequerySuggested -= value;
			}
		}
		#endregion

		#region API - Execute
		public void Execute(object parameter)
		{
			executeMethod.Invoke();
		}
		#endregion
	}
}

