using MEM_TSP.Gui.MachineCommands;
using MEM_TSP.Kernel;
using MEM_TSP.Kernel.BusinessLogic;
using MEM_TSP.Kernel.Model;
using MEM_TSP.Kernel.Model.Enums;
using MEM_TSP.Kernel.Model.Types;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Media;


namespace MEM_TSP.Gui.ViewModel.Controls
{
	/// ****************************************************************************************************************
	/// <summary>
	/// Das ViewModel fuer das TspAlgorithmControl
	/// </summary>
	/// ****************************************************************************************************************
	class TspAlgorithmControlViewModel : NotificationBase
	{
		private string executionButtonText = string.Empty;
		private Algorithm algorithm;
		private bool dragging = false;
		private bool autoRun = false;
		private double mouseX;
		private double mouseY;
		private double distance = 0;
		private double runtime = 0;
		private int amountOfPoints = 3;
		private int indexDraggingPoint;
		private Guid guid = Guid.NewGuid();

		private PointCollection currentRoute = new PointCollection();
		private TspProblem currentTspProblem = null;


		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Konstruktor
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public TspAlgorithmControlViewModel()
		{
			this.ExecuteAlgorithmCommand = new RelayCommand(this.OnExecuteAlgorithmCommand);
			this.GenerateRandomPointsCommand = new RelayCommand(this.OnGenerateRandomPointsCommand);
			this.LeftMouseButtonDownCommand = new RelayCommand(this.OnLeftMouseButtonDownCommand);
			this.LeftMouseButtonUpCommand = new RelayCommand(this.OnLeftMouseButtonUpCommand);
			this.PreviewMouseMoveCommand = new RelayCommand(this.OnPreviewMouseMoveCommand);
			this.RightMouseButtonUpCommand = new RelayCommand(this.OnRightMouseButtonUpCommand);

			this.CurrentPoints.CollectionChanged += this.OnCurrentPointsChangedEvent;

			this.algorithm = Algorithm.NearestNeighbor;
			this.ExecutionButtonText = "Solve";
			this.AmountOfPoints = 3;
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf ExecuteAlgorithmCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public RelayCommand ExecuteAlgorithmCommand { get; internal set; }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf GenerateRandomPointsCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public RelayCommand GenerateRandomPointsCommand { get; internal set; }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf LeftMouseButtonDownCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public RelayCommand LeftMouseButtonDownCommand { get; internal set; }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf LeftMouseButtonUpCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public RelayCommand LeftMouseButtonUpCommand { get; internal set; }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf PreviewMouseMoveCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public RelayCommand PreviewMouseMoveCommand { get; internal set; }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf RightMouseButtonUpCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public RelayCommand RightMouseButtonUpCommand { get; internal set; }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf AmountOfPoints
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public int AmountOfPoints 
		{ 
			get { return this.amountOfPoints; }
			set { this.SetProperty(ref this.amountOfPoints, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf AmountOfPoints
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public bool AutoRun 
		{ 
			get { return this.autoRun; }
			set { this.SetProperty(ref this.autoRun, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf CanvasX
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public int CanvasX { get { return 500; } }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf CanvasY
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public int CanvasY { get { return 300; } }

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf die zufaelligen Punkte
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public ObservableCollection<Point> CurrentPoints { get; internal set; } = new ObservableCollection<Point>();

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf das aktuelle Ergebnis
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public PointCollection CurrentRoute
		{
			get { return this.currentRoute; }
			private set { this.SetProperty(ref this.currentRoute, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf Distance
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public double Distance
		{
			get { return this.distance; }
			private set { this.SetProperty(ref this.distance, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf ExecutionButtonText
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public string ExecutionButtonText
		{ 
			get { return this.executionButtonText; }
			private set { this.SetProperty(ref this.executionButtonText, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf MaxAmountOfPoints
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public int MaxAmountOfPoints { get; } = 50;

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf MouseX
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public double MouseX
		{
			get { return this.mouseX; }
			set { this.SetProperty(ref this.mouseX, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf MouseY
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public double MouseY
		{
			get { return this.mouseY; }
			set { this.SetProperty(ref this.mouseY, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf die Laufzeit des Algorithmus
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public double Runtime
		{
			get { return this.runtime; }
			set { this.SetProperty(ref this.runtime, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf den Selektierten Algorithmus
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public Algorithm SelectedAlgorithm
		{
			get { return this.algorithm; }
			set { this.SetProperty(ref this.algorithm, value); }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Zugriff auf TspLogger
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		public TspLogger TspLogger
		{
			get { return MainManagerFacade.TspLogger; }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Callback fuer das Solve Kommando
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="tspProblem">Tsp-Problem mit Result</param>
		/// ------------------------------------------------------------------------------------------------------------
		public void Result(TspProblem tspProblem)
		{
			this.Distance = tspProblem.Result.Distance;
			this.Runtime = tspProblem.Result.ExecutionTime;
			this.CurrentRoute = new PointCollection(tspProblem.Result.Route);

			if (tspProblem.State == ProblemStates.solved)
			{
				this.ExecutionButtonText = "Solve";
				this.currentTspProblem = null;
			}

			if (tspProblem.State == ProblemStates.resumeable)
			{
				this.ExecutionButtonText = "Resume Solve";
				this.currentTspProblem = tspProblem;
			}
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Ueberpruefen, ob die Maus außerhalb des Canvas liegt
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <returns>true, wenn die Maus außerhalb des Canvas liegt</returns>
		/// ------------------------------------------------------------------------------------------------------------
		private bool IsMouseOutOfRange()
		{
			if (this.MouseX > this.CanvasX || this.MouseY > this.CanvasY)
			{
				return true;
			}

			if (this.MouseX < 0 || this.MouseY < 0)
			{
				return true;
			}

			return false;
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Reaktion auf den ExecuteAlgorithmCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Eventparameter</param>
		/// ------------------------------------------------------------------------------------------------------------
		private void OnExecuteAlgorithmCommand(object param)
		{
			if (this.ExecutionButtonText == "Solve" || this.ExecutionButtonText == "Resume Solve")
			{
				this.OnSolveCommand(null);
			}
			else if (this.ExecutionButtonText == "Stop")
			{
				this.OnCancelAlgorithmCommand(null);
			}
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Reaktion auf den SolveCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Eventparameter</param>
		/// ------------------------------------------------------------------------------------------------------------
		private void OnSolveCommand(object param)
		{
			var data = new TspCommand.TspCommandData();

			if (this.currentTspProblem != null)
			{
				if (this.currentTspProblem.Algorithm != this.SelectedAlgorithm)
				{
					this.currentTspProblem = null;
				}
				else
				{
					data.Value = this.currentTspProblem;
				}
			}

			if (this.currentTspProblem == null)
			{
				data.Value = new TspProblem()
				{
					Algorithm = this.SelectedAlgorithm,
					Points = this.CurrentPoints.ToList(),
					Callback = this.Result,
					State = ProblemStates.open,
					Guid = this.guid
				};
			}

			this.ExecutionButtonText = "Stop";
			MainCommandInvoker.Instance.InvokeCommand(typeof(TspCommand).Name, data);
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Reaktion auf GenerateRandomPointsCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Eventparameter</param>
		/// ------------------------------------------------------------------------------------------------------------
		private void OnGenerateRandomPointsCommand(object param)
        {
			var random = new System.Random();
			this.CurrentPoints.Clear();
			this.CurrentRoute = new PointCollection();
			this.currentTspProblem = null;

			for ( int k = 0; k < this.AmountOfPoints; k++)
			{
				var newRandomX = random.Next(this.CanvasX);
				var newRandomY = random.Next(this.CanvasY);

				this.CurrentPoints.Add(new Point(newRandomX, newRandomY));
            }
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Reaktion auf das PreviewMouseMoveCommand (Ziehen eines Punkts)
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Event-Point as object</param>
		/// ------------------------------------------------------------------------------------------------------------
		private void OnPreviewMouseMoveCommand(object param)
		{
			if (this.dragging)
			{
				if (this.IsMouseOutOfRange())
				{
					return;
				}

				this.CurrentPoints[this.indexDraggingPoint] = new Point(this.MouseX, this.MouseY);
			}
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Reaktion auf den LeftMouseButtonDownCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Event-Point as object</param>
		/// ------------------------------------------------------------------------------------------------------------
		private void OnLeftMouseButtonDownCommand(object param)
		{
			this.indexDraggingPoint = this.CurrentPoints.IndexOf((Point)param);

			if (indexDraggingPoint >= 0 && this.indexDraggingPoint < this.CurrentPoints.Count)
			{
				this.dragging = true;
			}
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Reaktion auf den LeftMouseButtonUpCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Event-Point as object</param>
		/// ------------------------------------------------------------------------------------------------------------
		private void OnLeftMouseButtonUpCommand(object param)
		{
			if (!this.dragging)
			{
				return;
			}

			this.dragging = false;
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Reaktion auf den RightMouseButtonUpCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Event-Point as object</param>
		/// ------------------------------------------------------------------------------------------------------------
		private void OnRightMouseButtonUpCommand(object param)
		{
			if (this.CurrentPoints.Count < 3)
			{
				this.TspLogger.TspLog.Add("There need to be 3 or more points to add points manually.");
				return;
			}

			if (this.CurrentPoints.Count == this.MaxAmountOfPoints)
			{
				this.TspLogger.TspLog.Add("Cannot add points beyond the maximum amount.");
				return;
			}

			this.CurrentPoints.Add(new Point(this.MouseX, this.MouseY));
			this.AmountOfPoints++;
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Reaktion auf das CancelAlgorithmCommand
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Eventparameter</param>
		/// ------------------------------------------------------------------------------------------------------------
		private void OnCancelAlgorithmCommand(object param)
		{
			var data = new TspCommand.TspCommandData()
			{
				Command = TspCommand.TspCommands.Stop,
				Value = this.guid
			};

			MainCommandInvoker.Instance.InvokeCommand(typeof(TspCommand).Name, data);
		}

		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Reaktion auf  das CurrentPointsChangedEvent
		/// </summary>
		/// ------------------------------------------------------------------------------------------------------------
		/// <param name="param">Eventparameter</param>
		/// <param name="e">NotifyCollectionChangedEventArgs</param>
		/// ------------------------------------------------------------------------------------------------------------
		private void OnCurrentPointsChangedEvent(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (!this.AutoRun)
			{
				return;
			}

			if (this.CurrentPoints.Count < 3)
			{
				return;
			}

			this.OnSolveCommand(null);
		}
	}
}
