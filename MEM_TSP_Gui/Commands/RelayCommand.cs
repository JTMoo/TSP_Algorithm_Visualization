using System;
using System.Diagnostics;
using System.Windows.Input;

namespace MEM_TSP.Gui
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Ein RelayCommand erlaubt bei Aufruf die Ausfuehrung einer Callback-Funktion
	/// </summary>
	/// ****************************************************************************************************************
	public class RelayCommand : ICommand
	{
		private readonly Action<object> relayToExecute;
		private readonly Predicate<object> relayToCanExecute;

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Konstruktor
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="execute">Die auszufuehrende Callback-Funktion.</param>
		/// ------------------------------------------------------------------------------------------------------------
		public RelayCommand(Action<object> execute) : this(execute, null)
		{ 
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Konstruktor
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="execute">Die auszufuehrende Callback-Funktion.</param>
		/// <param name="canExecute">Das vor der eigentlichen Callback zu ueberpruefende Property.</param>
		/// ------------------------------------------------------------------------------------------------------------
		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}

			this.relayToExecute = execute;
			this.relayToCanExecute = canExecute;
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Weist den CommandManager auf Bedingungen hin, die eine Befehlsausfuehrung beeinflussen könnten.
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Definiert die Methode, die überprueft, ob das Kommando ausgefuehrt werden kann
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Zusaetzliche Daten. Null, falls nicht benoetigt.</param>
		/// ------------------------------------------------------------------------------------------------------------
		/// <returns>True, wenn Kommando ausfuehrbar.</returns>
		/// ------------------------------------------------------------------------------------------------------------
		[DebuggerStepThrough]
		public bool CanExecute(object param)
		{
			return this.relayToCanExecute == null ? true : this.relayToCanExecute(param);
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Definiert die Methode, die ausgeführt werden soll.
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Zusaetzliche Daten. Null, falls nicht benoetigt. </param>
		/// ------------------------------------------------------------------------------------------------------------
		public void Execute(object param)
		{
			this.relayToExecute(param);
		}
	}
}
