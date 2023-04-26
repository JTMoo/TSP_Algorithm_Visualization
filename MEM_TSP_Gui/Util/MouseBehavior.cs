using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;


namespace MEM_TSP.Gui.Util
{
    /// ****************************************************************************************************************
    /// <summary>
    /// Die MouseBehaviour-Klasse kann dazu verwendet werden, um die Maus innerhalb des Windows zu tracken.
    /// </summary>
    /// ****************************************************************************************************************
    public class MouseBehaviour : Behavior<Canvas>
    {
        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Zugriff auf die DependencyProperty MouseYProperty
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------
        public static readonly DependencyProperty MouseYProperty = DependencyProperty.Register(
           "MouseY", typeof(double), typeof(MouseBehaviour), new PropertyMetadata(default(double)));

        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Zugriff auf die DependencyProperty MouseXProperty
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------
        public static readonly DependencyProperty MouseXProperty = DependencyProperty.Register(
           "MouseX", typeof(double), typeof(MouseBehaviour), new PropertyMetadata(default(double)));

        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Zugriff auf MouseY
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------
        public double MouseY
        {
            get { return (double)GetValue(MouseYProperty); }
            set { SetValue(MouseYProperty, value); }
        }

        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Zugriff auf MouseX
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------
        public double MouseX
        {
            get { return (double)GetValue(MouseXProperty); }
            set { SetValue(MouseXProperty, value); }
        }

        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Registrieren von Events
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------
        protected override void OnAttached()
        {
            AssociatedObject.MouseMove += AssociatedObjectOnMouseMove;
        }

        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Abmelden von Events
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------
        protected override void OnDetaching()
        {
            AssociatedObject.MouseMove -= AssociatedObjectOnMouseMove;
        }

        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// EventHandler einer Mausbewegung
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------
        /// <param name="sender">Sende-Objekt</param>
        /// <param name="mouseEventArgs">Event-Parameter</param>
        /// ------------------------------------------------------------------------------------------------------------
        private void AssociatedObjectOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var pos = mouseEventArgs.GetPosition(AssociatedObject);
            MouseX = pos.X;
            MouseY = pos.Y;
        }
    }
}
