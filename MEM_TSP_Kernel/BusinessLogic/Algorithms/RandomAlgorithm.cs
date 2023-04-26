using MEM_TSP.Kernel.Model.Enums;
using MEM_TSP.Kernel.Model.Types;
using System;
using System.Collections.Generic;
using System.Windows;


namespace MEM_TSP.Kernel.BusinessLogic.Algorithms
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Logik fuer den random Algorithmus
	/// </summary>
	/// ****************************************************************************************************************
	public class RandomAlgorithm : IAlgorithm
	{
		private List<Point> tempListPoints = new List<Point>();
		private int failedAttemptCounter = 0;
		private readonly int maxFailedAttempts = 30000;


		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Algorithmus fuer das Loesen des TSP
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public void Solve(TspProblem tspProblem)
		{
			var tempRoute = new List<Point>();
			tspProblem.Points.ForEach(point => this.tempListPoints.Add(point));

			while (this.tempListPoints.Count > 0)
			{
				var randomPos = new Random().Next(this.tempListPoints.Count);

				tempRoute.Add(this.tempListPoints[randomPos]);
				this.tempListPoints.RemoveAt(randomPos);
			}
			tempRoute.Add(tempRoute[0]);

			var tempDistance = TspManager.GetDistance(tempRoute);

			if (tspProblem.Result.Distance > tempDistance || tspProblem.Result.Distance == 0)
			{
				tspProblem.Result.Route = tempRoute;
			}
			else
			{
				this.failedAttemptCounter++;
			}

			if (this.failedAttemptCounter == this.maxFailedAttempts)
			{
				tspProblem.State = ProblemStates.solved;
			}
		}
    }
}
