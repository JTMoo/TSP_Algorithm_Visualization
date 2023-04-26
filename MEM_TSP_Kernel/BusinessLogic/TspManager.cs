using MEM_TSP.Kernel.BusinessLogic.Algorithms;
using MEM_TSP.Kernel.Model.Enums;
using MEM_TSP.Kernel.Model.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace MEM_TSP.Kernel.BusinessLogic
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Zentrale Zugriffsklasse für Tsp
	/// </summary>
	/// ****************************************************************************************************************
	internal class TspManager : IDisposable
	{
		private bool disposed = false;
		private readonly object algorithmLock = new object();
		private List<RunningAlgorithm> runningAlgorithms = new List<RunningAlgorithm>();


		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Destruktor
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		~TspManager()
		{
			this.Dispose(false);
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>         
		/// Freigeben von Resourcen
		/// </summary>         
		/// ------------------------------------------------------------------------------------------------------------
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>         
        /// Register algorithm
        /// </summary>         
        /// ------------------------------------------------------------------------------------------------------------
        /// <param name="algorithm">algorithm</param>
        /// <param name="tspProblem">Tsp-problem data</param>
        /// ------------------------------------------------------------------------------------------------------------
        public void RegisterAlgorithm(IAlgorithm algorithm, TspProblem tspProblem)
		{
			if (algorithm == null)
			{
				return;
			}

			if (tspProblem.State != ProblemStates.resumeable)
			{
				tspProblem.Result.ExecutionTime = 0;
				tspProblem.Watch = Stopwatch.StartNew();
			}

			if (tspProblem.State == ProblemStates.resumeable)
			{
				tspProblem.Watch.Start();
			}

			tspProblem.State = ProblemStates.running;

			var cancellationToken = new CancellationTokenSource();
			var runningAlgorithm = new RunningAlgorithm() { Algorithm = algorithm, TspProblem = tspProblem, CancellationToken = cancellationToken };

			lock (this.algorithmLock)
			{
				this.runningAlgorithms.Add(runningAlgorithm);
			}

			this.StartSolve(runningAlgorithm);
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>         
		/// Abmelden eines Algorithmus
		/// </summary>         
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="guid">Guid des ViewModels</param>
		/// ------------------------------------------------------------------------------------------------------------
		public void DeregisterAlgorithm(Guid guid)
		{
			if (this.runningAlgorithms.Count <= 0)
			{
				return;
			}

			var runningAlgorithm = this.runningAlgorithms.FirstOrDefault(alg => alg.TspProblem.Guid == guid);
			runningAlgorithm.TspProblem.Watch.Stop();

			lock (this.algorithmLock)
			{
				this.runningAlgorithms.Remove(runningAlgorithm);
			}
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Alle Verbindungen schliessen und Objekte zuruecksetzen
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		internal void Release()
		{
			this.Dispose();
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Abbruch des Ausfuehrens des Algorithmus
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		internal void CancelAlgorithm(Guid viewModelGuid)
		{
			this.runningAlgorithms.FirstOrDefault(rAlg => rAlg.TspProblem.Guid == viewModelGuid).CancellationToken.Cancel();
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Updates the result distance of a tsp problem
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="tspProblem">tsp-problem</param>
		/// ------------------------------------------------------------------------------------------------------------
		internal void UpdateResult(TspProblem tspProblem)
		{
			tspProblem.Result.ExecutionTime = tspProblem.Watch.ElapsedMilliseconds;
			tspProblem.Result.Distance = GetDistance(tspProblem.Result.Route);
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Calculates the total distance of a given route.
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="route">List of points</param>
		/// ------------------------------------------------------------------------------------------------------------
		/// <returns>Total distance rounded to two decimal places</returns>
		/// ------------------------------------------------------------------------------------------------------------
		internal static double GetDistance(List<Point> route)
		{
			double distance = 0;

			if (route.Count <= 1)
			{
				return distance;
			}

			foreach (var point in route)
			{
				var indexOfNextPoint = route.IndexOf(point) + 1;
				indexOfNextPoint = indexOfNextPoint == route.Count ? 0 : indexOfNextPoint;
				var nextPoint = route[indexOfNextPoint];

				var x_diff = point.X - nextPoint.X;
				var y_diff = point.Y - nextPoint.Y;

				distance += Math.Sqrt(x_diff * x_diff + y_diff * y_diff);
			}

			return Math.Round(distance, 2, MidpointRounding.AwayFromZero);
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>         
		/// Freigeben von Resourcen
		/// </summary>         
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="disposing">Managed (true) oder Native (false) Resourcen</param>
		/// ------------------------------------------------------------------------------------------------------------
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					// Release managed resources ...
					this.runningAlgorithms.ForEach(algorithm => algorithm.CancellationToken?.Dispose());
				}

				// Free the unmanaged resource ...
				this.disposed = true;
			}
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Start a new algorithm as a new task
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		private void StartSolve(RunningAlgorithm runningAlgorithm)
		{
			Task.Factory.StartNew(() =>
			{
				while (runningAlgorithm.TspProblem.State == ProblemStates.running)
				{
					if (runningAlgorithm.CancellationToken.IsCancellationRequested)
					{
						runningAlgorithm.TspProblem.State = ProblemStates.resumeable;
						MainManager.Instance.Invoke(() => runningAlgorithm.TspProblem.Callback?.Invoke(runningAlgorithm.TspProblem));
						break;
					}

					runningAlgorithm.Algorithm.Solve(runningAlgorithm.TspProblem);
					this.UpdateResult(runningAlgorithm.TspProblem);
					MainManager.Instance.Invoke(() => runningAlgorithm.TspProblem.Callback?.Invoke(runningAlgorithm.TspProblem));
				}

				this.DeregisterAlgorithm(runningAlgorithm.TspProblem.Guid);

			}, runningAlgorithm.CancellationToken.Token);
		}
	}
}
