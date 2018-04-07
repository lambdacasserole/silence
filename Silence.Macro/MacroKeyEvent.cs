namespace Silence.Macro
{
    /// <summary>
    /// Represents a keyboard event that occurs during macro recording.
    /// </summary>
    public abstract class MacroKeyEvent : MacroEvent
    {
        /// <summary>
        /// Gets or sets the virtual key code associated with the event.
        /// </summary>
        public int VirtualKeyCode { get; set; }

        /// <summary>
        /// Initialises a new instance of a macro key event.
        /// </summary>
        /// <param name="virtualKeyCode">The virtual key code associated with the event.</param>
        protected MacroKeyEvent(int virtualKeyCode)
        {
            VirtualKeyCode = virtualKeyCode;
        }
    }
}
