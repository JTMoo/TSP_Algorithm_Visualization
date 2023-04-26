using MEM_TSP.Kernel.Model.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;


namespace MEM_TSP.Kernel.Model.Types
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Spezifische Datenstruktur für die Beschreibung des Problems
	/// </summary>
	/// ****************************************************************************************************************
	public class TspProblem : NotificationBase
    {
		private ProblemStates state = ProblemStates.open;
		private List<Point> points = new List<Point>();
		private TspResult result = new TspResult();


		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf den Callback
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public Algorithm Algorithm { get; set; }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf den Callback
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public Action<TspProblem> Callback { get; set; } = null;
		
		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf den Global Unique Identifier
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public Guid Guid { get; set; }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf die Koordinaten des Punktes
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public List<Point> Points
		{
			get { return this.points; }
			set { this.SetProperty(ref this.points, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf das (Zwischen-)Ergebnis des Problems
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public TspResult Result
		{
			get { return this.result; }
			set { this.SetProperty(ref this.result, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf den Status des Problems
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public ProblemStates State
		{
			get { return this.state; }
			set { this.SetProperty(ref this.state, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf die Watch
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public Stopwatch Watch { get; set; } = Stopwatch.StartNew();
	}
}
