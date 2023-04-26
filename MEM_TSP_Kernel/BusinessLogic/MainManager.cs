using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using MEM_TSP.Kernel.Model;


namespace MEM_TSP.Kernel.BusinessLogic
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Die Hauptklasse der BusinessLogik
	/// </summary>
	/// ****************************************************************************************************************
	public class MainManager : IDisposable
	{
		internal static readonly MainManager Instance = new MainManager();

		private const int IntervalCommandExecution = 50;
		private bool disposed = false;

		private readonly TspLogger tspLogger = new TspLogger();
		private readonly MainCommandReceiver commandReceiver = new MainCommandReceiver();
		private CancellationTokenSource commandExecutionCancellationTokenSource;
		private Dispatcher dispatcher;


		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Destruktor
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		~MainManager()
		{
			this.Dispose(false);
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf den Tsp Manager
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------		
		internal TspManager TspManager { get; private set; }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf ApplicationState
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		internal TspLogger TspLogger
		{
			get { return this.tspLogger; }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf CommandReceiver
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		internal MainCommandReceiver CommandReceiver
		{
			get { return this.commandReceiver; }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Initialisierung
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public static void Init()
		{
			Trace.WriteLine("Init MainManager");
			Instance.Initialize();
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Freigeben von Resourcen
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public static void Release()
		{
			if (Instance != null)
			{
				Instance.CancelCommandExecution();
				if (Instance.TspManager != null)
				{
					Instance.TspManager.Release();
					Instance.TspManager = null;
				}
			}
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
		/// Synchrone Ausfuehrung im Dispatcher-Thread
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="callee">Delegate, der aufgerufen wird</param>
		/// ------------------------------------------------------------------------------------------------------------
		internal void Invoke(Action callee)
		{
			if (this.dispatcher.Thread == Thread.CurrentThread)
			{
				callee();
			}
			else
			{
				this.dispatcher.Invoke(callee);
			}
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Startet asynchrone Task.
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="action">Asynchron auszuführenden Task</param>
		/// <param name="cancellationToken">Abbruch-Token</param>
		/// <param name="creationOptions">Optionen des Tasks</param>
		/// ------------------------------------------------------------------------------------------------------------
		internal async void StartObservedTask(Action<object> action, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
		{
			try
			{
				await Task.Factory.StartNew(action, cancellationToken, creationOptions);
			}
			catch
			{
				throw;
			}
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
					this.commandExecutionCancellationTokenSource.Dispose();
					this.TspManager.Dispose();
				}

				// Free the unmanaged resource ...
				this.disposed = true;
			}
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Initialisierung der Hauptklasse
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		private void Initialize()
		{
			this.TspManager = new TspManager();
			this.dispatcher = Dispatcher.CurrentDispatcher;
			ExecutionStrategyRegistrator.RegisterWith(this.commandReceiver);

			this.InitCommandExecutionTask();
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Initialisierung Kommando Ausfuehrungs Tasks
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		private void InitCommandExecutionTask()
		{
			this.commandExecutionCancellationTokenSource = new CancellationTokenSource();
			this.StartObservedTask(_ => this.CommandExecution(), this.commandExecutionCancellationTokenSource.Token, TaskCreationOptions.LongRunning);
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Ausfuehrung der Kommandos
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		private void CommandExecution()
		{
			Thread.CurrentThread.Name = System.Reflection.MethodBase.GetCurrentMethod().Name;

			while (!this.commandExecutionCancellationTokenSource.IsCancellationRequested)
			{
				this.commandReceiver.ReceiveAndHandleCommands();
				Thread.Sleep(IntervalCommandExecution);
			}
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Abbruch der Kommando-Ausfuehrung
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		private void CancelCommandExecution()
		{
			this.commandExecutionCancellationTokenSource?.Cancel();
		}
	}
}
