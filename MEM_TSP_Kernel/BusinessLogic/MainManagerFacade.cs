using MEM_TSP.Kernel.Model;


namespace MEM_TSP.Kernel.BusinessLogic
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Fassadenklasse, um die Komplexitaet des MainManagers vom ViewModel abzuschirmen
	/// </summary>
	/// ****************************************************************************************************************
	public class MainManagerFacade
	{
		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf die ApplicationState-Daten des Main Managers
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public static TspLogger TspLogger
		{
			get { return MainManager.Instance.TspLogger; }
		}


		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Methode, um ein neues Kommando an die Maschine zu schicken
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="command">Neues Kommando</param>
		/// ------------------------------------------------------------------------------------------------------------
		public static void PushCommand(BaseCommand command)
		{
			MainManager.Instance.CommandReceiver.MainCommandQueue.PushCommand(command);
		}
	}
}