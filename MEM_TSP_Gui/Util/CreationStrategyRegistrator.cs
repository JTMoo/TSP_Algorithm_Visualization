using MEM_TSP.Gui.MachineCommands;

namespace MEM_TSP.Gui.Util
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Uebernimmt die Registrierung der CommandCreation-Strategien
	/// </summary>
	/// ****************************************************************************************************************
	public class CreationStrategyRegistrator
	{
		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Registriert alle bekannten Strategien
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="invoker">MachineCommandInvoker, bei dem die Strategien registriert werden</param>
		/// ------------------------------------------------------------------------------------------------------------
		public static void RegisterWith(MainCommandInvoker invoker)
		{
			invoker.RegisterCommandCreationStrategy("TspCommand", new TspCommandCreationStrategy());
		}
	}
}
