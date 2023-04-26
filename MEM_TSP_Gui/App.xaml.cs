using System.Windows;

namespace MEM_TSP.Gui
{
    /// ****************************************************************************************************************
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
	/// ****************************************************************************************************************
    public partial class App : Application
    {
        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Logik, wenn App das erste Mal gestartet wird.
        /// </summary>
		/// ------------------------------------------------------------------------------------------------------------
        /// <param name="e">StartupEventArgs</param>
		/// ------------------------------------------------------------------------------------------------------------
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                Kernel.BusinessLogic.MainManager.Init();
            }
            catch
            {
                MessageBox.Show("TSP-Solver is already running...");
                return;
            }

            base.OnStartup(e);
        }

        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Logik, wenn die App geschlossen wird.
        /// </summary>
		/// ------------------------------------------------------------------------------------------------------------
        /// <param name="e">ExitEventArgs</param>
		/// ------------------------------------------------------------------------------------------------------------
        protected override void OnExit(ExitEventArgs e)
        {
            Kernel.BusinessLogic.MainManager.Release();
            base.OnExit(e);
        }
    }
}
