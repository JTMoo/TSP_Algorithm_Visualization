using System;


namespace MEM_TSP.Kernel.BusinessLogic
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Diese Klasse definiert ein Tsp-Kommando
	/// </summary>
	/// ****************************************************************************************************************
	public class TspCommand : BaseCommand
	{
		private TspCommandData data;


		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Konstruktor
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="data">Daten des Kommandos</param>
		/// ------------------------------------------------------------------------------------------------------------
		public TspCommand(TspCommandData data)
		{
			this.data = data;
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Alle moeglichen ApplicationState-Kommandos
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public enum TspCommands
		{
			/// <summary>Den uebergebenen Algorithmus starten.</summary>
			Start,

			/// <summary>Den aktuellen Algorithmus stoppen.</summary>
			Stop,
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf Data
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public TspCommandData Data
		{
			get { return this.data; }
		}

		/// ****************************************************************************************************************
		/// <summary>
		/// Hilfsklasse zur gekapselten Datenhaltung innerhalb eines Kommandos
		/// Notwendig, da im Konstruktor eines Kommandos jeweils nur ein Parameter übergeben werden darf
		/// </summary>
		/// ****************************************************************************************************************
		public class TspCommandData
		{
			/// ------------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Zugriff auf das Kommando
			/// </summary>
			/// ------------------------------------------------------------------------------------------------------------
			public TspCommands Command { get; set; }

			/// ------------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Zugriff auf den Parameter des Kommandos
			/// </summary>
			/// ------------------------------------------------------------------------------------------------------------
			public Object Value { get; set; }
		}
	}
}
