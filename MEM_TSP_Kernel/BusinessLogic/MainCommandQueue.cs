using System.Collections.Generic;


namespace MEM_TSP.Kernel.BusinessLogic
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Ein FIFO-Buffer für Kommandos
	/// </summary>
	/// ****************************************************************************************************************
	public class MainCommandQueue
	{
		private readonly object queueLock = new object();
		private LinkedList<BaseCommand> commandQueue;


		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Der Konstruktor der Klasse
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		internal MainCommandQueue()
		{
			this.commandQueue = new LinkedList<BaseCommand>();
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Ein neues Kommando hinzufuegen
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="command">Hinzuzufuegendes Kommando</param>
		/// ------------------------------------------------------------------------------------------------------------
		internal void PushCommand(BaseCommand command)
		{
			lock (this.queueLock)
			{
				this.commandQueue.AddLast(command);
			}
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Das aelteste Kommando abholen
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <returns name="command">Aeltestes Kommando</returns>
		/// ------------------------------------------------------------------------------------------------------------
		internal BaseCommand PopCommand()
		{
			lock (this.queueLock)
			{
				if (this.commandQueue.Count == 0)
				{
					return null;
				}
				BaseCommand first = this.commandQueue.First.Value;
				this.commandQueue.RemoveFirst();
				return first;
			}
		}
	}
}
