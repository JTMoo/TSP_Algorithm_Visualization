using MEM_TSP.Kernel.BusinessLogic;


namespace MEM_TSP.Gui.MachineCommands
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Strategie zum Erstellen eines neuen Tsp-Kommandos
	/// </summary>
	/// ****************************************************************************************************************
	public sealed class TspCommandCreationStrategy : ICommandCreationStrategy
	{
		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Erzeugt ein neues Tsp-Kommando
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Parameter</param>
		/// ------------------------------------------------------------------------------------------------------------
		/// <returns>Neues Tsp-Kommando</returns>
		/// ------------------------------------------------------------------------------------------------------------
		BaseCommand ICommandCreationStrategy.Create(object param)
		{
			var tspCommandData = param as TspCommand.TspCommandData;
			return new TspCommand(tspCommandData);
		}
	}
}
