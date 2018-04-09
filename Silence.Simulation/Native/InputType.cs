namespace Silence.Simulation.Native
{
    /// <summary>
    /// Specifies the type of an input event.
    /// </summary>
    internal enum InputType : uint
    {
        /// <summary>
        /// The event is a mouse event.
        /// </summary>
        Mouse = 0,

        /// <summary>
        /// The event is a keyboard event.
        /// </summary>
        Keyboard = 1,

        /// <summary>
        /// The event is from input hardware other than a keyboard or mouse.
        /// </summary>
        Hardware = 2,
    }
}
