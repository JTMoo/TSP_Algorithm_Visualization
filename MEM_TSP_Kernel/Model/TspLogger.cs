using System.Collections.ObjectModel;


namespace MEM_TSP.Kernel.Model
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Diese Klasse hält alle Daten über den aktuellen Zustand der Applikation
	/// </summary>
	/// ****************************************************************************************************************
	public class TspLogger : NotificationBase
	{
		private ObservableCollection<string> tspLog = new ObservableCollection<string>();


		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf CustomConsoleText
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public ObservableCollection<string> TspLog
		{
			get { return this.tspLog; }
			internal set { this.SetProperty(ref this.tspLog, value); }
		}
	}
}
