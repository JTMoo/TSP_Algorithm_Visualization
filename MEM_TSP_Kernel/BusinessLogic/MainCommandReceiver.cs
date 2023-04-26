using System;
using System.Collections.Generic;


namespace MEM_TSP.Kernel.BusinessLogic
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Empfaengt IBaseCommands und arbeitet diese mittels ICommandExecutionStrategy-Klassen ab
	/// </summary>
	/// ****************************************************************************************************************
	public class MainCommandReceiver
	{
		private Dictionary<Type, ICommandExecutionStrategy> registeredStrategies = new Dictionary<Type, ICommandExecutionStrategy>();
		private MainCommandQueue mainCommandQueue = new MainCommandQueue();


		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// liefert die MachineCommandQueue
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		internal MainCommandQueue MainCommandQueue
		{
			get { return this.mainCommandQueue; }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// arbeitet alle anliegenden MachineCommands ab
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		internal void ReceiveAndHandleCommands()
		{
			BaseCommand command = null;
			do
			{
				command = this.mainCommandQueue.PopCommand();
				if (command != null)
				{
					if (this.registeredStrategies.ContainsKey(command.GetType()))
					{
						this.registeredStrategies[command.GetType()].Execute(command);
					}
					else
					{
						throw new InvalidOperationException("No registered MachineCommandExecutionStrategy of Type " + command.GetType());
					}
				}
			}
			while (command != null);
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// registriert eine CommandExecutionStrategy
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="baseCommandType">Typ des zu verarbeitenden Commands</param>
		/// <param name="strategy">auszufuehrende Strategie</param>
		/// ------------------------------------------------------------------------------------------------------------
		internal void RegisterStrategy(Type baseCommandType, ICommandExecutionStrategy strategy)
		{
			this.registeredStrategies.Add(baseCommandType, strategy);
		}
	}
}
