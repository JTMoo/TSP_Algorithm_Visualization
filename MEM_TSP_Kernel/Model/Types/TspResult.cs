using System.Collections.Generic;
using System.Windows;


namespace MEM_TSP.Kernel.Model.Types
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Spezifische Datenstruktur für die Beschreibung der Lösung eines TSP.
	/// </summary>
	/// ****************************************************************************************************************
	public class TspResult : NotificationBase
    {
		private double distance = 0;
		private double executionTime = 0;
		private List<Point> route = new List<Point>();


		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf die Distanz
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public double Distance
		{
			get { return this.distance; }
			set { this.SetProperty(ref this.distance, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf die Ausführungszeit
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public double ExecutionTime
		{
			get { return this.executionTime; }
			set { this.SetProperty(ref this.executionTime, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf die Route
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public List<Point> Route
		{
			get { return this.route; }
			set { this.SetProperty(ref this.route, value); }
		}
    }
}
