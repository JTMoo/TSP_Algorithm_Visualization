using MEM_TSP.Kernel.Model.Enums;
using MEM_TSP.Kernel.Model.Types;
using System;
using System.Collections.Generic;
using System.Windows;


namespace MEM_TSP.Kernel.BusinessLogic.Algorithms
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Logik für den TSP-Algorithmus Nearest-Neighbor
	/// </summary>
	/// ****************************************************************************************************************
	public class NearestNeighborAlgorithm : IAlgorithm
	{
		private List<Point> tempListPoints = new List<Point>();
		private int curPointPos = 0;


		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Algorithmus fuer das Loesen des TSP
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public void Solve(TspProblem tspProblem)
		{
			if (this.tempListPoints.Count <= 0)
			{
				tspProblem.Points.ForEach(point => this.tempListPoints.Add(point));
			}

			var curPoint = this.tempListPoints[this.curPointPos];
			this.tempListPoints.RemoveAt(this.curPointPos);

			if (tempListPoints.Count == 0)
			{
				tspProblem.Result.Route.Add(curPoint);
				tspProblem.Result.Route.Add(tspProblem.Result.Route[0]);
				tspProblem.State = ProblemStates.solved;
				return;
			}

			var tempMinDist = double.MaxValue;
			var tempPointPos = 0;
			tempListPoints.ForEach(nextPoint =>
			{
				var x_diff = curPoint.X - nextPoint.X;
				var y_diff = curPoint.Y - nextPoint.Y;

				var distance = Math.Sqrt(x_diff * x_diff + y_diff * y_diff);

				if (tempMinDist > distance)
				{
					tempMinDist = distance;
					this.curPointPos = tempPointPos;
				}

				tempPointPos++;
			});

			tspProblem.Result.Route.Add(curPoint);
		}
    }
}
