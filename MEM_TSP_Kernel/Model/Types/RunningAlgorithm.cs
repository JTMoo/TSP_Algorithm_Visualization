using MEM_TSP.Kernel.BusinessLogic.Algorithms;
using System.Threading;


namespace MEM_TSP.Kernel.Model.Types
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Datenstruktur, die einen laufenden Algorithmus repraesentiert
	/// </summary>
	/// ****************************************************************************************************************
	public class RunningAlgorithm
    {
		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf das TspProblem
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public TspProblem TspProblem { get; set; } = new TspProblem();

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf den Algorithm
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public IAlgorithm Algorithm { get; set; } = null;

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf den CancellationToken
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public CancellationTokenSource CancellationToken { get; set; } = null;
	}
}
