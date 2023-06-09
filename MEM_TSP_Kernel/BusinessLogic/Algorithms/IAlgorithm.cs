﻿using MEM_TSP.Kernel.Model.Types;


namespace MEM_TSP.Kernel.BusinessLogic.Algorithms
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Interface fuer einen Algorithmus
	/// </summary>
	/// ****************************************************************************************************************
	public interface IAlgorithm
	{
		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Startet den Algorithmus fuer das Loesen des TSP
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="tspProblem">tsp-problem data</param>
		/// ------------------------------------------------------------------------------------------------------------
		void Solve(TspProblem tspProblem);
	}
}
