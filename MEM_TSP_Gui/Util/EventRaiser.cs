using System;


namespace MEM_TSP.Gui.Util
{
    /// ****************************************************************************************************************
    /// <summary>
    /// Definieren der statischen EventRaiser-Klasse, fuer statische Eventfunktionen
    /// </summary>
    /// ****************************************************************************************************************
    public static class EventRaiser
    {
        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event-Raising Methode
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------
        /// <param name="handler">EventHandler</param>
        /// <param name="sender">Sender-Object</param>
        /// ------------------------------------------------------------------------------------------------------------
        public static void Raise(this EventHandler handler, object sender)
        {
            handler?.Invoke(sender, EventArgs.Empty);
        }

        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event-Raising Methode
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------
        /// <typeparam name="T">Definiert den Typ der EventArgs-Value</typeparam>
        /// <param name="handler">EventHandler</param>
        /// <param name="sender">Sender-Object</param>
        /// <param name="value">Wert fuer den EventArgs-Value</param>
        /// ------------------------------------------------------------------------------------------------------------
        public static void Raise<T>(this EventHandler<EventArgs<T>> handler, object sender, T value)
        {
            handler?.Invoke(sender, new EventArgs<T>(value));
        }

        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event-Raising Methode
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------
        /// <typeparam name="T">Definiert den Typ der EventArgs-Value</typeparam>
        /// <param name="handler">EventHandler</param>
        /// <param name="sender">Sender-Object</param>
        /// <param name="value">Wert fuer den EventArgs-Value</param>
        /// ------------------------------------------------------------------------------------------------------------
        public static void Raise<T>(this EventHandler<T> handler, object sender, T value) where T : EventArgs
        {
            handler?.Invoke(sender, value);
        }

        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event-Raising Methode
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------
        /// <typeparam name="T">Definiert den Typ der EventArgs-Value</typeparam>
        /// <param name="handler">EventHandler</param>
        /// <param name="sender">Sender-Object</param>
        /// <param name="value">Wert fuer den EventArgs-Value</param>
        /// ------------------------------------------------------------------------------------------------------------
        public static void Raise<T>(this EventHandler<EventArgs<T>> handler, object sender, EventArgs<T> value)
        {
            handler?.Invoke(sender, value);
        }
    }
}
