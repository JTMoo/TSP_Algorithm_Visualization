using MEM_TSP.Kernel.BusinessLogic.Algorithms;
using MEM_TSP.Kernel.Model.Enums;
using MEM_TSP.Kernel.Model.Types;
using System;


namespace MEM_TSP.Kernel.BusinessLogic
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Eine Strategie zur Ausführung von Tsp-Kommandos
	/// </summary>
	/// ****************************************************************************************************************
	internal class TspCommandExecutionStrategy : ICommandExecutionStrategy
	{
		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Tsp-Kommando ausfuehren
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="command">Kommando</param>
		/// ------------------------------------------------------------------------------------------------------------
		public void Execute(BaseCommand command)
		{
			var tspCommand = (TspCommand)command;
			IAlgorithm algorithm = null;

			switch (tspCommand.Data.Command)
			{
				case TspCommand.TspCommands.Start:
					var tspProblem = tspCommand.Data.Value as TspProblem;

					switch (tspProblem.Algorithm)
					{
						case Algorithm.NearestNeighbor:
							algorithm = new NearestNeighborAlgorithm();
							break;
						case Algorithm.Random:
							algorithm = new RandomAlgorithm();
							break;
					}

					MainManager.Instance.TspManager.RegisterAlgorithm(algorithm, tspProblem);
					break;
				case TspCommand.TspCommands.Stop:
					MainManager.Instance.TspManager.CancelAlgorithm((Guid)tspCommand.Data.Value);
					break;
			}
		}
	}
}
