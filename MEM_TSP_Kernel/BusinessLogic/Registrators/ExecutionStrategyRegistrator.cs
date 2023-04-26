namespace MEM_TSP.Kernel.BusinessLogic
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Uebernimmt die Registrierung der CommandExecutionStrategies
	/// </summary>
	/// ****************************************************************************************************************
	public class ExecutionStrategyRegistrator
	{
		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Registriert alle bekannten Strategien
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="receiver">MachineCommandReceiver, bei dem die Strategien registriert werden</param>
		/// ------------------------------------------------------------------------------------------------------------
		internal static void RegisterWith(MainCommandReceiver receiver)
		{
			receiver.RegisterStrategy(typeof(TspCommand), new TspCommandExecutionStrategy());
		}
	}
}
