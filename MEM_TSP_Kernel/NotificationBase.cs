using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace MEM_TSP.Kernel
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Basisklasse die das INotifyPropertyChanged interface implementiert und zwei protected Methoden zur Verfügung stellt,
	/// die Event schicken, wenn sich ein Property ändert. Für die EventArg Instanzen wird ein Cache verwendet, so dass bei
	/// jedem Aufruf der OnPropertyChanged(string property) Methode geprüft wird, ob es für dieses Property schon eine   
	/// EventArgs Instanz im Cache gibt.
	/// </summary>
	/// ****************************************************************************************************************	
	public class NotificationBase : INotifyPropertyChanged
	{
		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Wird geworfen, wenn sich der Wert eines Properties ändert.
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public event PropertyChangedEventHandler PropertyChanged;

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Eine spezifische Implementierung von OnPropertyChanged.
		/// Hier wird ein Dictionary als Cache verwendet, damit für ein Property nur ein PropertyChangedEventArgs Objekt erzeugt wird.
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="propertyName">Name of the property.</param>
		/// ------------------------------------------------------------------------------------------------------------
		public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = this.PropertyChanged;

			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Wert einer Eigenschaft setzten. Unterscheidet sich der neue Wert vom aktuellen, so wird der PropertyChanged-Event gefeuert 
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <typeparam name="T">Type der Property</typeparam>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="storage">Referenz zum Field der Property</param>
		/// <param name="value">Neuer Wert</param>
		/// <param name="propertyName">Name der Property</param>
		/// ------------------------------------------------------------------------------------------------------------
		/// <returns>true, wenn der neue Wert übernommen wurde (der neue Wert unterscheidet sich vom aktuellen)</returns>
		/// ------------------------------------------------------------------------------------------------------------
		protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (object.Equals(storage, value))
			{
				return false;
			}

			storage = value;
			this.OnPropertyChanged(propertyName);
			return true;
		}
	}
}
