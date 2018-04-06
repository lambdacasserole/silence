using System.Windows;
using System.Windows.Input;

namespace Silence.Hooking
{
    /// <summary>
    /// Represents a set of event arguments passed into the handler for a global mouse event.
    /// </summary>
    public class GlobalMouseEventHandlerArgs : HandleableEventArgs
    {
        /// <summary>
        /// The x- and y-coordinates of the cursor, in screen coordinates.
        /// </summary>
        public Point Point { get; }

        /// <summary>
        /// The mouse button associated with the message.
        /// </summary>
        public MouseButton Button { get; }

        /// <summary>
        /// Contains information about mouse X buttons or the mouse wheel if relevant.
        /// </summary>
        public int MouseData { get; }

        /// <summary>
        /// The event-injected flag. 
        /// </summary>
        public int Flags { get; }

        /// <summary>
        /// The time stamp for this message.
        /// </summary>
        public int Time { get; }

        /// <summary>
        /// Additional information associated with the message.
        /// </summary>
        public int ExtraInfo { get; }

        /// <summary>
        /// The mouse scroll wheel delta value if relevant.
        /// </summary>
        public int Delta { get; }
        
        /// <summary>
        /// Initialises a new instance of a set of global mouse event handler arguments.
        /// </summary>
        /// <param name="point">The x- and y-coordinates of the cursor, in screen coordinates.</param>
        /// <param name="button">Contains information about mouse X buttons or the mouse wheel if relevant.</param>
        /// <param name="mouseData">Contains information about mouse X buttons or the mouse wheel if relevant.</param>
        /// <param name="flags">The event-injected flag. </param>
        /// <param name="time">The time stamp for this message.</param>
        /// <param name="extraInfo">Additional information associated with the message.</param>
        /// <param name="delta">The mouse scroll wheel delta value if relevant.</param>
        public GlobalMouseEventHandlerArgs(Point point, MouseButton button, int mouseData, int flags, int time, int extraInfo, int delta)
        {
            Point = point;
            Button = button;
            MouseData = mouseData;
            Flags = flags;
            Time = time;
            ExtraInfo = extraInfo;
            Delta = delta;
        }
    }
}
