using MEM_TSP.Gui.MachineCommands;
using MEM_TSP.Gui.Util;
using MEM_TSP.Gui.View.Tabs;
using MEM_TSP.Kernel;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace MEM_TSP.Gui.ViewModel
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Das ViewModel fuer das MainWindow
	/// </summary>
	/// ****************************************************************************************************************
	public class MainViewModel : NotificationBase
	{
		private readonly string mainWindowTitle = "MEM_TSP_UI";
		private TabItem selectedTab;


		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Konstruktor
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public MainViewModel()
		{
			CreationStrategyRegistrator.RegisterWith(MainCommandInvoker.Instance);

			this.ExitCommand = new RelayCommand(this.OnExitCommand);
			this.AddAlgorithmTabCommand = new RelayCommand(this.OnAddAlgorithmTabCommand);
			this.AddMenuTabCommand = new RelayCommand(this.OnAddMenuTabCommand);
			this.RemoveTabCommand = new RelayCommand(this.OnRemoveTabCommand);

			this.Tabs = new ObservableCollection<TabItem>();
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf ExitCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public RelayCommand ExitCommand { get; private set; }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf AddAlgorithmTabCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public RelayCommand AddAlgorithmTabCommand { get; private set; }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf AddMenuTabCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public RelayCommand AddMenuTabCommand { get; private set; }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf RemoveTabCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public RelayCommand RemoveTabCommand { get; private set; }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf Tabs
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public ObservableCollection<TabItem> Tabs { get; private set; }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf den selektierten Tab
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public TabItem SelectedTab
		{
			get { return this.selectedTab; }
			set { this.SetProperty(ref this.selectedTab, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf MainWindowTitle
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public string MainWindowTitle
		{
			get { return this.mainWindowTitle; }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Verarbeitung des ExitCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Parameter</param>
		/// ------------------------------------------------------------------------------------------------------------
		private void OnExitCommand(object param)
		{
			System.Windows.Application.Current.Shutdown();
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Verarbeitung des AddAlgorithmTabCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Parameter</param>
		/// ------------------------------------------------------------------------------------------------------------
		private void OnAddAlgorithmTabCommand(object param)
		{
			var TabItem = new TspAlgorithmTab();
			this.Tabs.Add(TabItem);
			this.SelectedTab = TabItem;
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Verarbeitung des AddMenuTabCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Parameter</param>
		/// ------------------------------------------------------------------------------------------------------------
		private void OnAddMenuTabCommand(object param)
		{
			var TabItem = new MenuTab();
			this.Tabs.Add(TabItem);
			this.SelectedTab = TabItem;
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Verarbeitung des Kommandos RemoveTabCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Parameter</param>
		/// ------------------------------------------------------------------------------------------------------------
		private void OnRemoveTabCommand(object param)
		{
			this.Tabs.Remove(this.SelectedTab);
		}
	}
}
