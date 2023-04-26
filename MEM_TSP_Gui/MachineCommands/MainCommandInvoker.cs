using System;
using System.Collections.Generic;
using MEM_TSP.Kernel.BusinessLogic;


namespace MEM_TSP.Gui.MachineCommands
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Hier muessen sich CommandCreationStrategies registrieren, um verwendet werden zu koennen.
	/// </summary>
	/// ****************************************************************************************************************
	public class MainCommandInvoker
	{
		internal static readonly MainCommandInvoker Instance = new MainCommandInvoker();

		private Dictionary<object, ICommandCreationStrategy> registeredStrategies;


		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Konstruktor der Klasse
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		private MainCommandInvoker()
		{
			this.registeredStrategies = new Dictionary<object, ICommandCreationStrategy>();
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Registriert eine CommandCreationStrategy
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="commandType">Typ des Kommandos</param>
		/// <param name="strategy">Auszufuehrende Strategie</param>
		/// ------------------------------------------------------------------------------------------------------------
		public void RegisterCommandCreationStrategy(object commandType, ICommandCreationStrategy strategy)
		{
			this.registeredStrategies.Add(commandType, strategy);
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Verarbeitet ein Kommando
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="name">Name des Kommandos</param>
		/// <param name="parameter">Parameter</param>
		/// ------------------------------------------------------------------------------------------------------------
		public void InvokeCommand(object name, object parameter)
		{
			if (this.registeredStrategies.ContainsKey(name))
			{
				BaseCommand newCommand = this.registeredStrategies[name].Create(parameter);
				MainManagerFacade.PushCommand(newCommand);
			}
			else
			{
				throw new InvalidOperationException("No registered CommandCreationStrategy of Type " + name);
			}
		}
	}
}
